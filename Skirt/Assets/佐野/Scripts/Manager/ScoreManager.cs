using UnityEngine;
using UnityEngine.UI;

/* ★スコアに関するスクリプトです */
//-------------------------------------------------------------------
public class ScoreManager : MonoBehaviour
{
    /* 値 */
    int nowSumScore;                            // 現在のスコア
    int dispSumScore;                           // 表示スコア

    [Header("合計スコア")]
    [SerializeField] int addScore = 100;        // 加算するスコア
    int addDispSumScore;                        // 加算する表示スコア
    [SerializeField] int div;                   // 表示スコアと現在のスコアの差を割る数

    [Header("スコア")]
    [SerializeField] float dispTime;            // スコアの表示時間

    /* オブジェクト */
    GameObject dispSumScoreObj;                 // スコアオブジェクト

    [SerializeField] GameObject scorePref;      // スコアプレハブ
    GameObject scoreInst;                       // スコアのインスタンス
    GameObject canvas;                          // スコアを生成するキャンバス

    GameObject plObj;

    /* コンポーネント取得用 */    
    Text dispSumScoreText;                      // 合計スコアのテキスト
    Text scorePrefText;                         // スコアテキスト

//-------------------------------------------------------------------
    void Start()
    {
        FindObj();
        GetComp();
    }

    /* オブジェクト検索 */
    void FindObj()
    {
        dispSumScoreObj = GameObject.Find("Score");
        plObj           = GameObject.Find("Player");
        canvas          = GameObject.Find("GameUICanvas(Clone)");
    }

    /* コンポーネント取得 */
    void GetComp()
    {
        dispSumScoreText = dispSumScoreObj.GetComponent<Text>();
        scorePrefText    = scorePref.GetComponent<Text>();
    }

//-------------------------------------------------------------------

    void FixedUpdate()
    {
        DispSumScore();
    }

//-------------------------------------------------------------------

    // スコア加算
    public void AddScore(int addMag)
	{
        // 追加するスコア
        int score = addScore * (int)Mathf.Pow(2, addMag - 1);

        // 合計スコアに加算
        nowSumScore += score;

        // 表示合計スコアに一度に加算される値を決める
        addDispSumScore = (nowSumScore - dispSumScore) / div;

        // スコアのテキストをインスタンス化
        InstScore(score);
	}

    // スコア表示
    void InstScore(int score)
	{
        // テキスト、表示位置を決定
        scorePrefText.text = "+" + score.ToString();                  
        Vector3 pos = Camera.main.WorldToScreenPoint(plObj.transform.position);

        // インスタンス化
        scoreInst = Instantiate(scorePref, pos, Quaternion.identity,canvas.transform);

        // 削除
        Destroy(scoreInst, dispTime);
    }

    // 合計スコア表示
    void DispSumScore()
	{
        // 表示スコア徐々に加算
        if(dispSumScore < nowSumScore) {
            dispSumScore += addDispSumScore;
        }

        dispSumScoreText.text = "SCORE:" + dispSumScore.ToString();
	}

}
