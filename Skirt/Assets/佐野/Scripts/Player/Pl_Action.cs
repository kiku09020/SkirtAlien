using UnityEngine;

/* プレイヤーの捕食や移動などの行動に関するスクリプトです */
public partial class Pl_Action : MonoBehaviour
{
    /* 値 */
    [Header("移動")]
    [SerializeField] float normalSpd;   // 通常時の速度
    [SerializeField] float nowSpd;      // 現在の速度
    [SerializeField] float spdMax;      // 最大速度
    [SerializeField] float fallSpdMax;  // 最大落下速度
    [SerializeField] float moveDec;     // 減速度

    [Header("回転")]
    [SerializeField] float moveRot;             // 移動時の回転角度
    [SerializeField] float rotDec;              // キーを離したときの回転速度

    [Header("ダメージ")]  //--------------------
    [SerializeField] int    invTime;            // 無敵時間
    [SerializeField] float  dmgJumpForce;       // ダメージ時のジャンプ力
    int dmgCnt;

    [Header("捕食")]          //--------------------
    [SerializeField] float  eatTime;            // 捕食時間
    int     eatCnt;

    [Header("消化")]          //--------------------
    [SerializeField] int    digBtnCntMax;       // 消化するまでに押す回数
    int digBtnCnt;                              // ボタンが押された回数

    [Header("ジャンプ")]      //--------------------
    [SerializeField] float  normalJumpForce;    // 通常時のジャンプ力
    [SerializeField] float  nowJumpForce;       // 現在のジャンプ力
    [SerializeField] float  jumpTime;           // ジャンプ時間
    int     jumpCnt;

    float scrEdge;          // 画面端のX座標
    Vector2 pos, vel;       // 位置、速度

    //-------------------------------------------------------------------

    /* オブジェクト */
    GameObject gm_obj;
    GameObject aud_obj;
    GameObject part_obj;
    GameObject goal_obj;

    GameObject cam_obj;

    /* コンポーネント取得用 */
    Rigidbody2D     rb;
    SpriteRenderer  sr;

    GameManager     gm;
    AudioManager    aud;
    ScoreManager    score;
    ComboManager    combo;
    Goal_Ctrl       goal;

    Pl_States       st;
    Pl_HP           hp;
    Pl_Hunger       hung;
    Pl_Camera       cam;        // カメラ
    Pl_Anim anim;
    Pl_Particle part;

    //-------------------------------------------------------------------

    void Start()
    {
        gm_obj      = GameObject.Find("GameManager");
        part_obj    = gm_obj.transform.Find("ParticleManager").gameObject;
        aud_obj     = gm_obj.transform.Find("AudioManager").gameObject;

        goal_obj    = GameObject.Find("Goal");
        cam_obj     = GameObject.Find("PlayerCamera");

        /* コンポーネント取得 */
        rb          = GetComponent<Rigidbody2D>();
        sr          = GetComponent<SpriteRenderer>();

        gm          = gm_obj.GetComponent<GameManager>();
        score       = gm_obj.GetComponent<ScoreManager>();
        combo       = gm_obj.GetComponent<ComboManager>();
        aud         = aud_obj.GetComponent<AudioManager>();
        part        = part_obj.GetComponent<Pl_Particle>();

        st          = GetComponent<Pl_States>();
        hp          = GetComponent<Pl_HP>();
        hung        = GetComponent<Pl_Hunger>();
        anim        = GetComponent<Pl_Anim>();

        cam         = cam_obj.GetComponent<Pl_Camera>();
        goal        = goal_obj.GetComponent<Goal_Ctrl>();

        /* 初期化 */
        nowSpd = normalSpd;
        nowJumpForce = normalJumpForce;
    }

    //-------------------------------------------------------------------

    void FixedUpdate()
    {
        if (!gm.isGameOver && !goal.isGoaled) {
            scrEdge = cam.scrnWidthWld;     // スクリーン端の座標更新
            pos = transform.position;       // 位置
            vel = rb.velocity;              // 速度

            Move();         // 移動
            Rotate();       // 回転
            OutScr();       // はみ出し
        }

        // ゲームオーバー時に不透明にする
        else if(gm.isGameOver) {
            sr.color = Color.white;
		}
    }

    //-------------------------------------------------------------------
}
