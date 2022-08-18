using UnityEngine;

/* Pl_State内に呼び出す関数 */
public partial class Pl_Action
{
    // ダメージ
    public void Damage()
    {
        dmgCnt++;

        transform.localScale = Vector2.one;

        // ダメージくらった瞬間
        if (dmgCnt == 1) {
            hp.HP_Damage();
            part.Part_Damaged();
            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.damage);
            rb.AddForce(Vector2.up * dmgJumpForce);         // 少し飛ばす
        }

        // 点滅
        if (dmgCnt % 2 == 0) {
            sr.color = Color.clear;
        }

        else {
            sr.color = Color.white;
        }

        // ダメージ処理終了
        if (dmgCnt > invTime) {
            dmgCnt = 0;
            st.stateNum = Pl_States.States.normal;
        }
    }

    // 捕食
    public void Eating()
    {
        eatCnt++;      // カウンター増加

        if (eatCnt == 1) {
            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.eat);
            part.Part_Eating();
        }

        // 時間経過後、通常状態へ戻る
        if (eatCnt > eatTime) {
            eatCnt = 0;
            st.stateNum = Pl_States.States.normal;
        }
    }

    // 消化ボタン処理
    public void Digest_Btn()
    {
        // 消化
        if (digBtnCnt < digBtnCntMax) {
            digBtnCnt++;                                                            // 消化ボタン回数増加

            anim.DigBtnAnim();                                                      // アニメーション
            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.dig);        // 効果音
        }

        // 消化完了時(最後の消化)
        else {
            hung.HungInc();                                                         // 満腹度増やす
            digBtnCnt = 0;                                                          // 消化ボタン回数0にする
            hung.EatCntSetter(Pl_Hunger.EatenCntEnum.inc);                          // 消化数増加
            st.stateNum = Pl_States.States.normal;                                  // 通常状態に戻す

            anim.DigDoneAnim();                                                     // アニメーション
            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.digDone);    // 効果音再生
        }
    }

    // ジャンプ
    public void Jump()
    {
        jumpCnt++;

        // 一瞬ジャンプ
        if (jumpCnt == 1) {
            if (hung.hungFlg) {
                nowJumpForce = normalJumpForce * 0.75f;
            }

            else {
                nowJumpForce = normalJumpForce;
            }

            rb.AddForce(Vector2.up * nowJumpForce);

            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.jump);
            part.Part_Jumping();
        }

        // 解除
        if (jumpCnt > jumpTime) {
            jumpCnt = 0;
            st.stateNum = Pl_States.States.normal;
        }
    }

    // 値リセット
    public void ResetValues()
    {
        digBtnCnt = 0;
        dmgCnt = 0;
        eatCnt = 0;
        jumpCnt = 0;
    }
}
