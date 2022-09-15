using System.Collections;
using UnityEngine;

public class GameOverCtrl : MonoBehaviour
{
	/* 値 */
	[SerializeField] float gmovTime;            // 体力0になってからテキスト表示されるまでの時間

	/* フラグ */
	bool isInsted;

	/* コンポーネント取得用 */
	GameObject pl_obj;

	GameManager gm;
	CanvasGenelator cnvs;
	AudioManager aud;
	ParticleManager part;

	Pl_HP hp;
	Collider2D col;
	Rigidbody2D rb;

	//-------------------------------------------------------------------

	void Start()
	{
		/* コンポーネント取得 */
		GameObject ui_obj	= transform.Find("UIManager").gameObject;
		GameObject aud_obj	= transform.Find("AudioManager").gameObject;
		GameObject partObj = transform.Find("ParticleManager").gameObject;
		pl_obj	= GameObject.Find("Player");

		gm		= GetComponent<GameManager>();
		cnvs	= ui_obj.GetComponent<CanvasGenelator>();
		aud		= aud_obj.GetComponent<AudioManager>();
		part	= partObj.GetComponent<ParticleManager>();
		hp		= pl_obj.GetComponent<Pl_HP>();
		col		= pl_obj.GetComponent<Collider2D>();
		rb		= pl_obj.GetComponent<Rigidbody2D>();
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
			col.enabled = false;								// プレイヤーのcol無効化

			if(!isInsted) {
				StartCoroutine(GmOv());
			}
		}
	}

	// ゲームオーバー後
	IEnumerator GmOv()
	{
		Time.timeScale = 0.5f;          // スローにする
		aud.PauseAudio(true);			// 音声停止
		cnvs.GmOv_Del();                // キャンバス


		yield return new WaitForSeconds(gmovTime);

		if(!isInsted) {
			part.InstPart(ParticleManager.PartNames.dead, pl_obj.transform.position + Vector3.up * 50, false);
			rb.gravityScale = -0.5f;                            // 浮かせる
			pl_obj.transform.rotation = Quaternion.identity;    // 角度戻す
			pl_obj.transform.localScale = Vector2.one;          // サイズ戻す

			aud.PlayBGM(AudLists.BGMList.gameOver, false);
			Time.timeScale = 1;         // 時間戻す
			cnvs.GmOv_Inst();			// キャンバス生成
			isInsted = true;            // フラグ立てる
		}
	}
}
