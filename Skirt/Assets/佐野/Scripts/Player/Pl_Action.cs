using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* プレイヤーの捕食や移動などの行動に関するスクリプトです
 * ・移動 
 * ・ダメージ
 * ・攻撃
 * ・
 */
public class Pl_Action : MonoBehaviour
{
    /* 値 */
    [Header("移動")]
    public float startSpd = 7;
    [HideInInspector] public float moveSpd;        // 移動に加える力
    [SerializeField] float spdMax       = 7.5f;     // 最大速度
    [SerializeField] float moveDec      = 0.96f;    // 減速度
    [SerializeField] float moveRot      = 15;       // 移動時の回転角度
    public float velY;                              // 速度(Y)カメラ用

    [Header("ダメージ関係")]
    [SerializeField] int invTime;                   // 無敵時間
    [SerializeField] float dmgJumpForce = 300;      // ダメージ時のジャンプ力
    int dmgCnt;

    [Header("捕食")]
    [SerializeField] float eatTime      = 90    ;       // 捕食の長さ
    int eatCnt;

    [Header("消化")]
    [SerializeField] int digBtnCntMax = 10;         // 消化ボタン押す数
    int digBtnCnt;                                  // ボタン押された回数

    [Header("ジャンプ")]
    [SerializeField] float jumpForce    = 50;       // ジャンプ力
    [SerializeField] float jumpTime     = 30;		// ジャンプ時間
    int jumpCnt;

    float scrEdge;              // 画面端のX座標
    Vector2 pos, vel;           // 位置、速度

    /* フラグ */

    /* オブジェクト */
    GameObject gm_obj;
    GameObject part_obj;
    GameObject goal_obj;

    GameObject cam_obj;

    /* コンポーネント取得用 */
    Rigidbody2D     rb;
    SpriteRenderer  sr;

    GameManager     gm;
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

        cam_obj     = GameObject.Find("PlayerCamera");

        /* コンポーネント取得 */
        rb          = GetComponent<Rigidbody2D>();
        sr          = GetComponent<SpriteRenderer>();

        gm          = gm_obj.GetComponent<GameManager>();
        part        = part_obj.GetComponent<Pl_Particle>();
        goal        = goal_obj.GetComponent<Goal_Ctrl>();

        st       = GetComponent<Pl_States>();
        hp       = GetComponent<Pl_HP>();
        hung = GetComponent<Pl_Hunger>();
        anim = GetComponent<Pl_Anim>();

        cam         = cam_obj.GetComponent<Pl_Camera>();

        /* 初期化 */
        moveSpd = startSpd;
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

    // 移動
    void Move()
    {
        // 移動
        rb.AddForce(Vector2.right * gm.inpVer * moveSpd);

        if (velY < -100) {
            rb.AddForce(Vector2.up * 20);
           
        }

        SpdLimit();     // 速度制限
        Breaking();     // ブレーキ(慣性無視)
    }

    // 速度制限
    void SpdLimit()
	{
        // 右
        if(vel.x > spdMax) {
            rb.velocity = new Vector2(spdMax, vel.y);
        }

        // 左
        else if(vel.x < -spdMax) {
            rb.velocity = new Vector2(-spdMax, vel.y);
        }
    }

    // ブレーキ(スティックを急に反対方向に傾けたときに慣性を軽減するようにする)
    void Breaking()
	{
        // 左→右
        if(gm.inpVerOld < 0 && gm.inpVer > 0) {
            rb.velocity = new Vector2(vel.x * 0.75f, vel.y);
        }

        // 右→左
        if(gm.inpVerOld > 0 && gm.inpVer < 0) {
            rb.velocity = new Vector2(vel.x * 0.75f, vel.y);
        }
    }

    //-------------------------------------------------------------------

    // 回転
    void Rotate()
    {
        if(st.stateNum == Pl_States.States.landing) {
            transform.rotation = Quaternion.identity;
        }

        // 移動方向に合わせて回転
        else {
            transform.rotation = Quaternion.Euler(0, 0, gm.inpVer * moveRot);
        }

        // 少しずつ減速、角度を元に戻す
        if (gm.inpVer == 0) {
            rb.velocity = new Vector2(vel.x * moveDec, vel.y);
        }
    }

    // はみ出し時の処理
    void OutScr()
    {
        if (pos.x > scrEdge) {        // 右端→左端
            pos.x = -scrEdge;
            transform.position = new Vector2(pos.x, pos.y);
        }

        else if (pos.x < -scrEdge) {  // 左端→右端
            pos.x = scrEdge;
            transform.position = new Vector2(pos.x, pos.y);
        }
    }

    //-------------------------------------------------------------------
    /* Pl_State内に呼び出す関数 */

    // ダメージ
    public void Damage()
    {
        dmgCnt++;

        transform.localScale = Vector2.one;

        // ダメージくらった瞬間
        if (dmgCnt == 1) {
            hp.HP_Damage();
            part.Part_Damaged();
            rb.AddForce(Vector2.up * dmgJumpForce);         // 少し飛ばす
        }

        // 点滅
        if (dmgCnt % 2 == 0) {
            sr.color = Color.clear;
        }

        else {
            sr.color = Color.white;
        }

        // ダメージ処理終了
        if (dmgCnt > invTime) {
            dmgCnt = 0;
            st.stateNum = Pl_States.States.normal;
        }
    }

    // 捕食
    public void Eating()
    {
        eatCnt++;      // カウンター増加

        if (eatCnt == 1) {
            part.Part_Eating();
        }

        // 時間経過後、通常状態へ戻る
        if (eatCnt > eatTime) {
            eatCnt = 0;
            st.stateNum = Pl_States.States.normal;
        }
    }

    public void Digest_Btn()
    {
        print(digBtnCnt);

        if (digBtnCnt < digBtnCntMax) {
            digBtnCnt++;
            anim.DigBtnAnim();          // クリック時に再生
        }
        else {
            st.stateNum = Pl_States.States.normal;
            hung.HungInc();
            digBtnCnt = 0;
        }
    }

    // ジャンプ
    public void Jump()
    {
        jumpCnt++;

        // 一瞬ジャンプ
		if(jumpCnt == 1) {
            rb.AddForce(Vector2.up * jumpForce);
            part.Part_Jumping();
        }

        // 解除
        if(jumpCnt > jumpTime) {
            jumpCnt = 0;
            st.stateNum = Pl_States.States.normal;
        }
    }
}
