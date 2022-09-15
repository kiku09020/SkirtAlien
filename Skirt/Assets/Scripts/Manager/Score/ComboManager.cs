using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* ★〇〇に関するスクリプトです */
//-------------------------------------------------------------------
public class ComboManager : MonoBehaviour
{
    /* コンボ */
    public int cmbCnt;                  // 消化した敵の数
    int  cmbTargCnt;                    // 目的のコンボ数
    int  cmbStepNum;                    // コンボ倍率の段階数
    int  cmbMag;                        // コンボ倍率

    bool cmbFlg;                        // コンボ可能か
    float  cmbTimer;                      // 消化してからのタイマー
    [SerializeField] float cmbLimTime;  // コンボまでの制限時間

    // 消化数の扱い
    public enum CmbEnum {
        inc,        // 増やす
        reset,      // 0にする
    }

    /* オブジェクト */
    GameObject cmbTextObj;
    GameObject cmbTimerImgObj;

    /* コンポーネント取得用 */
    Text        cmbText;
    Image       cmbTimerImg;

    //-------------------------------------------------------------------
    void Start()
    {
        FindObj();
        GetComp();

        /* 初期化 */
        ResetCombo();
    }

    /* オブジェクト検索 */
    void FindObj()
    {
        cmbTextObj = GameObject.Find("Combo");
        cmbTimerImgObj = cmbTextObj.transform.Find("Image").gameObject;
    }

    /* コンポーネント取得 */
    void GetComp()
    {
        cmbText = cmbTextObj.GetComponent<Text>();
        cmbTimerImg = cmbTimerImgObj.GetComponent<Image>();
    }

//-------------------------------------------------------------------

    void FixedUpdate()
    {
        ComboTimer();
        DispComboMag();
    }

//-------------------------------------------------------------------

    // コンボ倍率のタイマー
    void ComboTimer()
	{
		if(cmbFlg) {
            cmbTimer += Time.deltaTime;
            cmbTimerImg.fillAmount = 1 - (cmbTimer / cmbLimTime);       // コンボタイマーのImage
		}

        // コンボ制限時間過ぎたとき、コンボ数リセット
        if (cmbTimer > cmbLimTime) {
            ResetCombo();
        }
	}

    // コンボ加算
    void AddCombo()
    {
        // 2回目以降
        if (cmbFlg) {
            cmbCnt++;
            cmbTimer = 0;
        }

        // 1回目
        else {
            cmbFlg = true;
        }
    }

    // コンボのリセット
    void ResetCombo()
    {
        cmbCnt = 0;
        cmbTargCnt = 2;
        cmbStepNum = 0;
        cmbMag = 1;

        cmbTimer = 0;
        cmbFlg = false;
    }

    // コンボ倍率のの表示
    void DispComboMag()
	{
        // 倍率が2以上のときのみ表示
        if(cmbMag > 1) {
            cmbText.gameObject.SetActive(true);
            cmbText.text = "×" + cmbMag.ToString();
        }

        else {
            cmbText.gameObject.SetActive(false);
        }
    }

    // コンボ処理(スコア加算時に呼び出し)
    public int Combo(int score)
    {
        // コンボ数が目的のコンボ数に達したとき
        if (cmbCnt >= cmbTargCnt) {
            cmbStepNum++;                       // 段階数増やす
            cmbTargCnt += (cmbStepNum * 2);     // 目標コンボ数 = 目標コンボ数 + コンボ段階数 * 2
            cmbMag *= 2;                        // コンボ倍率2倍に
        }

        // 追加するスコア*コンボ倍率
        return score *= cmbMag;
    }

    // コンボ数を増減する
    public void ComboSetter(CmbEnum setType)
    {
        switch (setType) {
            // 増やす
            case CmbEnum.inc:
                AddCombo();
                break;

            // リセット
            case CmbEnum.reset:
                ResetCombo();
                break;
        }
    }

    public int GetCmbMag()
    {
        return cmbMag;
    }
}
