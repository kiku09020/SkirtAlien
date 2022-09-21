using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* ★クリアボタンに関するスクリプトです */
public class Btn_Clear : MonoBehaviour
{
    /* 値 */


    /* コンポーネント取得用 */
    ScoreManager score;


//-------------------------------------------------------------------
    void Start()
    {
        /* オブジェクト取得 */
        score = new ScoreManager();

	/* コンポーネント取得 */     


        /* 初期化 */
        
    }

//-------------------------------------------------------------------
    void FixedUpdate()
    {
        
    }

//-------------------------------------------------------------------

    // タイトルへ
    public void BackTitle()
	{
        SceneManager.LoadScene("Title");
        score.ResetScore();
	}
}
