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
    [SerializeField] float startTime;       // 開始までの時間
                     float time;

    [Header("フラグ")]
    public bool isStarting;         // 開始
    public bool isGameOver;         // ゲームオーバー
    public bool isPaused;           // ポーズ中

    /* オブジェクト */
    GameObject cvs_obj;

    /* コンポーネント取得用 */
    CanvasGenelator cvs;
//-------------------------------------------------------------------

    void Awake()
    {
        /* オブジェクト検索 */
        cvs_obj = transform.GetChild(0).gameObject;

        /* コンポーネント取得 */
        stick = GameObject.Find("Stick").GetComponent<Joystick>();
        cvs = cvs_obj.GetComponent<CanvasGenelator>();

        /* 初期化 */
        isStarting = true;
        isPaused = false;
    }

	void Start()
	{
        
    }

    //-------------------------------------------------------------------

    void FixedUpdate()
    {
        // 入力値
        inpVerOld = inpVer; inpHorOld = inpHor;

        inpVer = stick.Horizontal;
        inpHor = stick.Vertical;

        Starting();

        Debug.Log(isStarting);
    }

//-------------------------------------------------------------------

    // 開始時の演出
    void Starting()
	{
		if(isStarting) {
            time += Time.deltaTime;

            // スタート時間
			if(time > startTime) {
                time = 0;
                isStarting = false;     // スタート終了
            }
		}
	}
}
