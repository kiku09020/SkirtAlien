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
    Pl_States st;
    Pl_Hunger hung;

//-------------------------------------------------------------------

    void Start()
    {
        pl_obj = transform.parent.gameObject;
        st = pl_obj.GetComponent<Pl_States>();
        hung = pl_obj.GetComponent<Pl_Hunger>();
    }

    //-------------------------------------------------------------------

    void OnTriggerStay2D(Collider2D col)
    {
        // 敵
        if (col.tag == "Enemy" ){
            // 攻撃したら消える
            if(st.stateNum == Pl_States.States.eating){
                st.stateNum = Pl_States.States.digest;      // 消化状態にする
                
                Destroy(col.gameObject);
            }
        }

        // 床
        if(col.tag == "Floor") {
            st.landFlg = true;

            // 地上状態
            if( st.stateNum == Pl_States.States.normal ||
                st.stateNum == Pl_States.States.floating) {
                st.stateNum = Pl_States.States.landing;
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Floor") {
            st.landFlg = false;

            // 通常状態
            if( st.stateNum != Pl_States.States.jumping && 
                st.stateNum != Pl_States.States.damage &&
                st.stateNum!=Pl_States.States.digest) {
                st.stateNum = Pl_States.States.normal;
            }
		}
    }
    
}
