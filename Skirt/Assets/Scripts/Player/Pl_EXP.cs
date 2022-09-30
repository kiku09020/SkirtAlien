using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* ★経験値に関するスクリプトです */
public class Pl_EXP : MonoBehaviour
{
    /* 値 */
    [Header("レベル")]
    [SerializeField] int maxLv;     // 最大レベル
    public int nowLv;               // 現在のレベル

    [Header("経験値")]
    [SerializeField] float lvExp;   // レベルごとに必要な経験値
    [SerializeField] float incExp;  // 経験値の増加量
    [SerializeField] float decExp;  // 経験値の減少量

    /* 合計・最大 */
    float nowExp;                   // 現在の経験値
    float maxExp;                   // そのレベルでの最大経験値
    float minExp;                   // そのレベルでの最小経験値
    float prevMinExp;               // 前のレベルの最小経験値

    float nowDispExp;               // 現在の表示経験値
    float maxDispExp;               // 最大の表示経験値

    enum ChangeType {
        lvUp,
        lvDn
    }

    /* コンポーネント取得用 */
    Image barImg;   // バーの画像
    Text  lvTxt;    // レベルのテキスト

    AudioManager aud;
    PlayerAnim anim;

    //-------------------------------------------------------------------
    void Start()
    {
        /* オブジェクト取得 */
        GameObject expObj = GameObject.Find("EXP");
        GameObject barobj = expObj.transform.Find("EXPBar").gameObject;
        GameObject txtObj = expObj.transform.Find("LvTxt").gameObject;

        GameObject plObj = GameObject.Find("Player");
        GameObject audObj = GameObject.Find("AudioManager");

        /* コンポーネント取得 */
        barImg = barobj.GetComponent<Image>();
        lvTxt = txtObj.GetComponent<Text>();

        aud = audObj.GetComponent<AudioManager>();
        anim = plObj.GetComponent<PlayerAnim>();

        /* 初期化 */
        nowLv = 1;
        ChangeLimitExp(ChangeType.lvUp);
    }

    //-------------------------------------------------------------------
    void FixedUpdate()
    {
        DispExp();

        print("min:" + minExp);
        print("max:" + maxExp);
        print("dispMax:" + maxDispExp);
        print("now:" + nowExp);
        print("lv:" + nowLv+"\n-----------");
    }

    //-------------------------------------------------------------------
    // 経験値表示
    void DispExp()
    {
        float dispExpVal;
        float targDispExp = nowExp - minExp;

        // 増やすとき
        if (nowDispExp < targDispExp) {
            dispExpVal = (targDispExp - nowDispExp) / 20;       // 一度に増やす量
            nowDispExp += dispExpVal;                           // 増やす

            // レベル最大で最大値より大きい場合、揃える
            if (nowDispExp > maxExp && nowLv == maxLv) {
                nowDispExp = maxDispExp;
            }
        }

        // 減らすとき
        else if (nowDispExp > targDispExp) {
            dispExpVal = (nowDispExp - targDispExp ) / 20;
            nowDispExp -= dispExpVal;

            // 0より大きくなったらそろえる
            if (nowDispExp < minExp && nowLv == 1) {
                nowDispExp = minExp;
            }
        }

        barImg.fillAmount = nowDispExp / maxDispExp;        // 経験値バー表示
        lvTxt.text = "Lv." + nowLv.ToString();              // レベル表示
    }

    //-------------------------------------------------------------------
    // 経験値増加
    public void IncExp(int mag)
    {
        float exp;

        // 倍率1の場合、そのまま加算
        if (mag == 1) {
            exp = incExp;
        }

        // 倍率1以上の場合、経験値倍率を加算
        else {
            float expMag = 1 + (0.5f * mag / 2);
            exp = incExp * expMag;
        }

        // 経験値加算
        nowExp += exp;

        // 最大経験値を超えたとき(レベルアップ)
        if (nowExp > maxExp) {
            if (nowLv < maxLv) {
                nowLv++;
                anim.LvUp(GetLvSize());
                StartCoroutine(LvUp());
                ChangeLimitExp(ChangeType.lvUp);
                nowDispExp = 0;

                aud.PlaySE(AudLists.SETypeList.ui, (int)AudLists.SEList_UI.lvUp);
            }

            // Lv3かつ経験値最大
            else {
                nowExp = maxExp;
                nowDispExp = maxDispExp;
            }
        }
    }

    // 経験値減少
    public void DecExp()
    {
        nowExp -= decExp;
        // 最小経験値を超えたとき
        if (nowExp < minExp ) {
            if (nowLv > 1) {
                nowLv--;
                ChangeLimitExp(ChangeType.lvDn);
                nowExp = maxExp;        // レベル下がった時の最大値に指定する

                aud.PlaySE(AudLists.SETypeList.ui, (int)AudLists.SEList_UI.lvDn);
            }
            else {
                nowExp = minExp;
            }
        }
    }
    //-------------------------------------------------------------------
    IEnumerator LvUp()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1;
    }


    //-------------------------------------------------------------------
    // 経験値の最小値・最大値の変更
    void ChangeLimitExp(ChangeType type)
    {
        switch (type) {
            case ChangeType.lvUp:
                prevMinExp = minExp;
                minExp = maxExp;                                // 最小値経験値変更
                maxExp = lvExp * Mathf.Pow(nowLv, 2) + lvExp;   // 最大経験値変更
                maxDispExp = maxExp - nowExp;
                break;

            case ChangeType.lvDn:
                maxExp = minExp;
                minExp = prevMinExp;
                break;
        }
    }

    //-------------------------------------------------------------------
    // レベルごとの横幅 を渡す
    public Vector2 GetLvSize()
    {
        float x = nowLv * 0.75f + 0.25f;        // x成分
        float y = nowLv * 0.50f;                // y成分

        return new Vector2(x, y);
    }

    // レベルごとの空気抵抗 を渡す
    public float GetDrag()
    {
        return nowLv * 0.3f + 0.5f;
    }

    // 一度に捕食できる敵の数 を渡す
    public int GetCanEatCnt()
    {
        return 1 + (nowLv - 1) * 2;
    }
}
