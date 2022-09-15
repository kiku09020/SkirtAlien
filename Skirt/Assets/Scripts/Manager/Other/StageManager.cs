using UnityEngine;
using UnityEngine.UI;

/* ★ステージに関するスクリプトです */
public class StageManager : MonoBehaviour
{
    /* 値 */
    [Header("ステージ関係")]
    const int stgCnt = 7;           // ステージの数
    string[] stgNames = { "タイトル","ステージ1", "ステージ2", "ステージ3", "ステージボス","","デバッグ" };              // 各ステージ名の配列
    public string nowStgName;       // 現在のステージ名

    public float stg_length;        // ステージの長さ
    public float stg_grav;          // ステージの重力

    [SerializeField] Text stgTxt;

    /* コンポーネント取得用 */
    GameManager gm;
    SceneController scene;
    Pl_States st;
    Slider sldr;

    //-------------------------------------------------------------------
    void Start()
    {
        GameObject gm_obj   = GameObject.Find("GameManager");
        GameObject sldr_obj = GameObject.Find("StageSlider");
        GameObject pl_obj   = GameObject.Find("Player");

        gm    = gm_obj.GetComponent<GameManager>();
        scene = gm_obj.GetComponent<SceneController>();
        st    = pl_obj.GetComponent<Pl_States>();
        sldr  = sldr_obj.GetComponent<Slider>();

        /* 初期化 */
        nowStgName = stgNames[scene.nowSceneIndex];
    }

    //-------------------------------------------------------------------
    void FixedUpdate()
    {
        Slider();
    }

    //-------------------------------------------------------------------
    void Slider()
    {
        sldr.value = 1 - (st.gameObject.transform.position.y / stg_length);
    }
}
