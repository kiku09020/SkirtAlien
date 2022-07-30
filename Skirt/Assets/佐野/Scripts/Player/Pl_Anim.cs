using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★〇〇に関するスクリプトです */
//-------------------------------------------------------------------
public class Pl_Anim : MonoBehaviour
{
    /* 値 */


    /* ゲームオブジェクト */
    GameObject gm_obj;

    /* コンポーネント */
    GameManager gm;

    Pl_States pl_st;
    Animator anim;
    //-------------------------------------------------------------------

    void Start()
    {
        /* オブジェクト検索 */
        gm_obj = GameObject.Find("GameManager");

        /* コンポーネント取得 */
        gm = gm_obj.GetComponent<GameManager>();

        pl_st = GetComponent<Pl_States>();
        anim = GetComponent<Animator>();
        /* 初期化 */
    }

//-------------------------------------------------------------------

    void Update()
    {
        StatesAnim();
    }

//-------------------------------------------------------------------

    void StatesAnim()
    {
        switch (pl_st.stateNum) {
            case Pl_States.States.normal:        // 通常時
                anim.SetBool("isFloat", false);
                anim.SetBool("isSwoop", false);
                anim.SetBool("isDamaged", false);
                anim.SetBool("isAttack", false);
                anim.SetBool("isLanding", false);

                break;

            //------------------------------
            case Pl_States.States.floating:      // ふわふわ
                anim.SetBool("isFloat", true);
                break;

            //------------------------------
            case Pl_States.States.swooping:      // 急降下
                anim.SetBool("isSwoop", true);
                break;

            //------------------------------
            case Pl_States.States.landing:       // 地上
                anim.SetBool("isLanding", true);
                anim.SetBool("isWalk", false);

                // 歩行
                if (gm.inpVer != 0) {
                    anim.SetBool("isWalk", true);
                    anim.SetBool("isLanding", false);
                }

                break;

            //------------------------------
            case Pl_States.States.jumping:       // ジャンプ中
                break;

            //------------------------------
            case Pl_States.States.attacking:     // 捕食中
                anim.SetBool("isAttack", true);
                break;

            //------------------------------
            case Pl_States.States.damage:        // ダメージ時
                anim.SetBool("isDamaged", true);
                break;

            //------------------------------
            case Pl_States.States.goaled:        // ゴール時
                break;
        }
    }
}
