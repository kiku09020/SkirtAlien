using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★牛のスクリプトです */
public class Cow : MonoBehaviour {
    /* 値 */
    [SerializeField] float moveForce;   // 動力
    [SerializeField] float turnPosX;    // 振り返る位置
    bool isTurned;                      // 振り返ってるか
    int dir = 1;                        // 移動方向

    /* コンポーネント取得用 */
    

//-------------------------------------------------------------------
    void Start()
    {
        /* コンポーネント取得 */
    
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

        // 右折り返し
        if (nowPos.x > turnPosX && !isTurned) {
            isTurned = true;
            dir = -1;
        }

        // 左折り返し
        else if (nowPos.x < -turnPosX && isTurned) {
            isTurned = false;
            dir = 1;
		}

        // 方向に合わせて移動
        transform.Translate(Vector2.right * moveForce * dir);

        // 反転
        transform.localScale = new Vector2(dir, 1);
    }
}
