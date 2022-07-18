using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChecker : MonoBehaviour
{
    public bool isGroundHit;
    public bool isPlayerHit;
    public bool isEnemyHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void OnTriggerEnter2D ( Collider2D col ) {//接触した
        if( col.gameObject.tag == "Floor"){
            isGroundHit = true;
            Debug.Log("Ground");
        }
    }

    void OnTriggerExit2D( Collider2D col )
    {
        // 地面から離れた
        if (col.gameObject.tag == "Floor")
        {
            isGroundHit = false;
        }
    }
}
