using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pegasus : MonoBehaviour
{
    [SerializeField] float moveSpeed;       // 動く速度
    [SerializeField] float moveDist;        // 動く距離

    Vector2 posInit;                        // 初期位置

    Rigidbody2D rb;

    //-------------------------------------------------------------------
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        posInit = transform.position;       // 初期位置を保存
    }

    void FixedUpdate()
    {
        // 現在位置取得
        Vector2 posNow = transform.position;

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
