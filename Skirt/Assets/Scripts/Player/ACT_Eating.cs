using UnityEngine;
using UnityEngine.UI;

public partial class Pl_Action {

    // 捕食中
    public void Eating()
    {
        if (eatTimer == 0) {
            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.eat);    // 効果音
            part.InstPart(ParticleManager.PartNames.eat, transform.position);   // パーティクル
            anim.EatingStart(exp.GetLvSize());
        }

        // 最大数に達したら、自動的に消化状態に遷移
        if (eatingCnt == exp.GetCanEatCnt()) {
            st.nowState = Pl_States.States.digest;
        }

        rb.drag = exp.GetDrag();
        eatTimer += Time.deltaTime;
    }

    // 捕食終了時
    public void EatEnd()
    {
        if (eatingCnt == 0) {
            st.nowState = Pl_States.States.normal;
            EatReset();
        }

        else if (eatingCnt > 0) {
            st.nowState = Pl_States.States.digest;
        }
    }

    // 捕食数加算
    public void AddEatCnt()
    {
        // レベルごとの最大捕食数より小さいとき加算
        if (eatingCnt < exp.GetCanEatCnt()) {
            eatingCnt++;
        }
    }

    //------------------------------------------
    // 消化中
    public void Digest()
    {
        rb.drag = 0.25f;
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
            combo.ComboSetter(ComboManager.CmbEnum.inc,eatingCnt);                            // コンボ数増加
            score.AddScore(eatingCnt);                                              // スコア追加
            exp.IncExp(combo.GetCmbMag());

            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.digDone);    // 効果音
            part.InstPart(ParticleManager.PartNames.eated, transform.position);     // パーティクル
            EatReset();

            digBtnCnt = 0;                                                          // 消化ボタン回数0にする
            st.nowState = Pl_States.States.normal;                                  // 通常状態に戻す
        }
    }

    void EatCoolTimer()
    {

        if (ctFlg) {
            ct += Time.deltaTime;

            if (ct > ctLim) {
                canEat = true;
                ctFlg = false;
                ct = 0;
            }
        }
    }

    public void EatReset()
    {
        anim.EatingEnd();
        eatTimer = 0;

        ctFlg = true;
        canEat = false;
        eatingCnt = 0;
    }
}
