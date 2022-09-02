using System.Collections;
using System.Collections.Generic;
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
    /* 値 */


    /* フラグ */
    [SerializeField] bool isInfinity;       // ステージ長さ無限

    /* オブジェクト */
    GameObject gmObj;
    GameObject audObj;

    Text txt_dbg_cam;
    Text txt_fallSpd;
    Text txt_eatenCnt;
    GameObject pl_obj;


    /* コンポーネント取得用 */
    GameManager gm;
    AudioManager aud;
    AudioSource audsrc;

    Player pl;
    Pl_Action act;
    Pl_States st;
    Pl_Camera cam;
    Pl_Hunger hung;
    ComboManager combo;


//-------------------------------------------------------------------

    void Start()
    {
        /* コンポーネント取得 */
        pl_obj  = GameObject.Find("Player");

        gmObj   = GameObject.Find("GameManager");
        audObj  = gmObj.transform.Find("AudioManager").gameObject;
        gm      = gmObj.GetComponent<GameManager>();
        aud     = audObj.GetComponent<AudioManager>();
        audsrc = audObj.GetComponent<AudioSource>();
        combo = gmObj.GetComponent<ComboManager>();
        

        pl      = pl_obj.GetComponent<Player>();
        act=pl_obj.GetComponent<Pl_Action>();
        st   = pl_obj.GetComponent<Pl_States>();
        hung    = pl_obj.GetComponent<Pl_Hunger>();

        // カメラ
        GameObject cam_obj = GameObject.Find("PlayerCamera");
        cam = cam_obj.GetComponent<Pl_Camera>();

        txt_dbg_cam = GameObject.Find("DBG_edge").GetComponent<Text>();
        txt_fallSpd=GameObject.Find("fallspd").GetComponent<Text>();
        txt_eatenCnt = GameObject.Find("eatenCnt").GetComponent<Text>();

        /* 初期化 */
        audsrc.enabled = false;
    }

//-------------------------------------------------------------------

    void Update()
    {
        Debug_Key();
        Debug_Log();
        Debug_Text();

        if (isInfinity)
        {
            if(pl_obj.transform.position.y < 1000)
            {
                pl_obj.transform.Translate(pl_obj.transform.position.x,1500,0) ;
            }
            
        }
    }

//-------------------------------------------------------------------

    // キー操作
    void Debug_Key()
	{
        // Rでリロード
        if(Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Gでゴール地点まで移動
        if(Input.GetKeyDown(KeyCode.G)) {
            pl.transform.position = new Vector2(0, 30);
        }
    }

    // ログ
    void Debug_Log()
	{
        
	}

    // 画面上に表示するテキスト
    void Debug_Text()
	{
        txt_dbg_cam.text = "edge = " + cam.camSize.ToString();
        txt_fallSpd.text="fallSpd = "+act.GetSpd().ToString();
        txt_eatenCnt.text = combo.cmbCnt.ToString();
	}
}
