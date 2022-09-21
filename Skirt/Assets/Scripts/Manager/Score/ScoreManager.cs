using UnityEngine;
using UnityEngine.UI;

/* ★スコアに関するスクリプトです */
//-------------------------------------------------------------------
public class ScoreManager : MonoBehaviour
{
    /* 値 */
    static public int nowSumScore;              // 現在のスコア
    static public int dispSumScore;                           // 表示スコア
    static public int savedScore;

    [Header("スコア計算")]
    [SerializeField] int addScore = 100;        // 加算するスコア
    int addDispSumScore;                        // 加算する表示スコア
    [SerializeField] int div;                   // 表示スコアと現在のスコアの差を割る数

    [Header("表示スコア")]
    [SerializeField] float dispTime;            // スコアの表示時間

    /* オブジェクト */
    [SerializeField] GameObject scorePref;      // スコアプレハブ
    GameObject scoreInst;                       // スコアのインスタンス
    GameObject canvas;                          // スコアを生成するキャンバス

    GameObject plObj;

    /* コンポーネント取得用 */    
    Text dispSumScoreText;                      // 合計スコアのテキスト
    Text scorePrefText;                         // スコアテキスト

    ComboManager combo;
    AudioManager aud;

//-------------------------------------------------------------------
    void Start()
    {
        GameObject dispSumScoreObj = GameObject.Find("Score");
        GameObject audObj = GameObject.Find("AudioManager");
        plObj = GameObject.Find("Player");
        canvas = GameObject.Find("GameUICanvas(Clone)");

        dispSumScoreText = dispSumScoreObj.GetComponent<Text>();
        scorePrefText = scorePref.GetComponent<Text>();
        combo = GetComponent<ComboManager>();

        aud = audObj.GetComponent<AudioManager>();
    }

//-------------------------------------------------------------------

    void FixedUpdate()
    {
        DispSumScore();
    }

//-------------------------------------------------------------------

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

            if (dispSumScore % 10 == 0) {
                aud.PlaySE(AudLists.SETypeList.score, 0);
            }
        }

        dispSumScoreText.text = "SCORE:" + dispSumScore.ToString();
	}

    // スコア加算(引数：コンボ数)
    public void AddScore()
    {
        int score = addScore;

        score = combo.Combo(score);

        // 合計スコアに加算
        nowSumScore += score;

        // 表示合計スコアに一度に加算される値を決める
        addDispSumScore = (nowSumScore - dispSumScore) / div;

        // スコアのテキストをインスタンス化
        InstScore(score);
    }

    //-------------------------------------------
    // ステージ開始時のスコアを保存する
    public void SaveScore()
    {
        savedScore = nowSumScore;
    }

    // 保存したスコアを読み込む
    public void LoadScore()
    {
        nowSumScore = savedScore;
        dispSumScore = savedScore;
    }

    // タイトルに戻った時にスコアをリセットする
    public void ResetScore()
    {
        nowSumScore = 0;
        dispSumScore = 0;
        savedScore = 0;
    }

    // スコア取得
    public int GetScore()
	{
        return nowSumScore;
	}
}
