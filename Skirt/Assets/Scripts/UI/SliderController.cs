using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour {
    /* 値 */
    bool isOnPlayer;

    [Header("透明度")]
    [SerializeField] float alphaAmount;     // 増減させる量
    [SerializeField] float maxAlpha;        // 最大
    [SerializeField] float minAlpha;        // 最小の透明度
    float nowAlpha;                         // 現在の透明度

    GameObject plObj;

    /* コンポーネント取得用 */
    Slider sldr;
    CanvasGroup canvas;
    RectTransform rect;

    StageManager stg;

    //-------------------------------------------------------------------
    void Start()
    {
        /* オブジェクト取得 */
        plObj = GameObject.FindGameObjectWithTag("Player");
        GameObject gmObj = GameObject.Find("GameManager");

        /* コンポーネント取得 */
        sldr = GetComponent<Slider>();
        canvas = GetComponent<CanvasGroup>();
        rect = GetComponent<RectTransform>();

        stg = gmObj.GetComponent<StageManager>();

        /* 初期化 */

    }

    //-------------------------------------------------------------------
    void FixedUpdate()
    {
        var plPos = plObj.transform.position;
        sldr.value = 1 - (plPos.y / stg.stg_length);

        CheckPlayer();
        ChangeAlpha();
    }

    //-------------------------------------------------------------------
    // プレイヤーのスクリーン座標を調べて、重なってるか判定する
    void CheckPlayer()
    {
        var plPos = Camera.main.WorldToScreenPoint(plObj.transform.position);       // プレイヤーのスクリーン座標
        var sldrPos = rect.transform.position;                                      // スライダー 
        var sldrSize = rect.sizeDelta;                                              // スライダーのサイズ

        // スライダーの位置-スライダーの幅 < プレイヤーの位置
        if (sldrPos.x - sldrSize.x < plPos.x) {
            isOnPlayer = true;
        }

        else {
            isOnPlayer = false;
        }
    }

    void ChangeAlpha()
    {
        // 重なってる
        if (isOnPlayer) {
            if (nowAlpha > minAlpha) {
                nowAlpha -= alphaAmount;        // 透明度減らす
            }
        }

        // 重なってない
        else {
            if (nowAlpha < maxAlpha) {
                nowAlpha += alphaAmount;        // 透明度元に戻す
            }
        }

        canvas.alpha = nowAlpha;                // CanvasGroupの透明度に適用
    }
}
