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
    [SerializeField] int addDispScore = 2;       // 加算する表示スコア

    /* オブジェクト */
    GameObject dispScoreObj;        // スコアオブジェクト

    [SerializeField] GameObject scorePref;           // スコアプレハブ
    GameObject scoreInst;
    GameObject canvas;

    GameObject plObj;

    /* コンポーネント取得用 */    
    Text dispScoreText;             // スコアのテキスト
    Text scorePrefText;             // 消化時出るプレハブのテキスト

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
        dispScoreObj = GameObject.Find("Score");
        plObj = GameObject.Find("Player");
        canvas = GameObject.Find("GameUICanvas(Clone)");
    }

    /* コンポーネント取得 */
    void GetComp()
    {
        dispScoreText = dispScoreObj.GetComponent<Text>();
        scorePrefText = scorePref.GetComponent<Text>();
    }

//-------------------------------------------------------------------

    void FixedUpdate()
    {
        DispScore();
    }

//-------------------------------------------------------------------

    // スコア加算
    public void AddScore(int addMag)
	{
        // 追加するスコア
        int score = addScore * addMag;

        // 加算
        nowScore += score;

        InstScoreText(score);
	}

    // スコア表示
    void InstScoreText(int score)
	{
        scorePrefText.text = score.ToString();                  // テキスト
        Vector3 pos = Camera.main.WorldToScreenPoint(plObj.transform.position);

        scoreInst = Instantiate(scorePref, pos, Quaternion.identity);    // インスタンス化
        scoreInst.transform.SetParent(canvas.transform);
        Destroy(scoreInst, 3);                                  // 削除
    }

    // スコア表示
    void DispScore()
	{
        // 表示スコア徐々に加算
        if(dispScore < nowScore) {
            dispScore += addDispScore;
        }

        dispScoreText.text = "SCORE:" + dispScore.ToString();
	}

}
