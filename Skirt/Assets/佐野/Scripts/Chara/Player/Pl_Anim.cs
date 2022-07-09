using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pl_Anim : MonoBehaviour {
    /* 値 */


    /* フラグ */


    /* オブジェクト */


    /* コンポーネント取得用 */
    Animator anim;

    Player pl;
    Pl_States pl_st;

    //-------------------------------------------------------------------

    void Start()
    {
        /* コンポーネント取得 */
        anim = GetComponent<Animator>();

        pl = GetComponent<Player>();
        pl_st = GetComponent<Pl_States>();

        /* 初期化 */

    }

    //-------------------------------------------------------------------

    void FixedUpdate()
    {
        // ダメージ
        if (pl_st.isDamaged) {
            anim.SetBool("isDamaged", true);
        }

        // 攻撃
        else if (pl_st.isAttacking) {
            anim.SetBool("isAttack", true);
            anim.SetBool("isDamaged", false);
        }

        // 地上
        else if (pl_st.isLanding) {
            anim.SetBool("isLanding", true);        // 地上モーション
            anim.SetBool("isAttack", false);
            anim.SetBool("isFloat", false);
            anim.SetBool("isSwoop", false);
            

            if (pl.inp_ver != 0) {
                anim.SetBool("isWalk", true);
                anim.SetBool("isLanding", false);        // 地上モーション
            }

            else {
                anim.SetBool("isWalk", false);
            }
        }

        // 3つの状態
        else {
            // ふわふわ
            if (pl_st.isFloating) {
                anim.SetBool("isFloat", true);
            }

            // 急降下
            else if (pl_st.isSwooping) {
                anim.SetBool("isSwoop", true);
            }

            // 通常
            else {

                anim.SetBool("isFloat", false);
                anim.SetBool("isSwoop", false);
                anim.SetBool("isLanding", false);
            }
        }
    }
}
