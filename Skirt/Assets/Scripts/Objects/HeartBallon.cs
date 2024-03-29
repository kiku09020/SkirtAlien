using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBallon : BallonClass
{
    Pl_HP hp;

    void Start()
    {
        GameObject plObj = GameObject.Find("Player");
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

            Destroy(gameObject);        // 削除
        }
    }
}
