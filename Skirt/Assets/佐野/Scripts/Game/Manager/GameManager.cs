using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    /* 値 */
    [Header("シーン関係")]
    static public string scene_name;    // 現在のシーン名を取得
    public int scene_index;             // 現在のシーン番号を取得

    [Header("ステージ関係")]
    public float stg_length;            // ステージの長さ

    public float stg_drag;              // ステージの空気抵抗(仮)
    public float stg_grav;              // ステージの重力

    /* フラグ */
    bool onceFlag;

    [Header("テキスト")]/* テキスト */
    [SerializeField] Text txt_goal;     // ゴール時のテキスト

    /* オブジェクト */
    GameObject  pl_obj;
    GameObject  goal_obj;
    GameObject  parent;
    GameObject hpGauge_obj;

    /* コンポーネント取得用 */
    Player              pl;
    Btn_Ctrl   btn_ctrl;
    Goal_Ctrl      goal;
    Pl_HP          hpGauge;


//-------------------------------------------------------------------

    void Start()
    {
        /* オブジェクト検索 */
        pl_obj      = GameObject.Find("Player");
        goal_obj    = GameObject.Find("Goal");
        hpGauge_obj = GameObject.Find("HPGauge");

        parent      = GameObject.Find("Canvas");

        /* コンポーネント取得 */
        btn_ctrl    = GetComponent<Btn_Ctrl>();

        pl          = pl_obj.GetComponent<Player>();
        goal        = goal_obj.GetComponent<Goal_Ctrl>();
        hpGauge     = hpGauge_obj.GetComponent<Pl_HP>();

        /* 初期化 */
        scene_name  = SceneManager.GetActiveScene().name;
        scene_index = SceneManager.GetActiveScene().buildIndex;
    }

//-------------------------------------------------------------------

    void Update()
    {
        Goaled();
    }

//-------------------------------------------------------------------

    // ステージごとの処理
    void Stage()
    {
        switch (scene_index){
            case 1: break;
        }
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
        SceneManager.LoadScene(scene_index + 1);
	}
}
