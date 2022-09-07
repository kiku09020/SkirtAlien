using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBallon : BallonClass
{
    Pl_HP hp;
    AudioManager aud;
    Pl_Particle part;

    void Start()
    {
        GameObject partObj = GameObject.Find("ParticleManager");
        GameObject audObj = GameObject.Find("AudioManager");
        GameObject plObj = GameObject.Find("Player");

        part = partObj.GetComponent<Pl_Particle>();
        aud = audObj.GetComponent<AudioManager>();
        hp = plObj.GetComponent<Pl_HP>();
    }

    void FixedUpdate()
    {
        Up();
    }

    // 当たった時
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") {
            hp.HP_Heal();

            part.InstPart(Pl_Particle.PartNames.heal, transform.position);
            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.heal);       // 効果音再生

            Destroy(gameObject);        // 削除
        }
    }
}
