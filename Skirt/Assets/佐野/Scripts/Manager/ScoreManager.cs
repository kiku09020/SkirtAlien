using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* ★スコアに関するスクリプトです */
//-------------------------------------------------------------------
public class ScoreManager : MonoBehaviour
{
    /* 値 */
    int nowScore;               // 現在のスコア
    int dispScore;              // 表示スコア

    [SerializeField]
    const int addScore = 100;   // 加算するスコア

    /* オブジェクト */
    GameObject scoreObj;        // スコアオブジェクト

    /* コンポーネント取得用 */    
    Text scoreText;             // スコアのテキスト

//-------------------------------------------------------------------
    void Start()
    {
        FindObj();
        GetComp();        

        /* 初期化 */
        
    }

    /* オブジェクト検索 */
    void FindObj()
    {
        scoreObj = GameObject.Find("Score");
    }

    /* コンポーネント取得 */
    void GetComp()
    {
        scoreText = scoreObj.GetComponent<Text>();
    }

//-------------------------------------------------------------------

    void FixedUpdate()
    {
        DispScore();
    }

//-------------------------------------------------------------------

    // スコア加算
    void AddScore(int addMag)
	{
        // 追加するスコア
        int score = addScore * addMag;

        // 加算
        nowScore += score;
	}

    // スコア表示
    void DispScore()
	{
        scoreText.text = dispScore.ToString();
	}

}
