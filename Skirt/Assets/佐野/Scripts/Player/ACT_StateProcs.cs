using UnityEngine;

/* Pl_State内に呼び出す関数 */
public partial class Pl_Action
{
    // ★ダメージ
    public void Damage_Proc()
    {
        // ダメージくらった瞬間
        if (dmgTimer == 0.0f) {
            InstantDamage();
        }

        dmgTimer += Time.deltaTime;       // タイマー増加

        transform.localScale = Vector2.one;     // 大きさ戻す

        // 点滅
        var alpha = Mathf.Cos(2 * Mathf.PI * dmgTimer / flashCycle);
        sr.color = new Color(1, 1, 1, alpha);


        // 時間経過後
        if (dmgTimer > dmgTimeLim) {
            dmgTimer = 0;
            sr.color = Color.white;
            st.stateNum = Pl_States.States.normal;
        }
    }

    // ダメージくらった瞬間
    public void InstantDamage()
    {
        hp.HP_Damage();                                                         // HP減らす
        combo.ComboSetter(ComboManager.CmbEnum.reset);                          // 消化コンボ数リセット
        rb.AddForce(Vector2.up * dmgJumpForce);                                 // 少し飛ばす

        aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.damage);     // 効果音
        part.InstPart(Pl_Particle.PartNames.damaged,transform.position);                           // パーティクル
        Vibration.Vibrate(300);                                                 // スマホ振動
    }

    //------------------------------------------
    // ★捕食
    public void Eating()
    {
        // 捕食した瞬間
        if (eatTimer == 0) {
            hung.HungDec_Atk();                                                 // 空腹度減らす

            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.eat);    // 効果音
            part.InstPart(Pl_Particle.PartNames.eat, transform.position);                           // パーティクル
        }

        eatTimer += Time.deltaTime;      // タイマー増加

        // 時間経過後
        if (eatTimer > eatTimeLim) {
            eatTimer = 0;
            st.stateNum = Pl_States.States.normal;
        }
    }

    //------------------------------------------
    // ★消化ボタン処理
    public void Digest_Btn()
    {
        if (digBtnCnt < digBtnCntMax) { // ●消化
            digBtnCnt++;            // 消化ボタン回数増加

            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.dig);        // 効果音
            part.InstPart(Pl_Particle.PartNames.digit, transform.position);         // パーティクル
            anim.DigBtnAnim();                                                      // アニメーション
        }

        else {      // ●消化完了時
            combo.ComboSetter(ComboManager.CmbEnum.inc);                            // コンボ数増加
            score.AddScore();                                                       // スコア追加
            hung.HungInc(combo.GetCmbMag());                                        // 満腹度増加

            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.digDone);    // 効果音
            part.InstPart(Pl_Particle.PartNames.eated, transform.position);                             // パーティクル

            digBtnCnt = 0;                                                          // 消化ボタン回数0にする
            st.stateNum = Pl_States.States.normal;                                  // 通常状態に戻す
        }
    }

    //------------------------------------------
    // ★ジャンプ
    public void Jump()
    {
        // ジャンプした瞬間
        if (jumpTimer == 0) {
            if (hung.hungFlg)   { nowJumpForce = nmlJumpForce * 0.75f; }    // 空腹時
            else                { nowJumpForce = nmlJumpForce; }            // 戻す

            rb.AddForce(Vector2.up * nowJumpForce);                         // ジャンプ

            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.jump);       // 効果音再生
            part.InstPart(Pl_Particle.PartNames.jump, transform.position);          // パーティクル生成
            anim.Jump();                                                            // アニメーション
            sr.color = Color.white;                                                 // 色変更
        }

        jumpTimer += Time.deltaTime;

        // 時間経過後
        if (jumpTimer > jumpTime) {
            jumpTimer = 0;
            st.stateNum = Pl_States.States.normal;
        }
    }

    //------------------------------------------
    // 値リセット
    public void ResetValues()
    {
        digBtnCnt = 0;
        dmgTimer  = 0;
        eatTimer  = 0;
        jumpTimer = 0;
    }
}
