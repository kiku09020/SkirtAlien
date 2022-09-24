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

    DataManager dm;
    GameData data;

    Text scoreText;
    Text highScoreText;

//-------------------------------------------------------------------
    void Awake()
    {
        /* オブジェクト取得 */
        GameObject audObj = transform.Find("AudioManager").gameObject;
        GameObject scoreTextObj = GameObject.Find("YourScore");
        GameObject highScoreTextObj = GameObject.Find("HighScore");

        /* コンポーネント取得 */
        aud = audObj.GetComponent<AudioManager>();
        dm = GetComponent<DataManager>();

        scoreText = scoreTextObj.GetComponent<Text>();
        highScoreText = highScoreTextObj.GetComponent<Text>();

        sm = new ScoreManager();

        data = dm.data;

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
        // 入れ替え
		if (data.highScore <= score) {
            data.highScore = score;
		}

        // そうじゃなかったらハイスコア読み込み
        else {
            dm.Load();
        }

        // 表示
        highScoreText.text += data.highScore.ToString();
	}
}
