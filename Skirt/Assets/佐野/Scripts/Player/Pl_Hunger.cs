using UnityEngine;
using UnityEngine.UI;

/* ★満腹度に関するスクリプトです */
//-------------------------------------------------------------------
public class Pl_Hunger : MonoBehaviour {
    [Header("満腹度")]
    public           float nowHung;                 // 満腹度の数値
                     float hungMax = 100;           // 最大満腹度

    [SerializeField] float hungValDec_State;        // 一度に減らす量(ふわふわ、急降下中)
    [SerializeField] float hungValDec_Atk;          // 一度に減らす量(捕食時)
    [SerializeField] float hungIncVal;              // 一度に増やす量

    [Header("フラグ")]
    [SerializeField] bool decFlg;                   // 減ってるとき
    [SerializeField] bool incFlg;                   // 増えてるとき
    public bool hungFlg;                            // 空腹時の
    bool onceFlg;

    [Header("その他")]
    public int eatCombo;                  // 消化した敵の数
    [SerializeField] Color hungColor;

    // 消化数の扱い
    public enum ComboEnum {
        inc,        // 増やす
        dec,        // 減らす
        reset,      // 0にする
    }

    /* オブジェクト */
    GameObject hungbar_obj;

    /* コンポーネント取得用 */
    Image hungImage;
    SpriteRenderer sr;

    Pl_HP hp;
    Pl_Action act;
    Pl_States st;

    //-------------------------------------------------------------------
    void Start()
    {
        /* オブジェクト検索 */
        hungbar_obj = GameObject.Find("HungBar");

        /* コンポーネント取得 */
        hungImage = hungbar_obj.GetComponent<Image>();
        sr = GetComponent<SpriteRenderer>();

        act = GetComponent<Pl_Action>();
        hp = GetComponent<Pl_HP>();
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
            // 一度のみ実行
            if (!onceFlg) {
                onceFlg = true;
            }

            transform.localScale = Vector2.one;         // 大きさ
            sr.color = hungColor;                       // 色変更
        }

        else {
            onceFlg = false;                            // onceフラグ降ろす

            st.stateNum = Pl_States.States.normal;      // 通常状態にする
        }
    }

    //-------------------------------------------------------------------
    // 減らす(状態)
    public void HungDec_State()
    {
        if (nowHung > 0) {
            nowHung -= hungValDec_State;
        }
    }

    // 減らす(捕食)
    public void HungDec_Atk()
    {
        if (nowHung > 0) {
            nowHung -= hungValDec_Atk;
        }
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

    // 消化した敵の数を増減する
    public void ComboSetter(ComboEnum setType)
    {
        switch (setType) {
            // 増やす
            case ComboEnum.inc:
                eatCombo++;
                break;

            //減らす
            case ComboEnum.dec:
                eatCombo--;
                break;

            // 0に戻す
            case ComboEnum.reset:
                eatCombo = 0;
                break;
        }
    }
}
