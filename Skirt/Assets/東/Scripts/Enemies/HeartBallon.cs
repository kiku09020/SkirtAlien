using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartBallon : MonoBehaviour
{
    [SerializeField] float movespeed = 0.01f;       // ���x

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
        // �㏸
        transform.Translate(0, movespeed, 0);
    }

    // ����������
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") {
            hp.HP_Heal();

            aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.heal);       // ���ʉ��Đ�

            Destroy(gameObject);        // �폜
        }
    }

    // ��܂ōs���������
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
