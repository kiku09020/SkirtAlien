using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* ★ボタンに関するスクリプトです */
public class Btn_Ctrl : MonoBehaviour
{
    /* フラグ */

    /* オブジェクト */
    GameObject pl_obj;
    GameObject gm_obj;

    /* コンポーネント取得用 */
    Pl_States pl_st;

    CanvasGenelator cvsGen;
    GameManager gm;

//-------------------------------------------------------------------

    void Start()
    {
        /* オブジェクト検索 */
        pl_obj = GameObject.Find("Player");
        gm_obj = GameObject.Find("GameManager");

        /* コンポーネント取得 */
        pl_st = pl_obj.GetComponent<Pl_States>();

        gm = gm_obj.GetComponent<GameManager>();
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

    public void Btn_Retry()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        Time.timeScale = 1;
	}

    // ポーズボタン/終了
    public void Btn_Quit()
    {
        gm.isPaused = false;
        Time.timeScale = 1;

        SceneManager.LoadScene("Title");
    }

    // アクションボタン
    public void Btn_Action()
	{
        // 地上にいたらジャンプする
        if(pl_st.stateNum == Pl_States.States.landing) {
            pl_st.stateNum = Pl_States.States.jumping;
        }

        // ダメージ時はなにもしない
        else if(pl_st.stateNum == Pl_States.States.damage) {

        }

        // それ以外のときは、捕食
        else if(pl_st.stateNum != Pl_States.States.jumping){
            pl_st.stateNum = Pl_States.States.attacking;
		}
	}
}
