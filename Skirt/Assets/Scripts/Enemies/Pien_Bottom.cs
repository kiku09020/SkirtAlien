using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pien_Bottom : MonoBehaviour
{
    public bool isGroundHit;        // 地上にいるか

    //着地したか
    void OnTriggerEnter2D ( Collider2D col ) {
        if( col.gameObject.tag == "Floor"){
            isGroundHit = true;
        }
    }

    // 地面から離れたか
    void OnTriggerExit2D( Collider2D col )
    {
        if (col.gameObject.tag == "Floor"){
            isGroundHit = false;
        }
    }
}
