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
    [Header("フラグ")]
    public bool isGameOver;     // ゲームオーバー

    // ------------------------------------------------------------------------
    /* オブジェクト検索 */
    GameObject gm_obj;
    GameObject goal_obj;

    /* コンポーネント取得用 */
    Rigidbody2D rb;
    Collider2D col;
    Pl_States pl_st;            // プレイヤーの状態
    GameManager gm;             // GameManager
    Btn_Ctrl btn_ctrl;       // ボタン
    Goal_Ctrl goal;           // ゴール
    Pl_HP pl_hp;      // 体力

    //-------------------------------------------------------------------

    void Start()
    {
        /* オブジェクト検索 */
        gm_obj = GameObject.Find("GameManager");
        goal_obj = GameObject.Find("Goal");

        /* コンポーネント取得 */
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        pl_st = GetComponent<Pl_States>();
        gm = gm_obj.GetComponent<GameManager>();
        btn_ctrl = gm_obj.GetComponent<Btn_Ctrl>();
        goal = goal_obj.GetComponent<Goal_Ctrl>();

        pl_hp = GetComponent<Pl_HP>();

        // --------------------------------------------------------------------

        /* 初期化 */

        // Transform関連   
        transform.position = new Vector2(0, gm.stg_length);        // スタート地点を、ステージの長さに合わせる
    }

    //-------------------------------------------------------------------

    void FixedUpdate()
    {
        if (!btn_ctrl.isPause && !goal.isGoaled) {
            GameOver();

            if (!isGameOver) {
                Landing_Proc(); // 地上にいるときの処理
                SizeChange();   // 大きさ
            }
        }
    }

    //---------------------------------------------------------------------------------------------

    void SizeChange()
    {
        if(pl_st.isAttacking){

		}

        else if (pl_st.isDamaged) {
            transform.localScale = new Vector2(3, 3);
        }

        // 入力値ごとに大きさ変える
        else if (pl_st.isLanding) {
            transform.localScale = new Vector2(3, 3);
        }

        else {
            // 上
            if (gm.inpHor < 0) {
                transform.localScale = new Vector2(3, 3 + Mathf.Abs(gm.inpHor) * 3);
            }
            // 下
            else if (gm.inpHor > 0) {
                transform.localScale = new Vector2(3 + Mathf.Abs(gm.inpHor) * 3, 3);
            }
        }
    }

    void GameOver()
    {
        // HPが0以下になったら終了
        if ( pl_hp.nowHP<= 0) {
            isGameOver = true;
        }

        if (isGameOver) {
            col.enabled = false;        // 当たり判定無効化

            StartCoroutine("GmOv");
        }
    }

    IEnumerator GmOv()
    {
        yield return new WaitForSeconds(3);
        Debug.Log("GameOver");
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
