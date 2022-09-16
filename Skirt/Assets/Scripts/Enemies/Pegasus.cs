using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pegasus : MonoBehaviour
{
    [SerializeField] float moveSpeed;       // �������x
    [SerializeField] float moveDist;        // ��������

    Vector2 posInit;                        // �����ʒu

    Rigidbody2D rb;

    //-------------------------------------------------------------------
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        posInit = transform.position;       // �����ʒu��ۑ�
    }

    void FixedUpdate()
    {
        // ���݈ʒu�擾
        Vector2 posNow = transform.position;

        // ���݈ʒu����̒��_��艺�ɂ��鎞�A��Ɉړ�
        if (posNow.y < posInit.y + moveDist) {
            rb.AddForce(Vector2.up * moveSpeed);
        }

        // ���݈ʒu�����̒��_������ɂ��鎞�A���Ɉړ�
        else if (posNow.y > posInit.y - moveDist) {
            rb.AddForce(Vector2.down * moveSpeed);
        }
    }
}
