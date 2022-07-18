using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* ★タイトル関連のスクリプトです */
public class Title_Ctrl : MonoBehaviour
{
    /* 値 */


    /* フラグ */


    /* オブジェクト */


    /* コンポーネント取得用 */



//-------------------------------------------------------------------

    void Start()
    {
        /* コンポーネント取得 */


        /* 初期化 */
        
    }

//-------------------------------------------------------------------

    void Update()
    {
        
    }

    //-------------------------------------------------------------------

    // スタートボタン
    public void Btn_Start()
    {
        SceneManager.LoadScene("Stage1");
    }

}
