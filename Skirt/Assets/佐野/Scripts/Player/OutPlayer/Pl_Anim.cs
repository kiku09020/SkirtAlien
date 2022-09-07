using UnityEngine;
using DG.Tweening;

/* ★プレイヤーのアニメーションに関するスクリプトです */
public class Pl_Anim : MonoBehaviour
{
    /* 値 */
    bool jump_Once;
    bool atk_Once;
    bool dig_Once;

    /* コンポーネント */
    GameManager gm;

    Pl_States pl_st;
    Animator anim;

    /* Tween */
    Tween twn_eat;
    Tween twn_dig;
    Tween twn_diged;

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
                jump_Once = false;
                atk_Once = false;
                dig_Once = false;
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

                if (!atk_Once) {
                    atk_Once = true;
                    anim.SetTrigger("attacking");
                    twn_eat = DOTween.Sequence().Append(transform.DOScale(new Vector2(2f, 1.5f), 0.4f).SetEase(Ease.OutCirc))
                                                .Append(transform.DOScale(new Vector2(1f, 1f), 0.25f).SetEase(Ease.OutCirc));
                }
                break;

            //------------------------------
            case Pl_States.States.digest:       // 消化中
                anim.SetTrigger("digesting");
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

        twn_dig.Kill();
        twn_diged.Kill();
        twn_eat.Kill();
    }
}
