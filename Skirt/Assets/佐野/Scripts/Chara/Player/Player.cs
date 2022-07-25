using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★プレイヤーのスクリプトです 
 * ・大きさ変更
 * ・ゲームオーバー処理
 * ・地上状態の処理
 */
public class Player : MonoBehaviour
{
    [Header("その他")]
    [SerializeField] float jumpForce = 50;     // ジャンプ力

    // ------------------------------------------------------------------------
    /* オブジェクト検索 */
    GameObject gm_obj;
    GameObject goal_obj;

    /* コンポーネント取得用 */
    Rigidbody2D rb;

    Pl_States   pl_st;      // プレイヤーの状態
    GameManager gm;         // GameManager
    Btn_Ctrl    btn_ctrl;   // ボタン
    Goal_Ctrl   goal;       // ゴール


    //-------------------------------------------------------------------

    void Start()
    {
        /* オブジェクト検索 */
        gm_obj      = GameObject.Find("GameManager");
        goal_obj    = GameObject.Find("Goal");

        /* コンポーネント取得 */
        rb          = GetComponent<Rigidbody2D>();
        pl_st       = GetComponent<Pl_States>();
        gm          = gm_obj.GetComponent<GameManager>();
        btn_ctrl    = gm_obj.GetComponent<Btn_Ctrl>();
        goal        = goal_obj.GetComponent<Goal_Ctrl>();

        // --------------------------------------------------------------------

        /* 初期化 */
        // スタート地点を、ステージの長さに合わせる
        transform.position = new Vector2(0, gm.stg_length);
    }

    //-------------------------------------------------------------------

    void FixedUpdate()
    {
        if (!btn_ctrl.isPause && !goal.isGoaled) {
            if (!gm.isGameOver) {
                Landing_Proc();     // 地上にいるときの処理
                SizeChange();       // 大きさ
            }
        }
    }

    //---------------------------------------------------------------------------------------------
    // 入力値ごとに大きさ変える
    void SizeChange()
    {
        // 攻撃時
        if(pl_st.isAttacking){
		}

        // ダメージ時
        else if (pl_st.isDamaged) {
            transform.localScale = new Vector2(3, 3);
        }

        // 地上にいるとき
        else if (pl_st.isLanding) {
            transform.localScale = new Vector2(3, 3);
        }

        else {
            if (gm.inpHor < 0) {        // 上
                transform.localScale = new Vector2(3, 3 + Mathf.Abs(gm.inpHor) * 3);
            }
            else if (gm.inpHor > 0) {   // 下
                transform.localScale = new Vector2(3 + Mathf.Abs(gm.inpHor) * 3, 3);
            }
        }
    }

    // 地上にいるときの処理
    void Landing_Proc()
    {
        if (pl_st.isLanding) {
            // 通常状態
            pl_st.state = (int)Pl_States.states.nml;
            pl_st.isJumping = false;

            // 回転しない
            transform.rotation = Quaternion.identity;

            // ボタンでジャンプ
            if (pl_st.isAttacking) {
                pl_st.isAttacking = false;
                pl_st.isJumping = true;
                rb.AddForce(Vector2.up * jumpForce);
            }
        }
    }

    //---------------------------------------------------------------------------------------------
    // 敵に触れたとき
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy") {
            // ダメージ状態
            pl_st.isDamaged = true;
        }
    }
}
