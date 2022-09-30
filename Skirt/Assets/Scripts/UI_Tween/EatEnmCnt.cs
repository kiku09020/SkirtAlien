using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatEnmCnt : MonoBehaviour
{
    /* 値 */


    /* コンポーネント取得用 */
    GameObject plObj;


//-------------------------------------------------------------------
    void Start()
    {
        /* オブジェクト取得 */
        plObj = GameObject.FindWithTag("Player");

	/* コンポーネント取得 */     


        /* 初期化 */
        
    }

//-------------------------------------------------------------------
    void FixedUpdate()
    {
        transform.position = plObj.transform.position;
    }

//-------------------------------------------------------------------

}
