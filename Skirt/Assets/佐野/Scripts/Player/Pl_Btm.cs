using UnityEngine;

/* ★プレイヤーの下部に敵がいるかどうかを確認するスクリプトです
 * ・捕食ができるかどうか　などを確認するために必要 */
public class Pl_Btm : MonoBehaviour
{
    /* コンポーネント取得用 */
    Pl_States st;

//-------------------------------------------------------------------

    void Start()
    {
        GameObject pl_obj = transform.parent.gameObject;
        st = pl_obj.GetComponent<Pl_States>();
    }

    //-------------------------------------------------------------------

    void OnTriggerStay2D(Collider2D col)
    {
        // 敵
        if (col.tag == "Enemy" ){
            // 攻撃したら、敵を消す
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
            if( st.stateNum != Pl_States.States.damage &&
                st.stateNum!=Pl_States.States.digest) {
                st.stateNum = Pl_States.States.normal;
            }
		}
    }
    
}
