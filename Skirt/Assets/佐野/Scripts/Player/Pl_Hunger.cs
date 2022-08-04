using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* ★満腹度に関するスクリプトです */
//-------------------------------------------------------------------
public class Pl_Hunger : MonoBehaviour {
    [Header("満腹度")]
    [SerializeField] float nowHung;                 // 満腹度の数値
                     float hungMax = 100;           // 最大満腹度

    [SerializeField] float hungValDec_State;           // 一度に減らす量(ふわふわ、急降下中)
    [SerializeField] float hungValDec_Atk;          // 一度に減らす量(捕食時)
    [SerializeField] float hungIncVal;              // 一度に増やす量

    [Header("フラグ")]
    [SerializeField] bool decFlg;       // 減ってるとき
    [SerializeField] bool incFlg;       // 増えてるとき

    /* オブジェクト */
    GameObject hungbar_obj;
    GameObject pl_obj;

    /* コンポーネント取得用 */
    Image hungImage;
    Pl_HP hp;

    //-------------------------------------------------------------------

    void Start()
    {
        /* オブジェクト検索 */
        hungbar_obj = GameObject.Find("HungBar");
        pl_obj = GameObject.Find("Player");

        /* コンポーネント取得 */
        hungImage = hungbar_obj.GetComponent<Image>();

        hp = pl_obj.GetComponent<Pl_HP>();

        /* 初期化 */
        nowHung = hungMax;
    }

    //-------------------------------------------------------------------

    void FixedUpdate()
    {
        HungDisp();
    }

    //-------------------------------------------------------------------
    // 満腹度バー更新
    void HungDisp()
    {
        hungImage.fillAmount = nowHung / hungMax;
    }

    // 減らす(状態)
    public  void HungDec_State()
    {
        nowHung -= hungValDec_State;
    }

    // 減らす(捕食)
    public void HungDec_Atk()
    {
        nowHung -= hungValDec_Atk;
    }

    // 増やす
    public void HungInc()
    {
        nowHung += hungIncVal;

        // 最大値よりも大きくなったら、戻す
        if (nowHung > hungMax) {
            nowHung = hungMax;
        }
    }
}
