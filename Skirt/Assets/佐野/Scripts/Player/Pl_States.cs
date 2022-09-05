using UnityEngine;

/* ★プレイヤーの状態に関するスクリプトです */
[HideInInspector]
public class Pl_States : MonoBehaviour
{
	[Header("状態")]
	public  States stateNum;		// キャラの状態(0:通常 1:ふわふわ 2:急降下 )

	// キャラ状態の列挙
	public enum States {
		normal,			// 通常
		floating,		// ふわふわ
		swooping,		// 急降下
		landing,		// 地上
		jumping,		// ジャンプ中
		eating,			// アタック
		digest,			// 消化
		damage,			// 被ダメージ
		goaled,			// ゴール後
	}

	[Header("フラグ")]
	public bool landFlg;                    // 地上にいるか
	public bool dmgFlg;						// 無敵時間中かどうか

	[Header("入力判定")]
	[SerializeField] float inpY_up_jdge = 0.6f;     // 上入力時の判定
	[SerializeField] float inpY_dn_jdge = 0.6f;     // 下入力時の判定

	[Header("空気抵抗")]
	[SerializeField] float drag_flt = 1.5f;			// ふわふわ時
	[SerializeField] float drag_nml = 1;            // 通常時
	[SerializeField] float drag_swp = 0.5f;         // 急降下時
	[SerializeField] float drag_dig = 0;            // 捕食時

	[Header("サイズ")]
	[SerializeField] float size_big = 1.5f;         // 拡大時のサイズ

	/* オブジェクト */
	GameObject gm_obj;

	/* コンポーネント取得用 */
	GameManager		gm;

	Rigidbody2D		rb;
	SpriteRenderer	sr;

	Pl_Action		pl_act;
	Pl_Hunger		hung;
	//-------------------------------------------------------------------

	void Start()
	{
		/* オブジェクト検索 */
		gm_obj	= GameObject.Find("GameManager");

		/* コンポーネント取得 */
		gm		= gm_obj.GetComponent<GameManager>();

		rb		= GetComponent<Rigidbody2D>();
		sr		= GetComponent<SpriteRenderer>();

		pl_act	= GetComponent<Pl_Action>();
		hung	= GetComponent<Pl_Hunger>();

		/* 初期化 */
		stateNum = States.normal;          // 状態
	}

	//-------------------------------------------------------------------

	void FixedUpdate()
	{
		if (stateNum != States.goaled && !gm.isGameOver&&!gm.isStarting) {
			StateProc();        // メイン処理

				// 満腹度を少しずつ減らす
				hung.HungDec_State();
			

			// 空腹
			if (hung.hungFlg) {
				hung.HungState();
            }
		}

		// state表示
		Debug.Log("<b><color=yellow>" + stateNum+"</color></b>");
	}

	//-------------------------------------------------------------------
	// 各状態時に行う処理
	void StateProc()
	{
		switch (stateNum) {
			case States.normal:		// 通常
				Normal();
				pl_act.ResetValues();
				break;

			case States.floating:	// ふわふわ
				Floating();
				break;

			case States.swooping:	// 急降下
				Swooping();
				break;

			case States.landing:	// 地上
				Landing();
				break;

			case States.jumping:	// ジャンプ
				pl_act.Jump();
				break;

			case States.eating:		// 捕食中
				pl_act.Eating();
				break;

			case States.digest:     // 消化
				Digest();
				break;

			case States.damage:		// 被ダメージ
				pl_act.Damage_Proc();
				break;
		}
	}

	//-------------------------------------------------------------------

	// ★通常状態
	public void Normal()
	{
		// 元の大きさに戻す
		transform.localScale = Vector2.one;

		// 色を不透明に
		sr.color = Color.white;

		// 空気抵抗を元に戻す
		rb.drag = drag_nml;

		// 判定値(上)よりも大きくなったら、ふわふわ
		if (gm.inpHor > inpY_up_jdge) {
			stateNum = States.floating;
		}
		// 判定値(下)よりも小さくなったら、急降下
		else if (gm.inpHor < inpY_dn_jdge) {
			stateNum = States.swooping;
		}
	}

	// ★急降下状態
	public void Swooping()
	{
		// 縦に伸ばす
		transform.localScale = new Vector2(1, 1 + Mathf.Abs(gm.inpHor) * size_big);

        if (!hung.hungFlg) {
			// 空気抵抗を減らす
			rb.drag = drag_swp;
		}

		// ノーマル状態に遷移
		if(gm.inpHor > inpY_dn_jdge && gm.inpHor < inpY_up_jdge) {
			stateNum = States.normal;
		}
	}

	// ★ふわふわ状態
	public void Floating()
	{
		// 横に伸ばす
		transform.localScale = new Vector2(1 + Mathf.Abs(gm.inpHor) * size_big, 1);

		if (!hung.hungFlg) {
			// 空気抵抗を増やす
			rb.drag = drag_flt;
		}

		// ノーマル状態に遷移
		if (gm.inpHor > inpY_dn_jdge && gm.inpHor < inpY_up_jdge) {
			stateNum = States.normal;
		}
	}

	// ★地上
	void Landing()
	{
		transform.localScale = Vector2.one;						// 大きさ戻す

		// 右移動時
		if(gm.inpVer > 0) {
			sr.flipX = true;					// スプライト反転
		}

		// 左移動時、停止時
		else {
			sr.flipX = false;					// 反転を元に戻す
		}
	}

	// ★消化時の処理
	void Digest()
    {
		rb.drag = drag_dig;				// 空気抵抗を減らす
	}

	//-------------------------------------------------------------------

	// ★ボタン押したときの処理
	public void ActBtnProc()
    {
		// 通常時のみ、捕食状態にする
		if (stateNum == States.normal) {
			stateNum = States.eating;
			hung.HungDec_Atk();
		}

		// 消化時
		else if (stateNum == States.digest) {
			pl_act.Digest_Btn();
		}

		// 地上にいたらジャンプする
		else if (landFlg && stateNum != States.eating) {
			stateNum = States.jumping;
		}
	}

	//-------------------------------------------------------------------
	
	void OnCollisionStay2D(Collision2D col)
	{
		// 敵に触れたら、ダメージ状態にする
		if (col.gameObject.tag == "Enemy") {
			stateNum = States.damage;
		}
	}

	/*
    void OnCollisionExit2D(Collision2D col)
    {
		if (col.gameObject.tag == "Floor" &&
			stateNum != States.jumping && stateNum != States.damage) {
			stateNum = States.normal;
		}
	}
	*/
}
