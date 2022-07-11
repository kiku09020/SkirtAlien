using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    /* 値 */
    [Header("シーン関係")]
    const int sceneCnt = 10;        // シーンの数
    string[] sceneNames;            // 各シーン名の配列
    public string nowSceneName;     // 現在のシーン名
    public int sceneIndex;                 // 現在のシーン番号を取得

    [Header("ステージ関係")]
    const int stgCnt = 4;           // ステージの数
    string[] stgNames;              // 各ステージ名の配列
    public string nowStgName;       // 現在のステージ名

    public float stg_length;        // ステージの長さ
    public float stg_drag;          // ステージの空気抵抗(仮)
    public float stg_grav;          // ステージの重力

    [Header("スマホ用の値")]
    [SerializeField] Joystick stick;    // スティック
    public float inpVer, inpHor;      // スティックの入力値
    public float inpVerOld, inpHorOld;

    /* フラグ */
    bool onceFlag;

    [Header("テキスト")]/* テキスト */
    [SerializeField] Text txt_goal;     // ゴール時のテキスト

    /* オブジェクト */
    GameObject  goal_obj;
    GameObject  parent;

    /* コンポーネント取得用 */
    Btn_Ctrl    btn_ctrl;
    Goal_Ctrl   goal;

//-------------------------------------------------------------------

    void Start()
    {
        /* オブジェクト検索 */
        goal_obj    = GameObject.Find("Goal");
        parent      = GameObject.Find("Canvas");

        /* コンポーネント取得 */
        btn_ctrl    = GetComponent<Btn_Ctrl>();
        goal        = goal_obj.GetComponent<Goal_Ctrl>();

        /* 初期化 */


        Stage();
        Scene();
    }

//-------------------------------------------------------------------

    void Update()
    {
        Goaled();

        // 入力値
        inpVerOld = inpVer; inpHorOld = inpHor;

        inpVer = stick.Horizontal;
        inpHor = stick.Vertical;
    }

//-------------------------------------------------------------------

    void Scene()
	{
        sceneIndex = SceneManager.GetActiveScene().buildIndex;

        sceneNames = new string[10] { "Title", "Menu", "Stage1", "Stage2", "Stage3", "BossStage", "Clear", "Tutorial", "Debug", "" };
        nowSceneName = sceneNames[sceneIndex];
    }

    // ステージごとの処理
    void Stage()
    {
        // ステージ名
        
        stgNames = new string[stgCnt] { "ステージ1", "ステージ2", "ステージ3", "ボスステージ" };
    }

    // ゴール時
    void Goaled()
	{
        if(goal.isGoaled) {
            if(!onceFlag) {
                onceFlag = true;

                // スティック非表示
                btn_ctrl.stick.SetActive(false);

                // テキスト表示
                Instantiate(txt_goal, parent.transform.position, Quaternion.identity, parent.transform);

                // 待機後、次のステージへ
                StartCoroutine(GotoNextStage());
            }
        }
    }

    // 次のステージへ
    IEnumerator GotoNextStage()
	{
        // 5秒待機
        yield return new WaitForSeconds(5);

        // 次のステージ読み込み
        SceneManager.LoadScene(sceneIndex + 1);
	}
}
