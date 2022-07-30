using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★床関連のスクリプトです */
public class Flor_Ctrl : MonoBehaviour
{
    /* 値 */


    /* フラグ */


    /* オブジェクト */
    GameObject pl_obj;

    /* コンポーネント取得用 */
    Player pl;
    Pl_States pl_st;


//-------------------------------------------------------------------

    void Start()
    {
        pl_obj = GameObject.Find("Player");

        /* コンポーネント取得 */
        pl = pl_obj.GetComponent<Player>();
        pl_st = pl_obj.GetComponent<Pl_States>();


        /* 初期化 */
        
    }

//-------------------------------------------------------------------

    void Update()
    {
        
    }

    //-------------------------------------------------------------------

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.name == "Player") {
            pl_st.stateNum = Pl_States.States.landing;
            pl_st.isLanding = true;
            Debug.Log("床");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.name == "Player") {
            pl_st.stateNum = Pl_States.States.normal;
            pl_st.isLanding = false;
        }
    }
}
