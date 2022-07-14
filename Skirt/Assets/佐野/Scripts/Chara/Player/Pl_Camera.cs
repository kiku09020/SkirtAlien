using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★カメラに関するスクリプトです */
public class Pl_Camera : MonoBehaviour
{
    /* 値 */
    public float scrn_EdgeX;

    /* フラグ */

    /* オブジェクト */
    GameObject pl_obj;

    /* コンポーネント取得用 */
    Camera cam;
    Player pl;


//-------------------------------------------------------------------

    void Start()
    {
        /* コンポーネント取得 */
        pl_obj = GameObject.Find("Player");

        cam = GetComponent<Camera>();
        pl = pl_obj.GetComponent<Player>();
        /* 初期化 */

    }

//-------------------------------------------------------------------

    void Update()
	{
		if(!pl.isGameOver) {

        // y座標のみ追従
        transform.position = new Vector3(transform.position.x, pl_obj.transform.position.y, transform.position.z);

        // カメラのワールド座標を取得
        scrn_EdgeX  = cam.ScreenToWorldPoint(new Vector2(Screen.width,0)).x;
		}
    }

//-------------------------------------------------------------------
}
