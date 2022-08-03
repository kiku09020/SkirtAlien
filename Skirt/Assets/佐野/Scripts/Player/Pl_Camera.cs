using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★カメラに関するスクリプトです */
public class Pl_Camera : MonoBehaviour
{
    /* 値 */
    public float scrnWidthWld;
    public float scrnHeightWld;
    public float scrnYWld;

    /* フラグ */

    /* オブジェクト */
    GameObject pl_obj;
    GameObject gm_obj;

    /* コンポーネント取得用 */
    Camera cam;
    Player pl;
    Pl_States pl_st;
    GameManager gm;
    StageManager stg;

//-------------------------------------------------------------------

    void Start()
    {
        /* コンポーネント取得 */
        pl_obj = GameObject.Find("Player");
        gm_obj = GameObject.Find("GameManager");

        cam = GetComponent<Camera>();
        pl = pl_obj.GetComponent<Player>();
        pl_st = pl_obj.GetComponent<Pl_States>();
        gm = gm_obj.GetComponent<GameManager>();
        stg = gm_obj.GetComponent<StageManager>();

        /* 初期化 */
        transform.position = new Vector3(0, stg.stg_length, -10);
    }

//-------------------------------------------------------------------

    void Update()
	{
        if (!gm.isGameOver && pl_st.stateNum != Pl_States.States.goaled) {
            // y座標のみ追従
            transform.position = new Vector3(transform.position.x, pl_obj.transform.position.y, transform.position.z);

            // カメラのワールド座標を取得
            scrnWidthWld = cam.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x;
            scrnHeightWld = cam.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
            scrnYWld = cam.ViewportToWorldPoint(new Vector2(0, cam.rect.y)).y;
        }
    }

//-------------------------------------------------------------------
}
