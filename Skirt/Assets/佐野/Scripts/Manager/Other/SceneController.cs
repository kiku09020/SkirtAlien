using UnityEngine;
using UnityEngine.SceneManagement;

/* ★シーンに関するスクリプトです */
public class SceneController : MonoBehaviour {
    [Header("値")]
    public int nowSceneIndex;		// 現在のシーン番号
    public int sceneCnt;		    // 読み込まれたシーン数

    [Header("文字列")]
    public string nowSceneName;		// 現在のシーン名

    //-------------------------------------------------------------------
    void Awake()
    {
        sceneCnt = SceneManager.sceneCount;         // 合計シーン数

        // 現在のシーン
        Scene nowScene = SceneManager.GetActiveScene();     // 現在のシーン
        nowSceneIndex  = nowScene.buildIndex;               // 現在のシーン番号取得
        nowSceneName   = nowScene.name;			            // 現在のシーン名取得
    }

    //-------------------------------------------------------------------
    // 次のシーンへ
    public void SceneNext()
    { SceneManager.LoadScene(nowSceneIndex + 1); }

    // 前のシーンへ
    public void ScenenBack()
    { SceneManager.LoadScene(nowSceneIndex - 1); }

    // 現在のシーン再読み込み
    public void SceneReload()
    { SceneManager.LoadScene(nowSceneIndex); }

    // 指定のシーン読み込み
    public void SceneLoading(string scnName)
    { SceneManager.LoadScene(scnName); }
}
