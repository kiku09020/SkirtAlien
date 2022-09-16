using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★〇〇に関するスクリプトです */
//-------------------------------------------------------------------
public class BallonClass : MonoBehaviour
{
    /* 値 */
    [SerializeField] float spd = 0.1f;       // 速度
    [SerializeField] float plPosDist = 50;       // プレイヤーとの距離

    Vector2 plPos;
    Vector2 blPos;

    GameObject plObj;

    //-------------------------------------------------------------------
    void Awake()
    {
        plObj = GameObject.Find("Player");
    }

    void Update()
    {
        plPos = plObj.transform.position;
        blPos = transform.position;
    }

    // 上昇
    public void Up()
	{
        var dist = Vector2.Distance(plPos, blPos);      // 距離

        // プレイヤーが近くにいるときのみ上昇
        if (dist < plPosDist) {
            transform.Translate(0, spd, 0);
        }

        // プレイヤーより上に行ってたら削除
        else if (plPos.y < blPos.y) {
            Destroy(gameObject);
        }
    }

    // プレイヤーの位置に合わせて、方向を合わせる
    public void Direction()
    {
        if (plPos.x < blPos.x) {
            transform.localScale = Vector2.one;
        }

        else {
            transform.localScale = new Vector2(-1, 1);
        }
    }
}
