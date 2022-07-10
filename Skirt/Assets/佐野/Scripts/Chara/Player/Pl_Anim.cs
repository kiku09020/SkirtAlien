using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pl_Anim : MonoBehaviour {
    /* 値 */


    /* フラグ */


    /* オブジェクト */


    /* コンポーネント取得用 */
    Animator anim;

    Player pl;
    Pl_States pl_st;

    //-------------------------------------------------------------------

    void Start()
    {
        /* コンポーネント取得 */
        anim = GetComponent<Animator>();

        pl = GetComponent<Player>();
        pl_st = GetComponent<Pl_States>();

        /* 初期化 */

    }

    //-------------------------------------------------------------------

    void FixedUpdate()
    {

    }
}
