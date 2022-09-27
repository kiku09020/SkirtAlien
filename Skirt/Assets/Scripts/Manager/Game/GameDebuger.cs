using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* ★ログ表示などのデバッグ関連の処理をするスクリプトです
 * ・ログ表示
 * ・テキスト表示
 * ・シーン再読み込みなどのキー操作
 */
public class GameDebuger : MonoBehaviour
{
    /* フラグ */
    [SerializeField] bool isInfinity;       // ステージ長さ無限

    /* オブジェクト */
    GameObject pl_obj;

    Text txt_dbg_cam;
    Text txt_fallSpd;
    Text txt_eatenCnt;

    /* コンポーネント取得用 */
    GameManager  gm;
    AudioSource  audsrc;
    ComboManager combo;

    Pl_Action act;
    Pl_States st;
    PlayerCamera cam;


//-------------------------------------------------------------------

    void Start()
    {
        /* コンポーネント取得 */
        GameObject gmObj  = GameObject.Find("GameManager");
        GameObject audObj = gmObj.transform.Find("AudioManager").gameObject;
        pl_obj  = GameObject.Find("Player");

        gm      = gmObj.GetComponent<GameManager>();
        combo   = gmObj.GetComponent<ComboManager>();
        audsrc  = audObj.GetComponent<AudioSource>();
        
        act     = pl_obj.GetComponent<Pl_Action>();
        st      = pl_obj.GetComponent<Pl_States>();

        // カメラ
        GameObject cam_obj = GameObject.Find("PlayerCamera");
        cam = cam_obj.GetComponent<PlayerCamera>();

        // テキスト
        txt_dbg_cam  = GameObject.Find("DBG_edge").GetComponent<Text>();
        txt_fallSpd  = GameObject.Find("fallspd").GetComponent<Text>();
        txt_eatenCnt = GameObject.Find("eatenCnt").GetComponent<Text>();

        /* 初期化 */
        audsrc.enabled = false;     // BGM無効化
    }

//-------------------------------------------------------------------

    void Update()
    {
        Debug_Key();
        Debug_Log();
        Debug_Text();

        // 無限
        if (isInfinity) {
            if(pl_obj.transform.position.y < 50) {
                pl_obj.transform.Translate(pl_obj.transform.position.x, 1500, 0);
            }
        }
    }

//-------------------------------------------------------------------

    // キー操作
    void Debug_Key()
	{
        // Rでリロード
        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Gでゴール地点まで移動
        if(Input.GetKeyDown(KeyCode.G)) {
            pl_obj.transform.position = new Vector2(0, 30);
        }
    }

    // ログ
    void Debug_Log()
	{
        Debug.Log("<b><color=yellow>" + st.nowState + "</color></b>");
    }

    // 画面上に表示するテキスト
    void Debug_Text()
	{
        txt_dbg_cam.text  = "edge = " + cam.camSize.ToString();
        txt_fallSpd.text  = "fallSpd = " + act.GetSpd().ToString();
        txt_eatenCnt.text = combo.cmbCnt.ToString();
	}
}
