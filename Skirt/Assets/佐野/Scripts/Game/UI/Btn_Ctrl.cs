using UnityEngine;
using UnityEngine.UI;

/* ★ボタンに関するスクリプトです */
public class Btn_Ctrl : MonoBehaviour
{
    /* フラグ */

    GameObject actbtn;

    /* オブジェクト */
    GameObject pl_obj;
    GameObject gm_obj;

    /* コンポーネント取得用 */
    Pl_States pl_st;

    GameManager gm;
    SceneController sc;
    CanvasGenelator cvsGen;

//-------------------------------------------------------------------

    void Start()
    {
        /* オブジェクト検索 */
        pl_obj = GameObject.Find("Player");
        gm_obj = GameObject.Find("GameManager");

        /* コンポーネント取得 */
        pl_st = pl_obj.GetComponent<Pl_States>();

        gm = gm_obj.GetComponent<GameManager>();
        sc = gm_obj.GetComponent<SceneController>();
        cvsGen = gm_obj.transform.GetChild(0).GetComponent<CanvasGenelator>();

        /* 初期化 */
    }

//-------------------------------------------------------------------

    void Update()
    {
        Android_Proc();
    }

    //-------------------------------------------------------------------

    // androidのボタン処理
    void Android_Proc()
    {
        // 戻るボタン押したときも、ポーズボタンと同様の処理
        if (Application.platform == RuntimePlatform.Android) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Btn_Pause();
            }
        }
    }

    //-------------------------------------------------------------------
    /* ポーズ画面内のボタン */

    // ポーズボタン
    public void Btn_Pause()
    {
        // 停止中のとき
		if(gm.isPaused) {
            gm.isPaused = false;
            Time.timeScale = 1;

            cvsGen.Pause();
        }

        // 停止してないとき
		else {
            gm.isPaused = true;
            Time.timeScale = 0;

            cvsGen.UnPause();
        }
    }

    // リトライボタン
    public void Btn_Retry()
    {
        sc.SceneReload();

        Time.timeScale = 1;
	}

    // ポーズボタン/終了
    public void Btn_Quit()
    {
        gm.isPaused = false;
        Time.timeScale = 1;

        sc.SceneLoading("Title");
    }

    // --------------------------------------------------------------
    /*  */
    // ゴール時

    public void Btn_NextStage()
    {
        sc.SceneNext();
    }

    //-------------------------------------------------------------------
    // アクションボタン
    public void Btn_Action()
	{
        pl_st.Act();
	}
}
