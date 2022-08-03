using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★プレイヤーのスクリプトです 
 * ・大きさ変更
 * ・ゲームオーバー処理
 * ・地上状態の処理
 * ・敵にふれたときの処理
 */

public class Player : MonoBehaviour
{
    // ------------------------------------------------------------------------
    /* オブジェクト */
    GameObject gm_obj;

    /* コンポーネント取得用 */
    GameManager gm;
    StageManager stg;

    Pl_States pl_st;


    //-------------------------------------------------------------------

    void Start()
    {
        /* オブジェクト検索 */
        gm_obj      = GameObject.Find("GameManager");

        /* コンポーネント取得 */
        gm          = gm_obj.GetComponent<GameManager>();
        stg = gm_obj.GetComponent<StageManager>();

        pl_st       = GetComponent<Pl_States>();

        // --------------------------------------------------------------------

        /* 初期化 */
        // スタート地点を、ステージの長さに合わせる
        transform.position = new Vector2(0, stg.stg_length);
    }

    //-------------------------------------------------------------------

    void FixedUpdate()
    {
        
    }
}
