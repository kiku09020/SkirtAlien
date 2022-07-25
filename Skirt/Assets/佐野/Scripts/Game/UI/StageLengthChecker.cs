using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageLengthChecker : MonoBehaviour
{
    /* 値 */


    /* フラグ */


    /* オブジェクト */
    GameObject sldr_obj;
    GameObject pl_obj;
    GameObject gm_obj;

    /* コンポーネント取得用 */
    Slider sldr;
    Player pl;
    GameManager gm;


//-------------------------------------------------------------------

    void Start()
    {
        sldr_obj = GameObject.Find("StageSlider");
        pl_obj = GameObject.Find("Player");
        gm_obj = GameObject.Find("GameManager");

        /* コンポーネント取得 */
        sldr = sldr_obj.GetComponent<Slider>();
        pl = pl_obj.GetComponent<Player>();
        gm = gm_obj.GetComponent<GameManager>();

        /* 初期化 */
        
    }

//-------------------------------------------------------------------

    void Update()
    {
        sldr.value = pl.gameObject.transform.position.y / gm.stg_length;
    }

//-------------------------------------------------------------------

}
