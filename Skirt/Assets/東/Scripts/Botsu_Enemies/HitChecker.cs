using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChecker : MonoBehaviour
{
    public bool isGroundHit;        // ínè„Ç…Ç¢ÇÈÇ©

    //íÖínÇµÇΩÇ©
    void OnTriggerEnter2D ( Collider2D col ) {
        if( col.gameObject.tag == "Floor"){
            isGroundHit = true;
        }
    }

    // ínñ Ç©ÇÁó£ÇÍÇΩÇ©
    void OnTriggerExit2D( Collider2D col )
    {
        if (col.gameObject.tag == "Floor"){
            isGroundHit = false;
        }
    }
}
