using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/* ★〇〇に関するスクリプトです */
//-------------------------------------------------------------------
public class Pl_Anim : MonoBehaviour
{
    /* 値 */
    bool jump_Once;
    bool atk_Once;
    bool dig_Once;

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

    void FixedUpdate()
    {
        StatesAnim();
    }

//-------------------------------------------------------------------

    void StatesAnim()
    {
        switch (pl_st.stateNum) {
            case Pl_States.States.normal:        // 通常時
                anim.SetTrigger("normal");
                anim.SetBool("walking", false);
                anim.SetBool("landing", false);
                jump_Once = false;
                atk_Once = false;
                dig_Once = false;

                DOTween.KillAll();
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
                    anim.SetTrigger("jumping");
                    anim.SetBool("landing", false);
                    anim.SetBool("walking", false);
                }

                break;

            //------------------------------
            case Pl_States.States.eating:       // 捕食中

                if (!atk_Once) {
                    atk_Once = true;
                    anim.SetTrigger("attacking");
                    DOTween.Sequence()  .Append(transform.DOScale(new Vector2(2f, 1.5f), 0.4f).SetEase(Ease.OutCirc))
                                        .Append(transform.DOScale(new Vector2(1f, 1f), 0.25f).SetEase(Ease.OutCirc));
                }
                break;

            //------------------------------
            case Pl_States.States.digest:       // 消化中
                anim.SetTrigger("digesting");
                if (!dig_Once) {
                    dig_Once = true;
                    transform.DOScale(new Vector2(1.2f, 1.2f), 0.25f)
                        .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCirc);
                }
                
                break;

            //------------------------------
            case Pl_States.States.damage:       // ダメージ時
                anim.SetTrigger("damaged");
                DOTween.KillAll();
                break;

            //------------------------------
            case Pl_States.States.goaled:       // ゴール時
                break;
        }
    }

    // 消化中にボタン押したときのアニメーション
    public void DigBtnAnim()
    {
        transform.DOShakeRotation(0.25f, new Vector3(0, 0, 30)).SetEase(Ease.InCirc);
    }
}
