using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★ダミー風船のスクリプトです */
public class DummyBallon : BallonClass
{
    /* コンポーネント取得用 */
    Pl_States st;

    //-------------------------------------------------------------------
    void Start()
    {
        GameObject plObj = GameObject.FindGameObjectWithTag("Player");
        st = plObj.GetComponent<Pl_States>();
    }

//-------------------------------------------------------------------

    void FixedUpdate()
    {
        Up();
        Direction();
    }

	//-------------------------------------------------------------------

	private void OnTriggerEnter2D(Collider2D col)
	{
        // プレイヤーと触れたとき
        if (col.tag == "Player") {
            st.stateNum = Pl_States.States.damage;      // ダメージ状態にする
            Destroy(gameObject);                        // 消える
        }
	}

}
