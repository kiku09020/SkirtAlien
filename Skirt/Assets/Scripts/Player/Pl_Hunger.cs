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

    [Header("表示")]
    [SerializeField] float cautHung;        // 警告する満腹度
    float flashTimer;


    [Header("フラグ")]
    public bool hungFlg;                    // 空腹時の
 
    /* コンポーネント取得用 */
    Image hungImage;

    //-------------------------------------------------------------------
    void Start()
    {
        /* オブジェクト検索 */
        GameObject hungbar_obj = GameObject.Find("HungBar");

        /* コンポーネント取得 */
        hungImage = hungbar_obj.GetComponent<Image>();

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

        var imgClr = hungImage.color;
        if (nowHung <= cautHung) {
            var alpha = Mathf.Cos(2 * Mathf.PI * (flashTimer / 0.3f));
            hungImage.color = new Color(imgClr.r, imgClr.g, imgClr.b, alpha);

            flashTimer += Time.deltaTime;
        }

        else {
            flashTimer = 0;
            hungImage.color = new Color(imgClr.r, imgClr.g, imgClr.b, 1);
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
        float addHung;

        // コンボ倍率が1のとき
        if (mag == 1) {
            addHung = hungIncVal;
        }

        // それ以降のコンボ倍率
        else {
            float hungMag = 1 + 0.5f * (mag / 2);      // 満腹度倍率
            addHung = hungIncVal * hungMag;
        }

        nowHung += addHung;            // 増やす
        Debug.Log(addHung);

        // 最大値よりも大きくなったら、戻す
        if (nowHung > hungMax) {
            nowHung = hungMax;
        }
    }
}
