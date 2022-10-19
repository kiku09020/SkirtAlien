using UnityEngine;

/* ★プレイヤーの状態に関するスクリプトです */
[HideInInspector]
public class Pl_States : MonoBehaviour
{
	[Header("状態")]
	public  States nowState;		// 現在の状態

	// 状態
	public enum States {
		normal,			// 通常
		eating,			// 捕食
		digest,			// 消化
	}

	[Header("フラグ")]
	public bool lndFlg;		// 地上にいるか
	public bool dmgFlg;		// 無敵時間中かどうか

	/* コンポーネント取得用 */
	GameManager		gm;
	StageManager    stg;

	Rigidbody2D rb;
	SpriteRenderer	sr;

	Pl_Action		act;
	//-------------------------------------------------------------------

	void Start()
	{
		/* オブジェクト検索 */
		GameObject gm_obj  = GameObject.Find("GameManager");

		/* コンポーネント取得 */
		gm		= gm_obj.GetComponent<GameManager>();
		stg     = gm_obj.GetComponent<StageManager>();

		sr		= GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
		act	= GetComponent<Pl_Action>();

		/* 初期化 */
		nowState = States.normal;          // 状態

		// 位置をステージの長さに合わせる
		transform.position = new Vector2(0, stg.stg_length);
	}

	//-------------------------------------------------------------------

	void FixedUpdate()
	{
		if (!gm.isGameOver && !gm.isGoaled) {
			StateProc();        // メイン処理
		}
	}

	//-------------------------------------------------------------------
	// 各状態時に行う処理
	void StateProc()
	{
		switch (nowState) {
			case States.normal:		// 通常
				Normal();		break;

			case States.eating:     // 捕食中
				act.Eating();	break;

			case States.digest:     // 消化
				act.Digest();		break;
		}

        if (dmgFlg) {
			act.Damage();
        }

        if (lndFlg) {
			Landing();
        }
	}

	//-------------------------------------------------------------------

	// ★通常状態
	public void Normal()
	{
		rb.drag = 0.5f;
	}

	// ★地上
	void Landing()
	{
		// 右移動時
		if(gm.inpVer > 0) {
			sr.flipX = true;					// 反転
		}

		// 左移動時、停止時
		else {
			sr.flipX = false;					// 反転を元に戻す
		}
	}

	//-------------------------------------------------------------------
	// 押した瞬間
	public void ActBtnDown()
    {
		// 捕食状態にする
		if ((nowState != States.digest) && !lndFlg && act.canEat) {
			nowState = States.eating;
		}

		// 消化時
		else if (nowState == States.digest) {
			act.Digest_Btn();
		}

		// 地上にいたらジャンプする
		else if (nowState == States.normal && lndFlg) {
			act.Jump();
		}
	}

	// 離した瞬間
	public void ActBtn_Up()
    {
		// 通常状態に遷移
        if (nowState == States.eating) {
			nowState = States.normal;
			act.EatEnd();
		}
    }

    //-------------------------------------------------------------------
	// 敵に触れたらダメージ
    void OnCollisionStay2D(Collision2D col)
    {
		if (col.gameObject.tag == "Enemy") {
			dmgFlg = true;
		}
    }
}
