using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★背景をステージの長さに合わせて生成するスクリプトです */
public class BG_Ctrl : MonoBehaviour
{
    /* 値 */

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
    }

//-------------------------------------------------------------------

    void Update()
    {

    }

//-------------------------------------------------------------------

}
