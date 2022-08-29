using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBallon : BallonClass
{
    GameObject audObj;
    GameObject plObj;

    Pl_HP hp;
    AudioManager aud;

    void Start()
    {
        audObj = GameObject.Find("AudioManager");
        plObj = GameObject.Find("Player");

        aud = audObj.GetComponent<AudioManager>();
        hp = plObj.GetComponent<Pl_HP>();
    }

    void FixedUpdate()
    {
        Up();
    }

    // ìñÇΩÇ¡ÇΩéû
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") {
            hp.HP_Heal();

            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.heal);       // å¯â âπçƒê∂

            Destroy(gameObject);        // çÌèú
        }
    }
}
