using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Ctrl : MonoBehaviour
{
    /* 値 */


    /* フラグ */


    /* オブジェクト */
    [SerializeField] Text txt_gameover;

    GameObject gm_obj;

    /* コンポーネント取得用 */
    GameManager gm;


//-------------------------------------------------------------------

    void Start()
    {
        /* コンポーネント取得 */
        gm_obj = GameObject.Find("GameManager");

        gm = gm_obj.GetComponent<GameManager>();

        /* 初期化 */
        
    }

//-------------------------------------------------------------------

    void Update()
    {
        
    }

//-------------------------------------------------------------------

    // テキスト生成
    void TextGen()
    {
        if (gm.isGameOver) {

        }
    }
}
