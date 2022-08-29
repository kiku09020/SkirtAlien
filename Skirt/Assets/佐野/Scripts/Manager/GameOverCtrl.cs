using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCtrl : MonoBehaviour
{
	/* 値 */
	[SerializeField] float gmovTime;            // 体力0になってからテキスト表示されるまでの時間

	/* フラグ */
	bool isInsted;

	/* オブジェクト */
	GameObject pl_obj;
	GameObject ui_obj;

	/* コンポーネント取得用 */
	GameManager gm;
	CanvasGenelator cnvs;

	Pl_HP hp;
	Collider2D col;

	//-------------------------------------------------------------------

	void Start()
	{
		/* コンポーネント取得 */
		ui_obj	= transform.GetChild(0).gameObject;
		pl_obj	= GameObject.Find("Player");

		gm		= GetComponent<GameManager>();
		cnvs	= ui_obj.GetComponent<CanvasGenelator>();
		hp		= pl_obj.GetComponent<Pl_HP>();
		col=pl_obj.GetComponent<Collider2D>();

		/* 初期化 */

	}

	//-------------------------------------------------------------------

	void FixedUpdate()
	{
		GameOver();
	}

	//-------------------------------------------------------------------

	void GameOver()
	{
		// HPが0以下になったら終了
		if (hp.nowHP <= 0) {
			gm.isGameOver = true;
		}

		if (gm.isGameOver) {
			col.enabled = false;        // プレイヤーのcol無効化

			if(!isInsted) {
				StartCoroutine("GmOv");
			}
		}
	}

	// ゲームオーバー後
	IEnumerator GmOv()
	{
		Time.timeScale = 0.5f;          // スローにする
		cnvs.GmOv_Del();

		yield return new WaitForSeconds(gmovTime);

		if(!isInsted) {
			Time.timeScale = 1;         // 時間戻す
			cnvs.GmOv_Inst();			// キャンバス生成
			isInsted = true;            // フラグ立てる
		}
	}
}
