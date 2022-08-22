using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★牛のスクリプトです */
public class Cow : MonoBehaviour
{
    /* 値 */
    [SerializeField] float spd = 0.1f;

    /* フラグ */


    /* オブジェクト */
    GameObject cam_obj;

    /* コンポーネント取得用 */
    Pl_Camera cam;


//-------------------------------------------------------------------

    void Start()
    {
        cam_obj = GameObject.Find("PlayerCamera");
        /* コンポーネント取得 */
        cam = cam_obj.GetComponent<Pl_Camera>();

        /* 初期化 */
        
    }

//-------------------------------------------------------------------

    void FixedUpdate()
    {
        transform.Translate(new Vector2(spd, 0));

        if (transform.position.x > cam.scrnWidthWld){
            transform.position = new Vector2(-cam.scrnWidthWld, transform.position.y);
		}
    }

//-------------------------------------------------------------------

}
