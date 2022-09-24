using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★〇〇に関するスクリプトです */
public class Enemy : MonoBehaviour
{
    /* 値 */


    /* コンポーネント取得用 */
    GameObject plObj;
    Pl_States state;

//-------------------------------------------------------------------
    void Awake()
    {
        /* オブジェクト取得 */
        plObj = GameObject.FindGameObjectWithTag("Player");

        /* コンポーネント取得 */
        state = plObj.GetComponent<Pl_States>();

        /* 初期化 */
        
    }

//-------------------------------------------------------------------
    void FixedUpdate()
    {
        
    }

//-------------------------------------------------------------------
    // 没関数　無敵時間中に敵の当たり判定を無効化する
    // 地上の敵がすりぬけたり、無敵時間終了時点に敵と重なっちゃうことが多いので没
    public void Muteki(Collider2D col)
    {
        if (state.stateNum == Pl_States.States.damage) {
            col.enabled = false;
        }
        else {
            col.enabled = true;
        }
    }
}
