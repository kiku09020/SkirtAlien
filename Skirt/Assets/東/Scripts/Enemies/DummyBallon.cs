using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★〇〇に関するスクリプトです */
//-------------------------------------------------------------------
public class DummyBallon : BallonClass
{
    /* コンポーネント取得用 */
    Pl_States st;

    //-------------------------------------------------------------------
    void Start()
    {
        GameObject plObj = GameObject.Find("Player");
        st = plObj.GetComponent<Pl_States>();

        /* 初期化 */

    }

//-------------------------------------------------------------------

    void FixedUpdate()
    {
        Up();
    }

	//-------------------------------------------------------------------

	private void OnTriggerEnter2D(Collider2D col)
	{
        if (col.tag == "Player") {
            st.stateNum = Pl_States.States.damage;
            Destroy(gameObject);
        }
	}

}
