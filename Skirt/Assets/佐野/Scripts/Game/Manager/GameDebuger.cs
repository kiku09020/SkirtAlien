using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDebuger : MonoBehaviour
{
    /* 値 */


    /* フラグ */


    /* オブジェクト */
    [SerializeField] Text txt_dbg_cam;

    /* コンポーネント取得用 */
    Player pl;
    Pl_Camera cam;


//-------------------------------------------------------------------

    void Start()
    {
        /* コンポーネント取得 */
        // プレイヤー
        GameObject pl_obj = GameObject.Find("Player");
        pl = pl_obj.GetComponent<Player>();

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
	}
}
