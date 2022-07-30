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
    [SerializeField] float moveForce    = 7;        // 移動に加える力
    [SerializeField] float spdMax       = 7.5f;     // 最大速度
    [SerializeField] float moveDec      = 0.96f;    // 減速度
    [SerializeField] float moveRot      = 15;       // 移動時の回転角度

    [Header("ダメージ関係")]
    [SerializeField] int invTime;                   // 無敵時間
    [SerializeField] float dmgJumpForce = 300;      // ダメージ時のジャンプ力
    int dmgCnt;

    [Header("攻撃")]
    [SerializeField] float atkTime      = 60;       // 攻撃の長さ
    int atkCnt;

    [Header("ジャンプ")]
    [SerializeField] float jumpForce    = 50;       // ジャンプ力
    int jumpCnt;

    float scrEdge;                // 画面端のX座標
    Vector2 pos, vel;              // 位置、速度

    /* フラグ */

    /* オブジェクト */
    GameObject gm_obj;
    GameObject goal_obj;

    GameObject cam_obj;

    /* コンポーネント取得用 */
    Rigidbody2D rb;
    SpriteRenderer sr;

    GameManager gm;
    Goal_Ctrl goal;

    Pl_States pl_st;
    Pl_HP pl_hp;
    Pl_Camera cam;            // カメラ

    //-------------------------------------------------------------------

    void Start()
    {
        gm_obj      = GameObject.Find("GameManager");
        goal_obj    = GameObject.Find("Goal");

        cam_obj     = GameObject.Find("PlayerCamera");

        /* コンポーネント取得 */
        rb          = GetComponent<Rigidbody2D>();
        sr          = GetComponent<SpriteRenderer>();

        gm          = gm_obj.GetComponent<GameManager>();
        goal        = goal_obj.GetComponent<Goal_Ctrl>();

        pl_st       = GetComponent<Pl_States>();
        pl_hp       = GetComponent<Pl_HP>();

        cam         = cam_obj.GetComponent<Pl_Camera>();
        /* 初期化 */

    }

    //-------------------------------------------------------------------

    void FixedUpdate()
    {
        if (!gm.isGameOver&&!goal.isGoaled) {
            scrEdge = cam.scrn_EdgeX;       // スクリーン端の座標更新
            pos = transform.position;       // 位置
            vel = rb.velocity;              // 速度

            Move_Side();
        }

        else if(gm.isGameOver) {
            sr.color = new Color(1, 1, 1,1);
		}
    }

    //-------------------------------------------------------------------

    // 左右移動
    public void Move_Side()
    {
        // ----------------------------------------------------------
        // 移動
        rb.AddForce(Vector2.right * gm.inpVer * moveForce);

        // 速度制限
        // 右
        if (vel.x > spdMax) {
            rb.velocity = new Vector2(spdMax, vel.y);
        }

        // 左
        else if (vel.x < -spdMax) {
            rb.velocity = new Vector2(-spdMax, vel.y);
        }

        // 左→右
        if (gm.inpVerOld < 0 && gm.inpVer > 0) {
            rb.velocity = new Vector2(vel.x * 0.75f, vel.y);
        }

        // 右→左
        if (gm.inpVerOld > 0 && gm.inpVer < 0) {
            rb.velocity = new Vector2(vel.x * 0.75f, vel.y);
        }

        if (!pl_st.isLanding) {
            // 移動方向に合わせて回転
            transform.rotation = Quaternion.Euler(0, 0, gm.inpVer * moveRot);
        }

        // 少しずつ減速、角度を元に戻す
        if (gm.inpVer == 0) {
            rb.velocity = new Vector2(vel.x * moveDec, vel.y);
        }

        // 画面外はみ出し時
        if (pos.x > scrEdge) {        // 右端→左端
            pos.x = -scrEdge;
            transform.position = new Vector2(pos.x, pos.y);
        }

        else if (pos.x < -scrEdge) {  // 左端→右端
            pos.x = scrEdge;
            transform.position = new Vector2(pos.x, pos.y);
        }

        else {
            sr.flipX = false;
        }
    }

    // 地上移動
    public void LandMove()
    {
        // 左移動時
        if(gm.inpVer < 0) {
            sr.flipX = false;
        }

        // 右移動時
        else if(gm.inpVer > 0) {
            sr.flipX = true;
        }

        else {
            sr.flipX = false;
        }
    }

    //-------------------------------------------------------------------
    // ダメージ
    public void Damage()
    {
        dmgCnt++;

        // ダメージくらった瞬間
        if (dmgCnt == 1) {
            pl_hp.HP_Damage();
            rb.AddForce(Vector2.up * dmgJumpForce);         // 少し飛ばす
        }

        // 点滅
        if (dmgCnt % 2 == 0) {
            sr.color = new Color(1, 1, 1, 0);
        }

        else {
            sr.color = new Color(1, 1, 1, 1);
        }

        // ダメージ処理終了
        if (dmgCnt > invTime) {
            dmgCnt = 0;
            pl_st.isDamaging = false;
        }
    }

    // アタック
    public void Attack()
    {
        atkCnt++;      // カウンター増加
        transform.localScale = new Vector2(3 + atkCnt * 0.05f, 3 + atkCnt * 0.05f);

        // 時間経過後、通常状態へ戻る
        if (atkCnt > atkTime) {
            atkCnt = 0;

            pl_st.isAttacking = false;
            pl_st.stateNum = Pl_States.States.normal;
        }
    }

    // ジャンプ
    public void Jump()
    {
        jumpCnt++;

        // 一瞬ジャンプ
		if(jumpCnt == 1) {
            rb.AddForce(Vector2.up * jumpForce);
        }

        // 解除
        if(jumpCnt > 60) {
            jumpCnt = 0;
            pl_st.isJamping = false;
        }
    }
}
