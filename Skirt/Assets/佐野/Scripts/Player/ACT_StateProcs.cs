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
            rb.AddForce(Vector2.up * dmgJumpForce);                                 // 少し飛ばす
            hp.HP_Damage();                                                         // HP減らす
            hung.ComboSetter(Pl_Hunger.ComboEnum.reset);                            // 消化コンボ数リセット
                                                                                    
            part.InstPart(Pl_Particle.PartNames.damaged);                           // パーティクル生成
            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.damage);     // 効果音再生
            Vibration.Vibrate(300);                                                 // スマホ振動
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
            part.InstPart(Pl_Particle.PartNames.eat);
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
            part.InstPart(Pl_Particle.PartNames.eating);                            // パーティクル生成
        }

        // 消化完了時(最後の消化)
        else {
            hung.HungInc();                                                         // 満腹度増やす
            digBtnCnt = 0;                                                          // 消化ボタン回数0にする
            hung.ComboSetter(Pl_Hunger.ComboEnum.inc);                          // コンボ数増加

            score.AddScore(hung.eatCombo);                                          // スコア追加
            anim.DigDoneAnim();                                                     // アニメーション
            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.digDone);    // 効果音再生
            part.InstPart(Pl_Particle.PartNames.eated);

            st.stateNum = Pl_States.States.normal;                                  // 通常状態に戻す
        }
    }

    // ジャンプ
    public void Jump()
    {
        jumpCnt++;

        // 一瞬ジャンプ
        if (jumpCnt == 1) {

            // 空腹時のジャンプ力
            if (hung.hungFlg) {
                nowJumpForce = normalJumpForce * 0.75f;
            }

            // 通常時のジャンプ力
            else {
                nowJumpForce = normalJumpForce;
            }

            // ジャンプ
            rb.AddForce(Vector2.up * nowJumpForce);

            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.jump);       // 効果音再生
            part.InstPart(Pl_Particle.PartNames.jump);                              // パーティクル生成
            sr.color = Color.white;                                                 // 色変更
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
