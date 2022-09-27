using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* ★経験値に関するスクリプトです */
public class Pl_EXP : MonoBehaviour
{
    /* 値 */
    [Header("レベル")]
    [SerializeField] int maxLevel;      // 最大レベル
    int nowLevel;                       // 現在のレベル
    
    [Header("経験値")]
    [SerializeField] float maxExp;      // 最大経験値
    float nowExp;                       // 現在の経験値

    [SerializeField] float incExp;      // 経験値の増加量
    [SerializeField] float decExp;      // 経験値の減少量

    [Header("表示")]
    [SerializeField] float dispExpVal;  // 一度に増減する量
    float nowDispExp;                   // 現在の表示経験値

    /* コンポーネント取得用 */
    Image barImg;   // バーの画像
    Text  lvTxt;    // レベルのテキスト

    //-------------------------------------------------------------------
    void Start()
    {
        /* オブジェクト取得 */
        GameObject expObj = GameObject.Find("EXP");
        GameObject barobj = expObj.transform.Find("EXPBar").gameObject;
        GameObject txtObj = expObj.transform.Find("LvTxt").gameObject;

        /* コンポーネント取得 */
        barImg = barobj.GetComponent<Image>();
        lvTxt = txtObj.GetComponent<Text>();

        /* 初期化 */

    }

//-------------------------------------------------------------------
    void FixedUpdate()
    {
        DispExp();
    }

    //-------------------------------------------------------------------
    // 経験値表示
    void DispExp()
    {
        // 増やすとき
        if (nowDispExp < nowExp) {
            nowDispExp += dispExpVal;

            // 揃える
            if (nowDispExp > nowExp) {
                nowDispExp = nowExp;
            }
        }

        // 減らすとき
        else if (nowDispExp > nowExp) {
            nowDispExp -= dispExpVal;

            // 揃える
            if (nowDispExp < nowExp) {
                nowDispExp = nowExp;
            }
        }

        // 最大経験値よりも大きいとき
        if (nowDispExp > maxExp) {
            nowDispExp = maxExp;
        }

        // 0以下のとき
        else if (nowDispExp < 0) {
            nowDispExp = 0;
        }

        barImg.fillAmount = nowDispExp / maxExp;
    }

    // 経験値増加
    public void IncExp()
    {
        nowExp += incExp;
    }

    // 経験値減少
    public void DecExp()
    {
        nowExp -= decExp;
    }

}
