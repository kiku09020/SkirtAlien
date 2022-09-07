using System.Collections;
using UnityEngine;

/* ★ボタンに関するスクリプトです */
public class Btn_Ctrl : MonoBehaviour
{
    /* コンポーネント取得用 */
    Pl_States       pl_st;

    GameManager     gm;
    AudioManager    aud;
    SceneController sc;
    CanvasGenelator cvsGen;
    Pl_Hunger       hung;

//-------------------------------------------------------------------
    void Start()
    {
        /* オブジェクト検索 */
        GameObject pl_obj  = GameObject.Find("Player");
        GameObject gm_obj  = GameObject.Find("GameManager");
        GameObject aud_obj = gm_obj.transform.Find("AudioManager").gameObject;

        /* コンポーネント取得 */
        pl_st   = pl_obj.GetComponent<Pl_States>();
        aud     = aud_obj.GetComponent<AudioManager>();

        gm      = gm_obj.GetComponent<GameManager>();
        sc      = gm_obj.GetComponent<SceneController>();
        cvsGen  = gm_obj.transform.GetChild(0).GetComponent<CanvasGenelator>();
        hung    = pl_obj.GetComponent<Pl_Hunger>();
    }

    //-------------------------------------------------------------------
    /* ポーズ画面内のボタン */
    // ポーズボタン/Continue
    public void Btn_Pause()
    {
        // Continue
		if(gm.isPaused) {
            gm.isPaused = false;
            Time.timeScale = 1;
            cvsGen.UnPause();

            aud.PlaySE(AudLists.SETypeList.ui, (int)AudLists.SEList_UI.decision);
        }

        // Pause
		else {
            gm.isPaused = true;
            Time.timeScale = 0;
            cvsGen.Pause();

            aud.PlaySE(AudLists.SETypeList.ui, (int)AudLists.SEList_UI.decision);
        }
    }

    // リトライボタン
    public void Btn_Retry()
    {
        gm.retry = true;
        cvsGen.Caution();

        aud.PlaySE(AudLists.SETypeList.ui, (int)AudLists.SEList_UI.push);
	}

    // ポーズボタン/終了
    public void Btn_Quit()
    {
        gm.quit = true;
        cvsGen.Caution();

        aud.PlaySE(AudLists.SETypeList.ui, (int)AudLists.SEList_UI.push);
    }

    //-------------------------------------------
    // 警告Yes
    public void Btn_Caution_Yes()
    {
        // リトライ
        if (gm.retry) {
            aud.PlaySE(AudLists.SETypeList.ui, (int)AudLists.SEList_UI.decision);

            StartCoroutine(Wait_Retry());
        }

        // 終了
        else if (gm.quit) {
            aud.PlaySE(AudLists.SETypeList.ui, (int)AudLists.SEList_UI.decision);

            StartCoroutine(Wait_Quit());
        }
    }

    IEnumerator Wait_Retry()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        Time.timeScale = 1;
        sc.SceneReload();
    }

    IEnumerator Wait_Quit()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        Time.timeScale = 1;
        sc.SceneLoading("Title");
    }

    // 警告No
    public void Btn_Caution_No()
    {
        gm.retry = false;      gm.quit = false;
        cvsGen.UnCaution();
        
        aud.PlaySE(AudLists.SETypeList.ui, (int)AudLists.SEList_UI.cancel);
    }

    // --------------------------------------------------------------
    // ゴール時
    public void Btn_NextStage()
    {
        sc.SceneNext();
            aud.PlaySE(AudLists.SETypeList.ui, (int)AudLists.SEList_UI.decision);
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
