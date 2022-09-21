using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* ★〇〇に関するスクリプトです */
public class Clear_Ctrl : MonoBehaviour
{
    /* 値 */
    int score;      // 最終的なスコア

    /* コンポーネント取得用 */
    AudioManager aud;
    ScoreManager sm;

    Text scoreText;
    Text highScoreText;

//-------------------------------------------------------------------
    void Start()
    {
        /* オブジェクト取得 */
        GameObject audObj = transform.Find("AudioManager").gameObject;
        GameObject scoreTextObj = GameObject.Find("YourScore");
        GameObject highScoreTextObj = GameObject.Find("HighScore");

        /* コンポーネント取得 */
        aud = audObj.GetComponent<AudioManager>();
        scoreText = scoreTextObj.GetComponent<Text>();
        highScoreText = highScoreTextObj.GetComponent<Text>();

        sm = new ScoreManager();
        score = sm.GetScore();
        scoreText.text += score.ToString();
        HighScoreSetter();

        /* 初期化 */
        aud.PlayBGM(AudLists.BGMList.result, true);
    }

//-------------------------------------------------------------------
    void FixedUpdate()
    {
        
    }

//-------------------------------------------------------------------

    // ハイスコア更新
    void HighScoreSetter()
	{
        GameData data = new GameData();

        // 入れ替え
		if (data.highScore <= score) {
            data.highScore = score;
		}

        // 表示
        highScoreText.text += data.highScore.ToString();
	}
}
