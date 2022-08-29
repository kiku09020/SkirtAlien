using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★〇〇に関するスクリプトです */
//-------------------------------------------------------------------
public class DummyBallon : BallonClass
{
    /* 値 */


    /* フラグ */


    /* オブジェクト */
    GameObject plObj;

    /* コンポーネント取得用 */
    Pl_Action act;
    Pl_HP hp;


    //-------------------------------------------------------------------
    void Start()
    {
        FindObj();
        GetComp();        

        /* 初期化 */
        
    }

    /* オブジェクト検索 */
    void FindObj()
    {
        plObj = GameObject.Find("Player");
    }

    /* コンポーネント取得 */
    void GetComp()
    {
        hp = plObj.GetComponent<Pl_HP>();
        act = plObj.GetComponent<Pl_Action>();
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

            act.Damage_Proc();
            Destroy(gameObject);
        }
	}

}
