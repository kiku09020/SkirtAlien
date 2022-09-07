using System.Collections;
using UnityEngine;
using DG.Tweening;

/* ★キャンバス生成に関するスクリプトです */
public class CanvasGenelator : MonoBehaviour
{
    bool once;

    /* オブジェクト */
    // プレハブ
    [SerializeField] GameObject cvsPref_ctrl;
    [SerializeField] GameObject cvsPref_game;
    [SerializeField] GameObject cvsPref_pause;
    [SerializeField] GameObject cvsPref_gameOver;
    [SerializeField] GameObject cvsPref_goal;
    [SerializeField] GameObject cvsPref_caution;

    // インスタンス
    GameObject cvsInst_ctrl;
    GameObject cvsInst_game;        // gameUI
    GameObject cvsInst_pause;       // pause
    GameObject cvsInst_gameOver;    // gameOver
    GameObject cvsInst_goal;        // goal
    GameObject cvsInst_caution;

    // transform
    Transform[] ctrlChild;
    Transform[] gameChild;

    /* コンポーネント取得用 */
    GameManager gm;

    //-------------------------------------------------------------------
    void Awake()
    {
        GameObject gm_obj = transform.parent.gameObject;

        /* コンポーネント取得 */
        gm = gm_obj.GetComponent<GameManager>();

        /* 初期化 */
        cvsInst_ctrl = Instantiate(cvsPref_ctrl);
        cvsInst_game = Instantiate(cvsPref_game);
        cvsInst_pause = Instantiate(cvsPref_pause);
        cvsInst_caution = Instantiate(cvsPref_caution);
        cvsInst_caution.transform.SetParent(cvsInst_pause.transform, false);

        cvsInst_pause.SetActive(false);
        cvsInst_caution.SetActive(false);
    }

    //-------------------------------------------------------------------
    void FixedUpdate()
    {
        Starting();
    }

    //-------------------------------------------------------------------
    public void Starting()
    {
        if (gm.isStarting) {
            cvsInst_ctrl.SetActive(false);
            cvsInst_game.SetActive(false);
        }
        else if(!once){
            cvsInst_ctrl.SetActive(true);
            cvsInst_game.SetActive(true);

            ctrlChild = new Transform[cvsInst_ctrl.transform.childCount];
            gameChild = new Transform[cvsInst_game.transform.childCount];

            // コントローラーUI
            for(int i = 0; i < ctrlChild.Length; i++) {
                ctrlChild[i] = cvsInst_ctrl.transform.GetChild(i);
                ctrlChild[i].transform.localScale = Vector2.zero;
                ctrlChild[i].DOScale(1, 0.3f).SetEase(Ease.OutSine);
            }

            // ゲーム画面UI
            for(int i = 0; i < gameChild.Length; i++) {
                gameChild[i] = cvsInst_game.transform.GetChild(i);
                var size = gameChild[i].transform.localScale;
                gameChild[i].transform.localScale = Vector2.zero;
                gameChild[i].DOScale(size, 0.5f).SetDelay(0.1f).SetEase(Ease.OutSine);
            }

            once = true;
        }
    }

    // ポーズ時
    public void Pause()
	{
        cvsInst_ctrl.SetActive(false);
        cvsInst_pause.SetActive(true);
        cvsInst_game.SetActive(false);
    }

    // ポーズ解除時
    public void UnPause()
	{
        cvsInst_ctrl.SetActive(true);
        cvsInst_pause.SetActive(false);
        cvsInst_game.SetActive(true);
    }

    // 警告
    public void Caution()
    {
        cvsInst_caution.SetActive(true);

        // アニメーション
        var sqec = DOTween.Sequence();
        var caut = cvsInst_caution.transform.Find("Caution").transform;
        sqec.Append(caut.DOScale(new Vector2(0f, 0f), 0f));
        sqec.Append(caut.transform.DOScale(new Vector2(1.2f, 1.2f), 0.3f));
        sqec.Append(caut.DOScale(new Vector2(1f, 1f), 0.1f));
    }

    // 警告解除時
    public void UnCaution()
    {
        cvsInst_caution.SetActive(false);
    }

    // ゲームオーバー時に削除
    public void GmOv_Del()
    {
        cvsInst_ctrl.SetActive(false);
        cvsInst_game.SetActive(false);
    }

    // ゲームオーバー時に表示
    public void GmOv_Inst()
    {
        cvsInst_gameOver = Instantiate(cvsPref_gameOver);
    }

    // ゴール時
    public void Inst_Goal()
	{
        cvsInst_goal = Instantiate(cvsPref_goal);

        for(int i = 0; i < ctrlChild.Length; i++) {
            ctrlChild[i].DOScale(Vector2.zero, 0.2f).SetEase(Ease.InQuint);
        }

        for(int i = 0; i < gameChild.Length; i++) {
            gameChild[i].DOScale(Vector2.zero, 0.2f).SetEase(Ease.InQuint);
        }

        StartCoroutine("actFalse");
    }

    IEnumerator actFalse()
    {
        yield return new WaitForSeconds(1);
        cvsInst_ctrl.SetActive(false);
        cvsInst_game.SetActive(false);
    }
}
