using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pegasus : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 posInit;        // �����ʒu
    Vector2 posNow;         // ���݂̈ʒu

    [SerializeField] float moveSpeed = 5f;      // �������x
    [SerializeField] float moveDist = 5;    // ��������

    float time;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // �����ʒu��ۑ�
        posInit = transform.position;
    }


    void FixedUpdate()
    {
        // ���݈ʒu�擾
        posNow = transform.position;

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
