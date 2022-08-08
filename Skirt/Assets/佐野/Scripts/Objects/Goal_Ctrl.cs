using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* ★ゴール関連のスクリプトです */
public class Goal_Ctrl : MonoBehaviour
{
    /* 値 */


    /* フラグ */
    public bool isGoaled;
    bool onceFlag;

    /* オブジェクト */
    GameObject pl_obj;
    GameObject gm_obj;
    GameObject um_obj;

    /* コンポーネント取得用 */
    Player pl;
    Pl_States pl_st;
    GameManager gm;
    CanvasGenelator cvsGen;
    //-------------------------------------------------------------------

    void Start()
    {
        pl_obj = GameObject.Find("Player");
        gm_obj = GameObject.Find("GameManager");
        um_obj = GameObject.Find("UIManager");

        /* コンポーネント取得 */
        pl = pl_obj.GetComponent<Player>();
        pl_st = pl_obj.GetComponent<Pl_States>();

        gm = gm_obj.GetComponent<GameManager>();
        cvsGen = um_obj.GetComponent<CanvasGenelator>();

        /* 初期化 */
        isGoaled = false;
    }

//-------------------------------------------------------------------

	// ゴール時
	private void OnTriggerEnter2D(Collider2D col)
	{
        if (col.tag == "Player")
        {
            isGoaled = true;                // ゴール

            pl_st.stateNum = Pl_States.States.goaled;
            cvsGen.Inst_Goal();
        }

	}

    //-------------------------------------------------------------------
}
