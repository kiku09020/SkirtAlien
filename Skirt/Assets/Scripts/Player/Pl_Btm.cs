using UnityEngine;

/* ★プレイヤーの下部に敵がいるかどうかを確認するスクリプトです
 * ・捕食ができるかどうか　などを確認するために必要 */
public class Pl_Btm : MonoBehaviour
{
    /* コンポーネント取得用 */
    Pl_States st;
    Pl_Action act;

//-------------------------------------------------------------------

    void Start()
    {
        GameObject pl_obj = transform.parent.gameObject;
        st = pl_obj.GetComponent<Pl_States>();
        act = pl_obj.GetComponent<Pl_Action>();
    }

    //-------------------------------------------------------------------

    void OnTriggerStay2D(Collider2D col)
    {
        // 敵
        if (col.tag == "Enemy" ){
            // 攻撃したら、敵を消す
            if(st.nowState == Pl_States.States.eating){
                act.AddEatCnt();
                
                Destroy(col.gameObject);
            }
        }

        // landing
        if(col.tag == "Floor") {
            st.lndFlg = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Floor") {
            st.lndFlg = false;
		}
    }
    
}
