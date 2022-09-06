using UnityEngine;

/* ★ボタンに関するスクリプトです */
public class Btn_Ctrl : MonoBehaviour
{
    /* コンポーネント取得用 */
    Pl_States       pl_st;

    GameManager     gm;
    SceneController sc;
    CanvasGenelator cvsGen;
    Pl_Hunger       hung;

//-------------------------------------------------------------------
    void Start()
    {
        /* オブジェクト検索 */
        GameObject pl_obj  = GameObject.Find("Player");
        GameObject gm_obj  = GameObject.Find("GameManager");

        /* コンポーネント取得 */
        pl_st   = pl_obj.GetComponent<Pl_States>();

        gm      = gm_obj.GetComponent<GameManager>();
        sc      = gm_obj.GetComponent<SceneController>();
        cvsGen  = gm_obj.transform.GetChild(0).GetComponent<CanvasGenelator>();
        hung    = pl_obj.GetComponent<Pl_Hunger>();
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
    // ゴール時
    public void Btn_NextStage()
    {
        sc.SceneNext();
    }

    //-------------------------------------------------------------------
    // アクションボタンを、状態によって変色したりする関数
    public void Btn_Action_States()
    {
        switch (pl_st.stateNum) {
            // 地上
            case Pl_States.States.landing:

                break;
            // 消化
            case Pl_States.States.digest:

                break;
        }
    }

    // アクションボタン
    public void Btn_Action()
	{
        pl_st.ActBtnProc();
	}

    //-------------------------------------------------------------------
    // 空腹にするボタン
    public void Btn_Hung()
	{
        hung.nowHung = 0;
	}
}
