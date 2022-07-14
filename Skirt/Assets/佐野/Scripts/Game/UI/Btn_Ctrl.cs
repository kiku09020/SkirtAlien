using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* ★ボタンに関するスクリプトです */
public class Btn_Ctrl : MonoBehaviour
{
    /* 値 */


    /* フラグ */
    public bool isPause;

    /* オブジェクト */
    public GameObject stick;

    [SerializeField] GameObject pauseUI_pref;   // Canvasのプレハブ
                     GameObject pauseUI_inst;   // Canvasのインスタンス


    /* コンポーネント取得用 */

//-------------------------------------------------------------------

    void Start()
    {
        /* オブジェクト検索 */

        /* コンポーネント取得 */

        /* 初期化 */
        isPause = false;
        pauseUI_inst = Instantiate(pauseUI_pref, Vector2.zero, Quaternion.identity);
        pauseUI_inst.SetActive(false);
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

        Debug.Log(isPause);
    }

    public void Btn_Retry()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

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
