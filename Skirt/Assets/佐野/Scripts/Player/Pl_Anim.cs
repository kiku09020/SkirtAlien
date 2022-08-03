using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★〇〇に関するスクリプトです */
//-------------------------------------------------------------------
public class Pl_Anim : MonoBehaviour
{
    /* 値 */
    bool jump_Once;
    bool atk_Once;

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
                anim.SetBool("walking", false);
                anim.SetBool("landing", false);
                jump_Once = false;
                atk_Once = false;
                break;

            //------------------------------
            case Pl_States.States.floating:      // ふわふわ
                anim.SetTrigger("float");
                anim.SetBool("walking", false);
                anim.SetBool("landing", false);
                break;

            //------------------------------
            case Pl_States.States.swooping:      // 急降下
                anim.SetTrigger("swoop");
                anim.SetBool("walking", false);
                anim.SetBool("landing", false);
                break;

            //------------------------------
            case Pl_States.States.landing:       // 地上
                // 歩行
                if (gm.inpVer != 0) {
                    anim.SetBool("walking", true);
                    anim.SetBool("landing", false);
                }

                // 地上で停止
                else {
                    anim.SetBool("landing", true);
                    anim.SetBool("walking", false);
                }
                break;

            //------------------------------
            case Pl_States.States.jumping:       // ジャンプ中

                // 一度のみ再生
                if (!jump_Once) {
                    jump_Once = true;
                    anim.SetTrigger("isJumping");
                    anim.SetBool("landing", false);
                }

                break;

            //------------------------------
            case Pl_States.States.attacking:     // 捕食中

                if (!atk_Once) {
                    atk_Once = true;
                    anim.SetTrigger("isAttacking");
                }
                break;

            //------------------------------
            case Pl_States.States.damage:        // ダメージ時
                anim.SetTrigger("damaged");
                break;

            //------------------------------
            case Pl_States.States.goaled:        // ゴール時
                break;
        }
    }
}
