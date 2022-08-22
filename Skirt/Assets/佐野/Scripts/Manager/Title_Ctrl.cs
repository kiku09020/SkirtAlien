using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

/* ★タイトル関連のスクリプトです */
public class Title_Ctrl : MonoBehaviour
{
    [SerializeField] float sceneChangeSec;      // シーンが移動するまでの秒数

    /* コンポーネント */
    GameObject cnvs;
    Animator anim;
	//-------------------------------------------------------------------

	void Start()
	{
        cnvs = GameObject.Find("Canvas");
        anim = cnvs.GetComponent<Animator>();
	}

	void Update()
    {

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
        StartCoroutine("SceneChange_Debug");
	}

    // スタート時のコルーチン
    IEnumerator SceneChange_Start()
	{
        anim.SetTrigger("isClicked");

        yield return new WaitForSeconds(sceneChangeSec);
        SceneManager.LoadScene("Stage1");
    }

    // デバッグ時のコルーチン
    IEnumerator SceneChange_Debug()
	{
        anim.SetTrigger("isClicked");
        yield return new WaitForSeconds(sceneChangeSec);
        SceneManager.LoadScene("DebugStage");
	}
}
