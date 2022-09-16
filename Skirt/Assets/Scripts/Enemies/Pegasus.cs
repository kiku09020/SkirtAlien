using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pegasus : MonoBehaviour
{
    [SerializeField] float moveSpeed;       // 動く速度
    [SerializeField] float moveDist;        // 動く距離

    Vector2 posInit;                        // 初期位置

    GameObject plObj;

    Rigidbody2D rb;

    //-------------------------------------------------------------------
    void Start()
    {
        plObj = GameObject.FindGameObjectWithTag("Player");

        rb = GetComponent<Rigidbody2D>();

        posInit = transform.position;       // 初期位置を保存
    }

    void FixedUpdate()
    {
        // 現在位置取得
        var posNow = transform.position;
        var plPos = plObj.transform.position;

        // 現在位置が上の頂点より下にいる時、上に移動
        if (posNow.y < posInit.y + moveDist) {
            rb.AddForce(Vector2.up * moveSpeed);
        }

        // 現在位置が下の頂点よりも上にいる時、下に移動
        else if (posNow.y > posInit.y - moveDist) {
            rb.AddForce(Vector2.down * moveSpeed);
        }

        if (posNow.x < plPos.x) {
            transform.localScale = new Vector2(-1, 1);
        }

        else {
            transform.localScale = Vector2.one;
        }
    }
}
