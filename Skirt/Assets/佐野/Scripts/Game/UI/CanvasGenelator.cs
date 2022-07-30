using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★〇〇に関するスクリプトです */
//-------------------------------------------------------------------
public class CanvasGenelator : MonoBehaviour
{
    /* オブジェクト */
    GameObject goal_obj;
    GameObject cvs_Ctrl;

    /* コンポーネント取得用 */
    // プレハブ
    [SerializeField] GameObject cvsPref_game;
    [SerializeField] GameObject cvsPref_pause;
    [SerializeField] GameObject cvsPref_gameOver;

    // インスタンス
    GameObject cvsInst_game;        // gameUI
    GameObject cvsInst_pause;       // pause
    GameObject cvsInst_gameOver;    // gameOver

    Goal_Ctrl goal;

    //-------------------------------------------------------------------

    void Awake()
    {
        /* オブジェクト検索 */
        cvs_Ctrl = GameObject.Find("ControllerCanvas");

        cvsInst_game = Instantiate(cvsPref_game);
        cvsInst_pause = Instantiate(cvsPref_pause);

        cvsInst_pause.SetActive(false);
        goal_obj = GameObject.Find("Goal");

        /* コンポーネント取得 */
        goal = goal_obj.GetComponent<Goal_Ctrl>();

        /* 初期化 */

    }

//-------------------------------------------------------------------

    void Update()
    {
        if(goal.isGoaled) {
            Goaled();
        }
    }

    //-------------------------------------------------------------------

    void Goaled()
    {
        cvs_Ctrl.SetActive(true);
        cvsInst_game.SetActive(true);
    }

    // ポーズ時
    public void Pause()
	{
        cvs_Ctrl.SetActive(true);
        cvsInst_pause.SetActive(false);
        cvsInst_game.SetActive(true);
    }

    // ポーズ解除時
    public void UnPause()
	{
        cvs_Ctrl.SetActive(false);
        cvsInst_pause.SetActive(true);
        cvsInst_game.SetActive(false);
    }

    // ゲームオーバー時に表示
    public void Inst_GameOver()
    {
        cvsInst_gameOver = Instantiate(cvsPref_gameOver);
    }
}
