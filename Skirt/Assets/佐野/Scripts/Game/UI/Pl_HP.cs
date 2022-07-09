using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pl_HP : MonoBehaviour
{
    /* 値 */


    /* フラグ */


    /* オブジェクト */

    /* コンポーネント取得用 */
    Image hp_Image;


    //-------------------------------------------------------------------

    void Start()
    {
        hp_Image = GetComponent<Image>();

        // 初期化
        hp_Image.fillAmount = 1;
    }

    // HPセット
    public void HP_Set(float hp_max,float hp_now)
    {
        hp_Image.fillAmount = hp_now / hp_max;
    }
}
