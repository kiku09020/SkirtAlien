using UnityEngine;

public partial class Pl_Action {

    // 捕食中
    public void Eating()
    {
        if (eatTimer == 0) {
            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.eat);    // 効果音
            part.InstPart(ParticleManager.PartNames.eat, transform.position);   // パーティクル
            anim.EatingStart();
        }

        rb.drag = 0.8f;
        eatTimer += Time.deltaTime;
    }

    // 捕食終了時
    public void Eatend()
    {
        anim.EatingEnd();
        eatTimer = 0;
    }

    //------------------------------------------
    // 消化中
    public void Digest()
    {
        rb.drag = 0;
        sr.color = Color.white;
    }

    // 消化ボタン処理
    public void Digest_Btn()
    {
        // ●消化
        if (digBtnCnt < digBtnCntMax) { 
            digBtnCnt++;            // 消化ボタン回数増加

            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.dig);        // 効果音
            part.InstPart(ParticleManager.PartNames.digit, transform.position);     // パーティクル
            anim.DigBtnAnim();                                                      // アニメーション
        }

        // ●消化完了時
        else {      
            combo.ComboSetter(ComboManager.CmbEnum.inc);                            // コンボ数増加
            score.AddScore();                                                       // スコア追加

            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.digDone);    // 効果音
            part.InstPart(ParticleManager.PartNames.eated, transform.position);     // パーティクル
            Eatend();

            digBtnCnt = 0;                                                          // 消化ボタン回数0にする
            st.nowState = Pl_States.States.normal;                                  // 通常状態に戻す
        }
    }
}
