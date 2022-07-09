using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Ctrl : MonoBehaviour
{
    /* 値 */
    [SerializeField] int bg_size        = 50;       // 背景サイズ

    /* フラグ */


    /* オブジェクト */
    [SerializeField] GameObject bg;             // 背景
    [SerializeField]Transform bg_parent;

    /* コンポーネント取得用 */
    GameManager gm;


//-------------------------------------------------------------------

    void Start()
    {
        /* オブジェクト検索 */


        /* コンポーネント取得 */
        gm = GetComponent<GameManager>();


        /* 初期化 */

        // 背景サイズ
        bg.transform.localScale = new Vector2(bg_size, bg_size);

        // ステージの長さに合わせて、背景を複製する
        for(int i = 0; i < (gm.stg_length / bg_size) + 1; i++) {
            Instantiate(bg, new Vector3(0, i * bg_size, 0), 
                        Quaternion.identity, bg_parent);

		}
    }

//-------------------------------------------------------------------

    void Update()
    {

    }

//-------------------------------------------------------------------

}
