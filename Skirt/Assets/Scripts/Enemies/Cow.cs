using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★牛のスクリプトです */
public class Cow : Enemy
{
    /* 値 */
    [SerializeField] float maxSpd;      // 速度
    [SerializeField] float moveForce;   // 動力
    [SerializeField] float decForce;    // 減速度
    [SerializeField] float turnPosX;    // 振り返る位置
    bool isTurned;                      // 振り返ってるか
    int dir = 1;                        // 移動方向

    /* コンポーネント取得用 */
    Rigidbody2D rb;

//-------------------------------------------------------------------
    void Start()
    {
        /* コンポーネント取得 */
        rb = GetComponent<Rigidbody2D>();
    }

    //-------------------------------------------------------------------
    void FixedUpdate()
    {
        Move();
    }

    //-------------------------------------------------------------------

    void Move()
    {
        var nowPos = transform.position;
        var nowSpd = rb.velocity;

        // 右折り返し
        if (nowPos.x > turnPosX && !isTurned) {
            isTurned = true;
            dir = -1;
            rb.velocity = nowSpd * decForce;
        }

        // 左折り返し
        else if (nowPos.x < -turnPosX && isTurned) {
            isTurned = false;
            dir = 1;
            rb.velocity = nowSpd * decForce;
		}

        // 方向に合わせて移動
		if (nowSpd.x < maxSpd) {
            rb.AddForce(Vector2.right * moveForce * dir);
		}

        // 反転
        transform.localScale = new Vector2(dir, 1);
    }
}
