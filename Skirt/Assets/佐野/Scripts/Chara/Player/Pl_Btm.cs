using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pl_Btm : MonoBehaviour
{
    /* 値 */

    /* フラグ */

    /* オブジェクト */
    GameObject pl_obj;

    /* コンポーネント取得用 */
    Player pl;
    Pl_States pl_st;

//-------------------------------------------------------------------

    void Start()
    {
        pl_obj = GameObject.Find("Player");
        pl = pl_obj.GetComponent<Player>();
        pl_st = pl_obj.GetComponent<Pl_States>();
    }

    //-------------------------------------------------------------------

    void OnTriggerStay2D(Collider2D col)
    {
        // 敵
        if (col.tag == "Enemy"){
            Debug.Log("敵");

            // 攻撃したら消える(仮)
            if (pl_st.isAttacking){
                Destroy(col.gameObject);
            }
        }
    }

}
