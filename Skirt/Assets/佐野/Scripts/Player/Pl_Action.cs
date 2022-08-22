using UnityEngine;

/* プレイヤーの捕食や移動などの行動に関するスクリプトです
 * ・移動 
 * ・ダメージ
 * ・捕食
 * ・消化
 */
public partial class Pl_Action : MonoBehaviour
{
    /* 値 */
    [Header("移動")]
    [SerializeField] float normalSpd;   // 通常時の速度
    [SerializeField] float nowSpd;      // 現在の速度
    [SerializeField] float spdMax;      // 最大速度
    [SerializeField] float moveDec;     // 減速度
    public float velY;                  // 速度(Y)カメラ用

    [Header("回転")]
    [SerializeField] float moveRot;     // 移動時の回転角度
    [SerializeField] float rotDec;      // キーを離したときの回転速度

    [Header("ダメージ関係")]  //--------------------
    [SerializeField, Tooltip("無敵時間")]
    int     invTime;
    [SerializeField,Tooltip("ダメージ時のジャンプ力")]
    float   dmgJumpForce;
    int     dmgCnt;

    [Header("捕食")]          //--------------------
    [SerializeField,Tooltip("捕食時間")]
    float   eatTime;
    int     eatCnt;

    [Header("消化")]          //--------------------
    [SerializeField,Tooltip("消化するまでにボタンを押す回数")]
    int     digBtnCntMax;
    int     digBtnCnt;      // ボタンが押された回数

    [Header("ジャンプ")]      //--------------------
    [SerializeField,Tooltip("通常時のジャンプ力")] 
    float normalJumpForce;
    [SerializeField,Tooltip("現在のジャンプ力")]
    float   nowJumpForce;
    [SerializeField,Tooltip("ジャンプ状態が終わるまでの時間")]
    float   jumpTime;
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
        part_obj    = GameObject.Find("ParticleManager");
        goal_obj    = GameObject.Find("Goal");
        aud_obj     = GameObject.Find("AudioManager");

        cam_obj     = GameObject.Find("PlayerCamera");

        /* コンポーネント取得 */
        rb          = GetComponent<Rigidbody2D>();
        sr          = GetComponent<SpriteRenderer>();

        gm          = gm_obj.GetComponent<GameManager>();
        aud         = aud_obj.GetComponent<AudioManager>();
        part        = part_obj.GetComponent<Pl_Particle>();
        goal        = goal_obj.GetComponent<Goal_Ctrl>();

        st          = GetComponent<Pl_States>();
        hp          = GetComponent<Pl_HP>();
        hung        = GetComponent<Pl_Hunger>();
        anim        = GetComponent<Pl_Anim>();

        cam         = cam_obj.GetComponent<Pl_Camera>();

        /* 初期化 */
        nowSpd = normalSpd;
        nowJumpForce = normalJumpForce;
    }

    //-------------------------------------------------------------------

    void FixedUpdate()
    {
        if (!gm.isGameOver && !goal.isGoaled) {
            velY = rb.velocity.y;
            scrEdge = cam.scrnWidthWld;       // スクリーン端の座標更新
            pos = transform.position;       // 位置
            vel = rb.velocity;              // 速度

            if (st.stateNum != Pl_States.States.digest) {
                Move();
                Rotate();
            }

            OutScr();
        }

        // ゲームオーバー時に不透明にする
        else if(gm.isGameOver) {
            sr.color = Color.white;
		}
    }

    //-------------------------------------------------------------------
}
