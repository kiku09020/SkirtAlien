using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/* ★タイトル関連のスクリプトです */
public class Title_Ctrl : MonoBehaviour
{
    [SerializeField] float sceneChangeSec;      // シーンが移動するまでの秒数

    /* コンポーネント */
    Animator anim;
	//-------------------------------------------------------------------

	void Start()
	{
        GameObject cnvs = GameObject.Find("Canvas");
        anim = cnvs.GetComponent<Animator>();
	}

    //-------------------------------------------------------------------

    // スタートボタン
    public void Btn_Start()
    {
        StartCoroutine("SceneChange_Start");
    }

    // デバッグステージ
    public void Btn_Dbg()
    {
        SceneManager.LoadScene("DebugStage");
    }

    // スタート時のコルーチン
    IEnumerator SceneChange_Start()
	{
        anim.SetTrigger("isClicked");

        yield return new WaitForSeconds(sceneChangeSec);
        SceneManager.LoadScene("Stage1");
    }
}
