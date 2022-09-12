using UnityEngine;
using DG.Tweening;

/* ★プレイヤーのアニメーションに関するスクリプトです */
public class Pl_Anim : MonoBehaviour
{
    /* 値 */
    bool dig_Once;

    /* コンポーネント */
    GameManager gm;

    Pl_States pl_st;
    Animator anim;

    /* Tween */
    Tween twn_dig;
    Tween twn_diged;
    Tween twn_eatStrt;

    //-------------------------------------------------------------------
    void Start()
    {
        /* オブジェクト検索 */
        GameObject gm_obj = GameObject.Find("GameManager");

        /* コンポーネント取得 */
        gm = gm_obj.GetComponent<GameManager>();

        pl_st = GetComponent<Pl_States>();
        anim = GetComponent<Animator>();
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
                ResetAnims();
                break;

            //------------------------------
            case Pl_States.States.floating:      // ふわふわ
                anim.SetTrigger("float");
                ResetAnims();
                break;

            //------------------------------
            case Pl_States.States.swooping:      // 急降下
                anim.SetTrigger("swoop");
                ResetAnims();
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
            case Pl_States.States.eating:       // 捕食中
                ResetAnims();
                break;

            //------------------------------
            case Pl_States.States.digest:       // 消化中
                anim.SetTrigger("digesting");
                anim.SetBool("eatStart", false);
                if (!dig_Once) {
                    dig_Once = true;
                    twn_dig = transform.DOScale(new Vector2(1.2f, 1.2f), 0.25f)
                                       .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCirc);
                }
                
                break;

            //------------------------------
            case Pl_States.States.damage:       // ダメージ時
                anim.SetTrigger("damaged");
                ResetAnims();
                break;
        }
    }

    public void EatingStart()
    {
        anim.SetBool("eatStart", true);
        twn_eatStrt = transform.DOScale(new Vector2(1f, 0.5f), 0.4f).SetEase(Ease.OutCirc).SetRelative(true);
    }

    public void EatingEnd()
    {
        anim.SetBool("eatEnd", true);
        anim.SetBool("eatStart", false);
        transform.DOScale(Vector2.one, 0.25f).SetEase(Ease.OutCirc);
        twn_eatStrt.Kill();
    }

    // 消化中にボタン押したときのアニメーション
    public void DigBtnAnim()
    {
        transform.DOShakeRotation(0.25f, new Vector3(0, 0, 30)).SetEase(Ease.InCirc);
    }

    public void Jump()
    {
        anim.SetTrigger("jumping");
        ResetAnims();
    }

    void ResetAnims()
    {
        anim.SetBool("walking", false);
        anim.SetBool("landing", false);
        anim.SetBool("eatEnd", false);

        dig_Once = false;
        twn_dig.Kill();
        twn_diged.Kill();
    }
}
