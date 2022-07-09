using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Goal_Ctrl : MonoBehaviour
{
    /* 値 */


    /* フラグ */
    public bool isGoaled;

    /* オブジェクト */
    GameObject pl_obj;

    /* コンポーネント取得用 */
    Player pl;

//-------------------------------------------------------------------

    void Start()
    {
        pl_obj = GameObject.Find("Player");

        /* コンポーネント取得 */
        pl = pl_obj.GetComponent<Player>();

        /* 初期化 */
        isGoaled = false;
    }

//-------------------------------------------------------------------

	// ゴール時
	private void OnTriggerEnter2D(Collider2D col)
	{
        if (col.tag == "Player")
        {
            isGoaled = true;                // ゴール

            pl.transform.localScale = new Vector2(3, 3);
        }

	}

	//-------------------------------------------------------------------

}
