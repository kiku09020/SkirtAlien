using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★背景をステージの長さに合わせて生成するスクリプトです */
public class BG_Ctrl : MonoBehaviour
{
    /* 値 */
    float scrlSpd;
    [SerializeField] float spd;

    /* フラグ */

    /* オブジェクト */
    GameObject pl_obj;
    GameObject cam_obj;
    GameObject gm_obj;

    /* コンポーネント取得用 */
    Renderer rend;
    Pl_Action pl;
    Pl_States plst;
    Pl_Camera cam;
    GameManager gm;
    StageManager stg;

//-------------------------------------------------------------------

    void Start()
    {
        /* オブジェクト検索 */
        gm_obj = GameObject.Find("GameManager");
        pl_obj = GameObject.Find("Player");
        cam_obj = GameObject.Find("PlayerCamera");

        /* コンポーネント取得 */
        rend = GetComponent<Renderer>();
        pl = pl_obj.GetComponent<Pl_Action>();
        plst = pl_obj.GetComponent<Pl_States>();
        cam = cam_obj.GetComponent<Pl_Camera>();
        gm = gm_obj.GetComponent<GameManager>();
        stg = gm_obj.GetComponent<StageManager>();

        /* 初期化 */
    }

    //-------------------------------------------------------------------

    void LateUpdate()
    {
        transform.position = new Vector2(0, cam.transform.position.y);

        // ゲームオーバー時、ゴール時は追尾しない
        if (!gm.isGameOver && plst.stateNum != Pl_States.States.goaled) {
            // 速度
            scrlSpd = stg.stg_length - pl.transform.position.y;
            float y = Mathf.Repeat(-scrlSpd * spd / 100, 1);

            // オフセット
            Vector2 ofst = new Vector2(0, y);

            rend.sharedMaterial.SetTextureOffset("_MainTex", ofst);
        }
    }

//-------------------------------------------------------------------

}
