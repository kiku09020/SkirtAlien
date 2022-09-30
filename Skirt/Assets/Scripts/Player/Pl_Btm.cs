using UnityEngine;

/* ★プレイヤーの下部に敵がいるかどうかを確認するスクリプトです
 * ・捕食ができるかどうか　などを確認するために必要 */
public class Pl_Btm : MonoBehaviour
{
    /* コンポーネント取得用 */
    Pl_States st;
    Pl_Action act;

    AudioManager aud;
    ParticleManager part;
    PlayerAnim anim;

//-------------------------------------------------------------------
    void Start()
    {
        GameObject pl_obj   = transform.parent.gameObject;

        GameObject gmObj    = GameObject.Find("GameManager");
        GameObject audObj   = gmObj.transform.Find("AudioManager").gameObject;
        GameObject partObj  = gmObj.transform.Find("ParticleManager").gameObject;

        st   = pl_obj.GetComponent<Pl_States>();
        act  = pl_obj.GetComponent<Pl_Action>();
        anim = pl_obj.GetComponent<PlayerAnim>();
        aud  = audObj.GetComponent<AudioManager>();
        part = partObj.GetComponent<ParticleManager>();
    }

    //-------------------------------------------------------------------
    void OnTriggerStay2D(Collider2D col)
    {
        // 敵
        if (col.tag == "Enemy" ){
            // 攻撃したら、敵を消す
            if (st.nowState == Pl_States.States.eating){
                act.AddEatCnt();            // 捕食中の敵の数追加
                aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.eatEnmy);        // 効果音
                part.InstPart(ParticleManager.PartNames.eat, col.transform.position);       // パーティクル

                col.enabled = false;
                anim.EatenEnmy(col.gameObject,0.2f);
                Destroy(col.gameObject, 0.2f);
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
