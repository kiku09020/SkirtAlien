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
    AudioManager    aud;
    ScoreManager    sm;
    DataManager     dm;
    GameData        data;

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
        dm  = GetComponent<DataManager>();
        sm  = new ScoreManager();

        scoreText     = scoreTextObj.GetComponent<Text>();
        highScoreText = highScoreTextObj.GetComponent<Text>();

        /* 初期化 */
        DispScore();                                    // ハイスコアテキスト
        aud.PlayBGM(AudLists.BGMList.result, true);     // BGM再生
    }

//-------------------------------------------------------------------
    // ハイスコア更新
    void DispScore()
	{
        data  = dm.data;
        score = sm.GetScore();                  // スコア取得

        // 入れ替え
        if (data.highScore <= score) {
            data.highScore = score;
		}

        // 表示
        scoreText.text     += score.ToString();             // スコアテキスト
        highScoreText.text += data.highScore.ToString();    
	}
}
