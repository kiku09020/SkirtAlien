using UnityEngine;

/* ★ゲーム全般の処理を行うスクリプトです */
public class GameManager : MonoBehaviour
{
    /* 値 */
    [Header("スマホ用の値")]
    public float inpVer;                    // 垂直(縦)
    public float inpHor;                    // 平行(横)
    public float inpVerOld, inpHorOld;      // ひとつ前の入力値
    Joystick stick;                         // スティック

    [Header("開始演出")]
    [SerializeField] float startTimeLim = 4.6f; // 開始までの時間
                     float startTimer;

    [Header("フラグ")]
    public bool isStarting;         // 開始
    public bool started;            // 開始時間が終わった瞬間
    public bool isGameOver;         // ゲームオーバー
    public bool isGoaled;           //ゴール

    // ポーズ
    public bool isPaused;           // ポーズ中

    /* コンポーネント取得用 */
    CanvasGenelator cvs;
    AudioManager aud;
//-------------------------------------------------------------------
    void Awake()
    {
        /* コンポーネント取得 */
        stick = GameObject.Find("Stick").GetComponent<Joystick>();
        aud   = transform.Find("AudioManager").GetComponent<AudioManager>();
        cvs   = transform.Find("UIManager").gameObject.GetComponent<CanvasGenelator>();

        /* 初期化 */
        isStarting = true;
        isPaused   = false;

        aud.PlayBGM(AudLists.BGMList.stg_intro, false);    // イントロ再生
    }

    //-------------------------------------------------------------------
    void FixedUpdate()
    {
        // 入力値
        inpVerOld = inpVer;         inpHorOld = inpHor;
        inpVer = stick.Horizontal;  inpHor = stick.Vertical;

        Starting();
    }

//-------------------------------------------------------------------
    void Starting() // 開始時の演出
    {
        started = false;

		if(isStarting) {
            startTimer += Time.deltaTime;

            // 時間経過時
			if(startTimer > startTimeLim) {
                startTimer = 0;
                isStarting = false;     // スタート演出終了
                started = true;
                aud.PlayBGM(AudLists.BGMList.stg_normal,true);     // BGM再生
            }
		}
	}
}
