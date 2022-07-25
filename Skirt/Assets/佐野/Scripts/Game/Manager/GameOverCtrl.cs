using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverCtrl : MonoBehaviour
{
	/* 値 */
	[SerializeField] float gmovTime;			// 体力0になってからテキスト表示されるまでの時間


	/* フラグ */


	/* オブジェクト */
	[SerializeField] GameObject txt_gmov;       // ゲームオーバーのテキスト
	GameObject cnvs;

	GameObject pl_obj;

	/* コンポーネント取得用 */
	GameManager gm;
	Pl_HP hp;
	Player pl;


	//-------------------------------------------------------------------

	void Start()
	{
		/* コンポーネント取得 */
		pl_obj = GameObject.Find("Player");
		cnvs = GameObject.Find("Canvas");

		gm = GetComponent<GameManager>();
		pl = pl_obj.GetComponent<Player>();
		hp = pl_obj.GetComponent<Pl_HP>();

		/* 初期化 */

	}

	//-------------------------------------------------------------------

	void Update()
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
			pl.GetComponent<Collider2D>().enabled = false;        // プレイヤーのcol無効化

			StartCoroutine("GmOv");
		}
	}

	IEnumerator GmOv()
	{
		yield return new WaitForSeconds(gmovTime);
		Instantiate(txt_gmov, cnvs.transform);
		Debug.Log("GameOver");
	}
}
