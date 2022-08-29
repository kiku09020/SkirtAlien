using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★〇〇に関するスクリプトです */
//-------------------------------------------------------------------
public class BallonClass : MonoBehaviour
{
    /* 値 */
    public float movespeed = 0.01f;       // 速度

//-------------------------------------------------------------------
    // 上まで行ったら消す
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

	//-------------------------------------------------------------------

	public void Up()
	{
        // 上昇
        transform.Translate(0, movespeed, 0);
    }
}
