using System.Collections;
using UnityEngine;

/* ★ボタンに関するスクリプトです */
public class Btn_Ctrl : MonoBehaviour
{
    /* コンポーネント取得用 */
    Pl_States       st;
    Pl_Action       act;
    Pl_Hunger       hung;

    GameManager     gm;
    AudioManager    aud;
    SceneController sc;
    CanvasGenelator cvsGen;
    Pause           pause;
    ScoreManager    score;

    //-------------------------------------------------------------------
    void Start()
    {
        /* オブジェクト検索 */
        GameObject pl_obj  = GameObject.Find("Player");
        GameObject gm_obj  = GameObject.Find("GameManager");
        GameObject ui_Obj  = GameObject.Find("UIManager");
        GameObject aud_obj = gm_obj.transform.Find("AudioManager").gameObject;

        /* コンポーネント取得 */
        gm      = gm_obj.GetComponent<GameManager>();
        sc      = gm_obj.GetComponent<SceneController>();
        score   = gm_obj.GetComponent<ScoreManager>();
        cvsGen  = ui_Obj.GetComponent<CanvasGenelator>();
        pause   = ui_Obj.GetComponent<Pause>();
        aud     = aud_obj.GetComponent<AudioManager>();

        st      = pl_obj.GetComponent<Pl_States>();
        act     = pl_obj.GetComponent<Pl_Action>();
        hung    = pl_obj.GetComponent<Pl_Hunger>();
    }

    //-------------------------------------------------------------------
    /* ポーズ画面内のボタン */
    // ●ポーズボタン/Continue
    public void Btn_Pause()
    {
        // Continue
		if(gm.isPaused) {
            gm.isPaused = false;
            Time.timeScale = 1;     // 時間戻す
            cvsGen.UnPause();       // Canvas非表示

            aud.PlaySE(AudLists.SETypeList.ui, (int)AudLists.SEList_UI.cancel);     // 効果音
            aud.PauseAudio(false);
        }

        // Pause
		else {
            gm.isPaused = true;
            Time.timeScale = 0;     // 時間停止
            cvsGen.Pause();         // Canvas表示

            aud.PlaySE(AudLists.SETypeList.ui, (int)AudLists.SEList_UI.decision);   // 効果音
            aud.PauseAudio(true);
        }
    }

    // ●リトライボタン
    public void Btn_Retry(bool isPause)
    {
        if (isPause) {
            pause.retry = true;
            cvsGen.Caution();           // 警告表示
            pause.SetText();            // テキスト設定
        }

        else {
            StartCoroutine(Wait_Retry());
        }

        aud.PlaySE(AudLists.SETypeList.ui, (int)AudLists.SEList_UI.push);       // 効果音
	}

    // ●ポーズボタン/終了
    public void Btn_Quit(bool isPause)
    {
        if (isPause) {
            cvsGen.Caution();           // 警告表示
            pause.quit = true;
            pause.SetText();            // テキスト設定
        }

        else {
            StartCoroutine(Wait_Quit());
        }

        aud.PlaySE(AudLists.SETypeList.ui, (int)AudLists.SEList_UI.push);       // 効果音
    }

    //-------------------------------------------
    // ●警告Yes
    public void Btn_Caution_Yes()
    {
        // 効果音(共通)
        aud.PlaySE(AudLists.SETypeList.ui, (int)AudLists.SEList_UI.decision);

        // リトライ
        if (pause.retry) {
            StartCoroutine(Wait_Retry());
        }

        // 終了
        else if (pause.quit) {
            StartCoroutine(Wait_Quit());
        }
    }

    // リトライ時のコルーチン
    IEnumerator Wait_Retry()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        Time.timeScale = 1;
        score.LoadScore();          // ステージ開始時のスコアにする
        sc.SceneReload();
    }

    // 終了時のコルーチン
    IEnumerator Wait_Quit()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        Time.timeScale = 1;
        score.ResetScore();         // スコアリセットする
        sc.SceneLoading("Title");
    }

    // ●警告No
    public void Btn_Caution_No()
    {
        pause.retry = false;      pause.quit = false;       // リトライ・終了フラグ降ろす
        cvsGen.UnCaution();                                 // 警告非表示

        aud.PlaySE(AudLists.SETypeList.ui, (int)AudLists.SEList_UI.cancel);     // 効果音
    }

    // --------------------------------------------------------------
    // ●ゴール時
    public void Btn_NextStage()
    {
        aud.PlaySE(AudLists.SETypeList.ui, (int)AudLists.SEList_UI.decision);
        sc.SceneNext();
    }

    //-------------------------------------------------------------------
    // アクションボタンを、状態によって変色したりする関数
    public void Btn_Action_States()
    {
        switch (st.nowState) {
            // 消化
            case Pl_States.States.digest:

                break;
        }
    }

    /* アクションボタン */
    // 押した瞬間
    public void Btn_Act_Down()
	{
        st.ActBtnProc();
	}

    // 押してる最中
    public void Btn_Act_Downing()
    {
        st.ActBtn_Downing();
    }

    // 離した瞬間
    public void Btn_Act_Up()
    {
        st.ActBtn_Up();
    }

    //-------------------------------------------------------------------
    /* Debug */

    // 空腹にするボタン
    public void Btn_Hung()
	{
        hung.nowHung = 0;
	}

    // ダメージ
    public void Btn_Dmg()
    {
        act.Damage();
    }
}
