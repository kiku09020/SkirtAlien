using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* ★満腹度に関するスクリプトです */
//-------------------------------------------------------------------
public class Pl_Hunger : MonoBehaviour
{
    [Header("満腹度")]
    [SerializeField] int   hungLv;   // 現在の満腹度レベル
    [SerializeField] float nowHung;     // 満腹度の数値
    [SerializeField] float decVal;      // 満腹度減らす量
    float hungMax = 100;                // 最大満腹度

    [Header("スカートサイズ")]
    float size_one = 3;   //lv1
    float size_two = 4;   //lv2
    float size_thr = 5;   //lv3
    public float skirtSize;

    [Header("フラグ")]
    [SerializeField] bool decFlg;       // 減ってるとき
    [SerializeField] bool incFlg;       // 増えてるとき

    /* オブジェクト */
    GameObject hungbar_obj;
    GameObject pl_obj;

    /* コンポーネント取得用 */
    Image hungImage;
    Player pl;

//-------------------------------------------------------------------

    void Start()
    {
        /* オブジェクト検索 */
        hungbar_obj = GameObject.Find("HungBar");
        pl_obj = GameObject.Find("Player");

        /* コンポーネント取得 */
        hungImage = hungbar_obj.GetComponent<Image>();
        pl = pl_obj.GetComponent<Player>();

        /* 初期化 */
        nowHung = hungMax;
        hungLv = 1;
    }

//-------------------------------------------------------------------

    void Update()
    {
        // 満腹度バー更新
        hungImage.fillAmount = nowHung / hungMax;
    }

//-------------------------------------------------------------------

    // レベル変更時の処理
    void ChangeLevel()
	{
        // 満腹度レベルごとの処理
        switch(hungLv)
        {
            
            case 1:     // レベル1
                skirtSize = size_one;
            break;

            // ----------------------------

            case 2:     // レベル2
                skirtSize = size_two;
            break;

            // ----------------------------

            case 3:     // レベル3
                skirtSize = size_thr;
            break;
        }
	}
}
