using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pien_Bottom : MonoBehaviour
{
    public bool isLanding;        // ínè„Ç…Ç¢ÇÈÇ©

    //íÖínÇµÇΩÇ©
    void OnTriggerEnter2D ( Collider2D col ) {
        if( col.gameObject.tag == "Floor"){
            isLanding = true;
        }
    }

    // ínñ Ç©ÇÁó£ÇÍÇΩÇ©
    void OnTriggerExit2D( Collider2D col )
    {
        if (col.gameObject.tag == "Floor"){
            isLanding = false;
        }
    }
}
