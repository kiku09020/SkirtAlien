using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Btn_Ctrl : MonoBehaviour
{
    /* 値 */


    /* フラグ */
    public bool isPause;

    /* オブジェクト */
    public GameObject stick;

    [SerializeField] GameObject pauseUI_pref;   // Canvasのプレハブ
                     GameObject pauseUI_inst;   // Canvasのインスタンス

    GameObject pl_obj;

    /* コンポーネント取得用 */
    GameManager gm;
    Player pl;

//-------------------------------------------------------------------

    void Start()
    {
        /* オブジェクト検索 */
        pl_obj = GameObject.Find("Player");

        /* コンポーネント取得 */
        pl = pl_obj.GetComponent<Player>();
        gm = GetComponent<GameManager>();

        /* 初期化 */
        isPause = false;
        pauseUI_inst = Instantiate(pauseUI_pref, Vector2.zero, Quaternion.identity);
        pauseUI_inst.SetActive(false);
    }

//-------------------------------------------------------------------

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Btn_Pause();
            }
        }
    }

    //-------------------------------------------------------------------

    // ポーズボタン
    public void Btn_Pause()
    {
        // 停止中のとき
		if(isPause) {
            isPause = false;
            Time.timeScale = 1;

            pauseUI_inst.SetActive(false);
            stick.SetActive(true);
        }

        // 停止してないとき
		else {
            isPause = true;
            Time.timeScale = 0;

            pauseUI_inst.SetActive(true);
            stick.SetActive(false);
        }
    }

    public void Btn_Retry()
    {
        SceneManager.LoadSceneAsync(GameManager.scene_name);

        Time.timeScale = 1;
	}

    // ポーズボタン/終了
    public void Btn_Quit()
    {
        isPause = false;
        Time.timeScale = 1;

        SceneManager.LoadScene("Title");
    }
}
