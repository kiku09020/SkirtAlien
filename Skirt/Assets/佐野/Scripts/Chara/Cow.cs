using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Update()
    {
        transform.Translate(new Vector2(spd, 0));

        if (transform.position.x > cam.scrn_EdgeX){
            transform.position = new Vector2(-cam.scrn_EdgeX, transform.position.y);
		}
    }

//-------------------------------------------------------------------

}
