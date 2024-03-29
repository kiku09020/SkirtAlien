using UnityEngine;
using DG.Tweening;

/* ★プレイヤーのアニメーションに関するスクリプトです */
public class PlayerAnim : MonoBehaviour
{
    /* 値 */
    bool dig_Once;

    /* コンポーネント */
    GameManager gm;

    Pl_States st;
    Animator  anim;

    /* Tween */
    Tween twn_dig;
    Tween twn_eatStrt;

    //-------------------------------------------------------------------
    void Start()
    {
        /* オブジェクト検索 */
        GameObject gm_obj = GameObject.Find("GameManager");

        /* コンポーネント取得 */
        gm = gm_obj.GetComponent<GameManager>();

        st = GetComponent<Pl_States>();
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
        switch (st.nowState) {
            case Pl_States.States.normal:        // 通常時
                anim.SetTrigger("normal");
                ResetAnims();
                break;

            //------------------------------
            case Pl_States.States.eating:
                anim.SetBool("damaged", false);
                break;

            case Pl_States.States.digest:       // 消化中

                anim.SetTrigger("digesting");
                anim.SetBool("eatStart", false);
                anim.SetBool("landing", false);
                anim.SetBool("damaged", false);

                if (!dig_Once) {
                    dig_Once = true;
                    twn_dig = transform.DOScale(new Vector2(1.2f, 1.2f), 0.25f)
                                       .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCirc);
                }

                break;
        }

        if (st.dmgFlg) {
            if (st.nowState == Pl_States.States.normal) {
                anim.SetBool("damaged", true);
            }
        }

        else {
            anim.SetBool("damaged", false);
        }

        if (st.lndFlg && st.nowState == Pl_States.States.normal) {
            Laning();               // 地上
        }


        // ゲームオーバー
        if (gm.isGameOver) {
            ResetAnims();
            anim.SetTrigger("dead");
        }
    }

    //-------------------------------------------------------------------
    // 地上
    void Laning()
    {
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
    }

    //-------------------------------------------------------------------
    // 捕食した瞬間
    public void EatingStart(Vector2 eatingSize)
    {
        anim.SetBool("eatStart",true);
        twn_eatStrt = transform.DOScale(eatingSize, 0.4f).SetEase(Ease.OutCirc).SetRelative(true);
    }

    // 捕食終了した瞬間
    public void EatingEnd()
    {
        transform.DOScale(Vector2.one, 0.25f).SetEase(Ease.OutCirc);
        twn_eatStrt.Kill();
    }

    // 食われた敵のアニメーション
    public void EatenEnmy(GameObject enemyObj,float destTime)
    {
        enemyObj.transform.DOMove(transform.position, destTime).SetEase(Ease.InCubic);
        enemyObj.transform.DOScale(Vector2.zero, destTime).SetEase(Ease.InCubic);
    }

    // 消化中にボタン押したときのアニメーション
    public void DigBtnAnim()
    {
        transform.DOShakeRotation(0.25f, new Vector3(0, 0, 30)).SetEase(Ease.InCirc);
    }

    public void LvUp(Vector2 size)
    {
        DOTween.KillAll();
        var seq = DOTween.Sequence();

        seq.Append(transform.DOScale(size, 0.4f).SetEase(Ease.OutCirc))
           .Append(transform.DOScale(1, 0.2f).SetEase(Ease.OutCirc));
    }

    //-------------------------------------------------------------------
    // ジャンプ
    public void Jump()
    {
        anim.SetTrigger("jumping");
        ResetAnims();
    }

    //-------------------------------------------------------------------
    // アニメーションリセット
    void ResetAnims()
    {
        anim.SetBool("eatStart", false);
        anim.SetBool("walking", false);
        anim.SetBool("landing", false);

        dig_Once = false;
        twn_dig.Kill();
    }
}
