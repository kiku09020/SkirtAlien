using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★プレイヤーの下部に敵がいるかどうかを確認するスクリプトです
 * ・捕食ができるかどうか　などを確認するために必要 */
public class Pl_Btm : MonoBehaviour
{
    /* 値 */

    /* フラグ */

    /* オブジェクト */
    GameObject pl_obj;

    /* コンポーネント取得用 */
    Pl_States pl_st;
    Pl_HP pl_hp;

//-------------------------------------------------------------------

    void Start()
    {
        pl_obj = transform.parent.gameObject;
        pl_st = pl_obj.GetComponent<Pl_States>();
        pl_hp = pl_obj.GetComponent<Pl_HP>();
    }

    //-------------------------------------------------------------------

    void OnTriggerStay2D(Collider2D col)
    {
        // 敵
        if (col.tag == "Enemy"){

            // 攻撃したら消える(仮)
            if(pl_st.stateNum == Pl_States.States.attacking){
                pl_hp.HP_Heal();
                Destroy(col.gameObject);
            }
        }
    }
}
