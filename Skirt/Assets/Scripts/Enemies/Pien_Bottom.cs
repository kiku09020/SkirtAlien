using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pien_Bottom : MonoBehaviour
{
    public bool isLanding;        // �n��ɂ��邩

    //���n������
    void OnTriggerEnter2D ( Collider2D col ) {
        if( col.gameObject.tag == "Floor"){
            isLanding = true;
        }
    }

    // �n�ʂ��痣�ꂽ��
    void OnTriggerExit2D( Collider2D col )
    {
        if (col.gameObject.tag == "Floor"){
            isLanding = false;
        }
    }
}
