using UnityEngine;

/* ★ゴール関連のスクリプトです */
public class Goal_Ctrl : MonoBehaviour
{
    /* コンポーネント取得用 */
    GameManager gm;
    CanvasGenelator cvsGen;
    //-------------------------------------------------------------------
    void Start()
    {
        GameObject gm_obj = GameObject.Find("GameManager");
        GameObject um_obj = GameObject.Find("UIManager");

        /* コンポーネント取得 */
        gm = gm_obj.GetComponent<GameManager>();
        cvsGen = um_obj.GetComponent<CanvasGenelator>();
    }

    //-------------------------------------------------------------------
	// ゴール時
	private void OnTriggerEnter2D(Collider2D col)
	{
        if (col.tag == "Player") {
            gm.isGoaled = true;                // ゴール
            cvsGen.Inst_Goal();   
        }
	}
}
