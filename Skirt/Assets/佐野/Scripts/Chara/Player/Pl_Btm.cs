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
    Player pl;
    Pl_States pl_st;
    Pl_HP pl_hp;

//-------------------------------------------------------------------

    void Start()
    {
        pl_obj = GameObject.Find("Player");
        pl = pl_obj.GetComponent<Player>();
        pl_st = pl_obj.GetComponent<Pl_States>();
        pl_hp = pl_obj.GetComponent<Pl_HP>();
    }

    //-------------------------------------------------------------------

    void OnTriggerStay2D(Collider2D col)
    {
        // 敵
        if (col.tag == "Enemy"){
            Debug.Log("敵");

            // 攻撃したら消える(仮)
            if (pl_st.isAttacking){
                pl_hp.nowHP += pl_hp.heal;
                Destroy(col.gameObject);
            }
        }
    }

}
