using UnityEngine;
using UnityEngine.UI;

/* ★満腹度に関するスクリプトです */
public class Pl_Hunger : MonoBehaviour {
    [Header("満腹度")]
    public float nowHung;                   // 満腹度の数値
           float hungMax = 100;             // 最大満腹度

    [SerializeField] float hungValDecNml;   // 一度に減らす量(通常時)
    [SerializeField] float hungValDecAtk;   // 一度に減らす量(捕食時)
    [SerializeField] float hungIncVal;      // 一度に増やす量

    [Header("フラグ")]
    public bool hungFlg;                    // 空腹時の

    [Header("その他")]
    [SerializeField] Color hungColor;

    /* コンポーネント取得用 */
    Image hungImage;
    SpriteRenderer sr;

    Pl_States st;

    //-------------------------------------------------------------------
    void Start()
    {
        /* オブジェクト検索 */
        GameObject hungbar_obj = GameObject.Find("HungBar");

        /* コンポーネント取得 */
        hungImage = hungbar_obj.GetComponent<Image>();
        sr = GetComponent<SpriteRenderer>();
        st = GetComponent<Pl_States>();

        /* 初期化 */
        nowHung = hungMax;
    }

    //-------------------------------------------------------------------
    void FixedUpdate()
    {
        HungDisp();
        Hungry();
    }

    //-------------------------------------------------------------------
    // 満腹度バー更新
    void HungDisp()
    {
        hungImage.fillAmount = nowHung / hungMax;
    }

    // 空腹
    void Hungry()
    {
        // 0以下になったら空腹
        if (nowHung <= 0) {
            hungFlg = true;
        }
        else {
            hungFlg = false;
        }
    }

    //-------------------------------------------------------------------
    // 空腹時の処理
    public void HungState()
    {
        if (hungFlg) {
            transform.localScale = Vector2.one;         // 大きさ
            sr.color = hungColor;                       // 色変更
        }

        else {
            st.stateNum = Pl_States.States.normal;      // 通常状態にする
        }
    }

    //-------------------------------------------------------------------
    // 減らす(通常状態)
    public void HungDec_State()
    {
        if (nowHung > 0) {
            nowHung -= hungValDecNml;
        }
    }

    // 減らす(捕食)
    public void HungDec_Atk()
    {
        if (nowHung > 0) {
            nowHung -= hungValDecAtk;
        }
    }

    // 増やす
    public void HungInc(int mag)
    {
        if (mag == 1) {
            nowHung += hungIncVal;
        }

        else {
            nowHung += (hungIncVal * mag) * 0.75f;
        }

        // 最大値よりも大きくなったら、戻す
        if (nowHung > hungMax) {
            nowHung = hungMax;
        }
    }
}
