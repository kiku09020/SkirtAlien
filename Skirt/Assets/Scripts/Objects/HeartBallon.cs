using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBallon : BallonClass
{
    Pl_HP hp;
    AudioManager aud;
    ParticleManager part;

    void Start()
    {
        GameObject partObj = GameObject.Find("ParticleManager");
        GameObject audObj = GameObject.Find("AudioManager");
        GameObject plObj = GameObject.Find("Player");

        part = partObj.GetComponent<ParticleManager>();
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

            part.InstPart(ParticleManager.PartNames.heal, transform.position);
            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.heal);       // å¯â âπçƒê∂

            Destroy(gameObject);        // çÌèú
        }
    }
}
