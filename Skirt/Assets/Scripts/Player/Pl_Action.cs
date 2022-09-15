using UnityEngine;

/* プレイヤーの捕食や移動などの行動に関するスクリプトです */
public partial class Pl_Action : MonoBehaviour
{
    /* 値 */
    float scrEdge;          // 画面端のX座標
    Vector2 pos, vel;       // 位置、速度

    [Header("移動")]
    [SerializeField] float nmlSpd;          // 通常時の速度
    [SerializeField] float nowSpd;          // 現在の速度
    [SerializeField] float spdMax;          // 最大速度
    [SerializeField] float fallSpdMax;      // 最大落下速度

    [SerializeField] float moveDec;         // 落下時の減速度
    [SerializeField] float breakDec;        // ブレーキ時の減速度
                            
    [Header("回転")]        //--------------------
    [SerializeField] float moveRot;         // 移動時の回転角度
    [SerializeField] float rotDec;          // キーを離したときの回転速度

    [Header("ダメージ")]    //--------------------
    [SerializeField] int   dmgTimeLim;      // 無敵時間
    [SerializeField] float dmgJumpForce;    // ダメージ時のジャンプ力
    [SerializeField] float flashCycle;      // 点滅時のサイクル
    float dmgTimer;

    [Header("捕食")]        //--------------------
    [SerializeField] float eatTimeLim;      // 捕食時間
    float eatTimer;

    [Header("消化")]        //--------------------
    [SerializeField] int   digBtnCntMax;    // 消化するまでに押す回数
    float digBtnCnt;                          // ボタンが押された回数

    [Header("ジャンプ")]    //--------------------
    [SerializeField] float nmlJumpForce;    // 通常時のジャンプ力
    [SerializeField] float nowJumpForce;    // 現在のジャンプ力
    [SerializeField] float jumpTime;        // ジャンプ時間
    float     jumpTimer;

    //-------------------------------------------------------------------
    /* コンポーネント取得用 */
    Rigidbody2D     rb;
    SpriteRenderer  sr;

    GameManager     gm;
    AudioManager    aud;
    ScoreManager    score;
    ComboManager    combo;

    Pl_States       st;
    Pl_HP           hp;
    Pl_Hunger       hung;
    PlayerCamera       cam;        // カメラ
    PlayerAnim anim;
    ParticleManager part;

    //-------------------------------------------------------------------
    void Start()
    {
        GameObject gm_obj      = GameObject.Find("GameManager");
        GameObject part_obj    = gm_obj.transform.Find("ParticleManager").gameObject;
        GameObject aud_obj     = gm_obj.transform.Find("AudioManager").gameObject;
        GameObject cam_obj     = GameObject.Find("PlayerCamera");

        /* コンポーネント取得 */
        gm          = gm_obj.GetComponent<GameManager>();
        score       = gm_obj.GetComponent<ScoreManager>();
        combo       = gm_obj.GetComponent<ComboManager>();
        aud         = aud_obj.GetComponent<AudioManager>();
        part        = part_obj.GetComponent<ParticleManager>();
        cam         = cam_obj.GetComponent<PlayerCamera>();

        rb          = GetComponent<Rigidbody2D>();
        sr          = GetComponent<SpriteRenderer>();
        st          = GetComponent<Pl_States>();
        hp          = GetComponent<Pl_HP>();
        hung        = GetComponent<Pl_Hunger>();
        anim        = GetComponent<PlayerAnim>();

        /* 初期化 */
        nowSpd = nmlSpd;
        nowJumpForce = nmlJumpForce;
    }

    //-------------------------------------------------------------------
    void FixedUpdate()
    {
        if (!gm.isGameOver && !gm.isGoaled) {
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
