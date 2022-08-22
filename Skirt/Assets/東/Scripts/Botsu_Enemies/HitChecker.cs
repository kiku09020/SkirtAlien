using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChecker : MonoBehaviour
{
    public bool isGroundHit;        // �n��ɂ��邩

    //���n������
    void OnTriggerEnter2D ( Collider2D col ) {
        if( col.gameObject.tag == "Floor"){
            isGroundHit = true;
        }
    }

    // �n�ʂ��痣�ꂽ��
    void OnTriggerExit2D( Collider2D col )
    {
        if (col.gameObject.tag == "Floor"){
            isGroundHit = false;
        }
    }
}
