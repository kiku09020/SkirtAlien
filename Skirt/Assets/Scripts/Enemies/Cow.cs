using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★牛のスクリプトです */
public class Cow : MonoBehaviour
{
    /* 値 */
    [SerializeField] float spd;     // 速度

    /* コンポーネント取得用 */
    PlayerCamera cam;

//-------------------------------------------------------------------
    void Start()
    {
        /* コンポーネント取得 */
        GameObject cam_obj = GameObject.Find("PlayerCamera");
        cam = cam_obj.GetComponent<PlayerCamera>();
    }

//-------------------------------------------------------------------
    void FixedUpdate()
    {
        // 移動
        transform.Translate(new Vector2(spd, 0));       

        // 右端に行ったら左端から出てくる
        if (transform.position.x > cam.scrnWidthWld){
            transform.position = new Vector2(-cam.scrnWidthWld, transform.position.y);
		}
    }

//-------------------------------------------------------------------

}
