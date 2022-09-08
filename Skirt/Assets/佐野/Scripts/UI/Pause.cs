using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* ★注意書きの制御に関するスクリプトです */
//-------------------------------------------------------------------
public class Pause : MonoBehaviour
{
    /* 値 */


    /* フラグ */
    public bool retry;
    public bool quit;

    //-------------------------------------------------------------------
    // Cautionキャンバス生成後に呼び出し
    public void SetText()
    {
        GameObject cautObj = GameObject.Find("Caution").transform.Find("Text").gameObject;
        
        string cautReasonText;
        string cautConfirmText = "本当によろしいですか？";

        if (retry) {
            cautReasonText = "ステージのはじめからやり直します。\n";
            cautObj.GetComponent<Text>().text = cautReasonText + cautConfirmText;

        }

        else if (quit) {
            cautReasonText = "タイトルに戻ります。\n";
            cautObj.GetComponent<Text>().text = cautReasonText + cautConfirmText;
        }
    }
}
