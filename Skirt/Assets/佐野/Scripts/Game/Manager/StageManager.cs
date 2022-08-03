using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* ★〇〇に関するスクリプトです */
//-------------------------------------------------------------------
public class StageManager : MonoBehaviour
{
    /* 値 */
    [Header("ステージ関係")]
    const int stgCnt = 4;           // ステージの数
    string[] stgNames;              // 各ステージ名の配列
    public string nowStgName;       // 現在のステージ名

    public float stg_length;        // ステージの長さ
    public float stg_drag;          // ステージの空気抵抗(仮)
    public float stg_grav;          // ステージの重力

    /* フラグ */


    /* オブジェクト */
    GameObject sldr_obj;
    GameObject pl_obj;

    /* コンポーネント取得用 */
    Slider sldr;
    Player pl;


    //-------------------------------------------------------------------
    void Start()
    {
        FindObj();
        GetComp();

        Stage();

        /* 初期化 */
        stg_length = 3000;
    }

    /* オブジェクト検索 */
    void FindObj()
    {
        sldr_obj = GameObject.Find("StageSlider");
        pl_obj = GameObject.Find("Player");

    }

    /* コンポーネント取得 */
    void GetComp()
    {
        sldr = sldr_obj.GetComponent<Slider>();
        pl = pl_obj.GetComponent<Player>();


    }

    //-------------------------------------------------------------------

    void Update()
    {
        Slider();
    }

    //-------------------------------------------------------------------
    // ステージごとの処理
    void Stage()
    {
        // ステージ名
        stgNames = new string[stgCnt] { "ステージ1", "ステージ2", "ステージ3", "ボスステージ" };
    }

    void Slider()
    {
        sldr.value = 1 - (pl.gameObject.transform.position.y / stg_length);
    }
}
