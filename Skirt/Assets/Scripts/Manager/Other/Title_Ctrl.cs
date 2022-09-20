using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/* ★タイトル関連のスクリプトです */
public class Title_Ctrl : MonoBehaviour
{
    [SerializeField] float sceneChangeSec;      // シーンが移動するまでの秒数
    [SerializeField] float tapTimeLim;          // 長押しタイマーの制限時間
    float tapTimer;                             // 長押しタイマー
    bool isTapping;

    /* コンポーネント */
    Animator anim;
	//-------------------------------------------------------------------

	void Start()
	{
        GameObject cnvs = GameObject.Find("Canvas");
        anim = cnvs.GetComponent<Animator>();
	}

    void FixedUpdate()
    {
        if (isTapping) {
            if (tapTimer > tapTimeLim) {
                tapTimer = 0;
                isTapping = false;
                StartCoroutine("SceneChange_Start");
            }

            tapTimer += Time.deltaTime;
        }

        QuitGame();
    }

    //-------------------------------------------------------------------

    // 長押し開始
    public void TapStart()
    {
        isTapping = true;
    }

    // 長押し終了
    public void TapEnd()
    {
        isTapping = false;
        tapTimer = 0;
    }

    // スタート時のコルーチン
    IEnumerator SceneChange_Start()
	{
        anim.SetTrigger("isClicked");

        yield return new WaitForSeconds(sceneChangeSec);
        SceneManager.LoadScene("Stage1");
    }

    //-------------------------------------------
    // デバッグステージ
    public void Btn_Dbg()
    {
        SceneManager.LoadScene("DebugStage");
    }

    // バックボタン押したとき
    void QuitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {                 // App
            Application.Quit();
        }
    }
}
