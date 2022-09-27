using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pegasus : MonoBehaviour {
    [SerializeField] float moveSpeed;       // �������x
    [SerializeField] float moveDist;        // ��������

    Vector2 posInit;                        // �����ʒu

    GameObject plObj;

    Rigidbody2D rb;

    //-------------------------------------------------------------------
    void Start()
    {
        plObj = GameObject.FindGameObjectWithTag("Player");

        rb = GetComponent<Rigidbody2D>();

        posInit = transform.position;       // �����ʒu��ۑ�
    }

    void FixedUpdate()
    {
        // ���݈ʒu�擾
        var posNow = transform.position;
        var plPos = plObj.transform.position;

        Move(posNow);
        Flip(posNow, plPos);
    }
    
    // �ړ�
    void Move(Vector2 posNow)
	{
        // ���݈ʒu����̒��_��艺�ɂ��鎞�A��Ɉړ�
        if (posNow.y < posInit.y + moveDist) {
            rb.AddForce(Vector2.up * moveSpeed);
        }

        // ���݈ʒu�����̒��_������ɂ��鎞�A���Ɉړ�
        else if (posNow.y > posInit.y - moveDist) {
            rb.AddForce(Vector2.down * moveSpeed);
        }
    }

    // ���]
    void Flip(Vector2 posNow,Vector2 plPos)
	{
        // ���]
        if (posNow.x < plPos.x) {
            transform.localScale = new Vector2(-1, 1);
        }

        else {
            transform.localScale = Vector2.one;
        }
    }
}
