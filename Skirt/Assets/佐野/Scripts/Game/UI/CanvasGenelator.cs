using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/* ★〇〇に関するスクリプトです */
//-------------------------------------------------------------------
public class CanvasGenelator : MonoBehaviour
{
    /* フラグ */
    bool onceStartFlg;      // スタート時のフラグ

    /* オブジェクト */
    // プレハブ
    [SerializeField] GameObject cvsPref_ctrl;
    [SerializeField] GameObject cvsPref_game;
    [SerializeField] GameObject cvsPref_pause;
    [SerializeField] GameObject cvsPref_gameOver;
    [SerializeField] GameObject cvsPref_goal;

    // インスタンス
    GameObject cvsInst_ctrl;
    GameObject cvsInst_game;        // gameUI
    GameObject cvsInst_pause;       // pause
    GameObject cvsInst_gameOver;    // gameOver
    GameObject cvsInst_goal;        // goal

    /* コンポーネント取得用 */
    GameObject gm_obj;
    GameManager gm;

    //-------------------------------------------------------------------

    void Awake()
    {
        /* オブジェクト検索 */
        gm_obj = transform.parent.gameObject;

        /* コンポーネント取得 */
        gm = gm_obj.GetComponent<GameManager>();

        /* 初期化 */
        cvsInst_ctrl = Instantiate(cvsPref_ctrl);
        cvsInst_game = Instantiate(cvsPref_game);
        cvsInst_pause = Instantiate(cvsPref_pause);

        cvsInst_pause.SetActive(false);
    }

    public void SetAct()
	{
        cvsInst_ctrl.SetActive(false);
        cvsInst_game.SetActive(false);
	}

    //-------------------------------------------------------------------

    void FixedUpdate()
    {
        Starting();
    }

    //-------------------------------------------------------------------

    // スタート時
    void Starting()
	{
        if(!gm.isStarting) {
            if(!onceStartFlg) {
                onceStartFlg = true;

                // キャンバス生成
                cvsInst_ctrl.SetActive(true);
                cvsInst_game.SetActive(true);
            }
        }
    }

    // ポーズ時
    public void Pause()
	{
        cvsInst_ctrl.SetActive(true);
        cvsInst_pause.SetActive(false);
        cvsInst_game.SetActive(true);
    }

    // ポーズ解除時
    public void UnPause()
	{
        cvsInst_ctrl.SetActive(false);
        cvsInst_pause.SetActive(true);
        cvsInst_game.SetActive(false);
    }

    // ゲームオーバー時に表示
    public void Inst_GameOver()
    {
        cvsInst_gameOver = Instantiate(cvsPref_gameOver);
        cvsInst_ctrl.SetActive(false);
        cvsInst_game.SetActive(false);
    }

    // ゴール時
    public void Inst_Goal()
	{
        cvsInst_goal = Instantiate(cvsPref_goal);
        cvsInst_ctrl.SetActive(false);
        cvsInst_game.SetActive(false);
	}
}
