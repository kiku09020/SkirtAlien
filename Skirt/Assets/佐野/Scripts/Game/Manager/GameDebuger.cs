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
    [SerializeField] Text txt_dbg_cam;
    [SerializeField] Text txt_stgName;
    GameObject pl_obj;

    /* コンポーネント取得用 */
    Player pl;
    Pl_Camera cam;
    GameManager gm;


//-------------------------------------------------------------------

    void Start()
    {
        /* コンポーネント取得 */
        // プレイヤー
        pl_obj = GameObject.Find("Player");
        pl = pl_obj.GetComponent<Player>();
        gm = GetComponent<GameManager>();

        // カメラ
        GameObject cam_obj = GameObject.Find("PlayerCamera");
        cam = cam_obj.GetComponent<Pl_Camera>();
        

        /* 初期化 */
        
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
        txt_dbg_cam.text = "edge = " + cam.scrn_EdgeX.ToString();

        txt_stgName.text = gm.nowSceneName;
	}
}
