using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pegasus : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 posInit;        // 初期位置
    Vector2 posNow;         // 現在の位置

    [SerializeField] float moveSpeed = 5f;      // 動く速度
    [SerializeField] float moveDist = 5;    // 動く距離

    float time;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 初期位置を保存
        posInit = transform.position;
    }


    void FixedUpdate()
    {
        // 現在位置取得
        posNow = transform.position;

        // 現在位置が上の頂点より下にいる時、上に移動
        if (posNow.y < posInit.y + moveDist) {
            rb.AddForce(Vector2.up * moveSpeed);
        }

        // 現在位置が下の頂点よりも上にいる時、下に移動
        else if (posNow.y > posInit.y - moveDist) {
            rb.AddForce(Vector2.down * moveSpeed);
        }
    }
}
