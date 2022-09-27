using UnityEngine;

/* Pl_State内に呼び出す関数 */
public partial class Pl_Action
{
    // ★ダメージ
    public void Damage()
    {
        // ダメージくらった瞬間
        if (dmgTimer == 0) {
            InstantDamage();
        }

        else {
            transform.localScale = Vector2.one;     // 大きさ戻す

            // 点滅
            var alpha = Mathf.Cos(2 * Mathf.PI * dmgTimer / flashCycle);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
        }

        dmgTimer += Time.deltaTime;       // タイマー増加

        // 時間経過後
        if (dmgTimer > dmgTimeLim) {
            sr.color = Color.white;
            st.dmgFlg = false;
            dmgTimer = 0;
        }
    }

    // ダメージくらった瞬間
    public void InstantDamage()
    {
        hp.HP_Damage();                                     // HP減らす
        combo.ComboSetter(ComboManager.CmbEnum.reset);      // 消化コンボ数リセット
        rb.AddForce(Vector2.up * dmgJumpForce);             // 少し飛ばす

        Vibration.Vibrate(300);                             // スマホ振動
    }

    //------------------------------------------
    public void Eating()
    {
        if (eatTimer == 0) {
            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.eat);    // 効果音
            part.InstPart(ParticleManager.PartNames.eat, transform.position);   // パーティクル
            anim.EatingStart();
        }

        eatTimer += Time.deltaTime;
    }

    public void Eatend()
    {
        anim.EatingEnd();
        eatTimer = 0;
    }

    //------------------------------------------
    // ★消化ボタン処理
    public void Digest_Btn()
    {
        if (digBtnCnt < digBtnCntMax) { // ●消化
            digBtnCnt++;            // 消化ボタン回数増加

            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.dig);        // 効果音
            part.InstPart(ParticleManager.PartNames.digit, transform.position);     // パーティクル
            anim.DigBtnAnim();                                                      // アニメーション
        }

        else {      // ●消化完了時
            combo.ComboSetter(ComboManager.CmbEnum.inc);                            // コンボ数増加
            score.AddScore();                                                       // スコア追加
            hung.HungInc(combo.GetCmbMag());                                        // 満腹度増加

            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.digDone);    // 効果音
            part.InstPart(ParticleManager.PartNames.eated, transform.position);     // パーティクル
            Eatend();

            digBtnCnt = 0;                                                          // 消化ボタン回数0にする
            st.nowState = Pl_States.States.normal;                                  // 通常状態に戻す
        }
    }
}
