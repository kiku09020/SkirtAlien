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
    int cmbTargCnt;     // 目的のコンボ数
    int cmbStepNum;     // コンボ倍率の段階数
    int cmbMag;         // コンボ倍率

    // 消化数の扱い
    public enum ComboEnum {
        inc,        // 増やす
        dec,        // 減らす
        reset,      // 0にする
    }

    /* フラグ */


    /* オブジェクト */
    GameObject comboTextObj;

    /* コンポーネント取得用 */
    Text comboText;


    //-------------------------------------------------------------------
    void Start()
    {
        FindObj();
        GetComp();

        /* 初期化 */
        ComboReset();
    }

    /* オブジェクト検索 */
    void FindObj()
    {
        comboTextObj = GameObject.Find("Combo");
    }

    /* コンポーネント取得 */
    void GetComp()
    {
        comboText = comboTextObj.GetComponent<Text>();
    }

//-------------------------------------------------------------------

    void Update()
    {
        if (cmbMag > 1) {
            comboText.text = "×" + cmbMag.ToString();
        }
        else {
            comboText.text = "";
        }
    }

//-------------------------------------------------------------------

    public int Combo(int score)
    {
        // コンボ処理
        if (cmbCnt >= cmbTargCnt) {
            cmbStepNum++;                       // 段階数増やす
            cmbTargCnt += (cmbStepNum * 2);       // 目標コンボ数 = 目標コンボ数 + コンボ段階数 * 2
            cmbMag *= 2;
        }

        Debug.Log("コンボ倍率 = " + cmbMag);

        // 追加するスコア*コンボ倍率
        return score *= cmbMag;
    }

    public void ComboReset()
    {
        cmbTargCnt = 2;
        cmbStepNum = 0;
        cmbMag = 1;
    }

    // 消化した敵の数を増減する
    public void ComboSetter(ComboEnum setType)
    {
        switch (setType) {
            // 増やす
            case ComboEnum.inc:
                cmbCnt++;
                break;

            //減らす
            case ComboEnum.dec:
                cmbCnt--;
                break;

            // 0に戻す
            case ComboEnum.reset:
                cmbCnt = 0;
                break;
        }
    }
}
