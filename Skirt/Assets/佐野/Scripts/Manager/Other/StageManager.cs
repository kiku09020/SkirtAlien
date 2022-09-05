using UnityEngine;
using UnityEngine.UI;

/* ★〇〇に関するスクリプトです */
//-------------------------------------------------------------------
public class StageManager : MonoBehaviour
{
    /* 値 */
    [Header("ステージ関係")]
    const int stgCnt = 6;           // ステージの数
    string[] stgNames = { "ステージ1", "ステージ2", "ステージ3", "ステージボス","デバッグ","デバッグ東" };              // 各ステージ名の配列
    public string nowStgName;       // 現在のステージ名

    public float stg_length;        // ステージの長さ
    public float stg_grav;          // ステージの重力

    /* フラグ */


    /* オブジェクト */
    GameObject sldr_obj;
    GameObject pl_obj;
    GameObject gm_obj;

    [SerializeField] Text stgTxt;

    /* コンポーネント取得用 */
    Slider sldr;
    Player pl;
    GameManager gm;
    SceneController scene;


    //-------------------------------------------------------------------
    void Start()
    {
        FindObj();
        GetComp();

        /* 初期化 */
        nowStgName = stgNames[scene.nowSceneIndex - 2];
    }

    /* オブジェクト検索 */
    void FindObj()
    {
        sldr_obj = GameObject.Find("StageSlider");
        pl_obj = GameObject.Find("Player");
        gm_obj = GameObject.Find("GameManager");
    }

    /* コンポーネント取得 */
    void GetComp()
    {
        sldr = sldr_obj.GetComponent<Slider>();
        pl = pl_obj.GetComponent<Player>();
        gm = gm_obj.GetComponent<GameManager>();
        scene = gm_obj.GetComponent<SceneController>();
    }

    //-------------------------------------------------------------------
    void Update()
    {
        Slider();
    }

    //-------------------------------------------------------------------
    void Slider()
    {
        sldr.value = 1 - (pl.gameObject.transform.position.y / stg_length);
    }
}
