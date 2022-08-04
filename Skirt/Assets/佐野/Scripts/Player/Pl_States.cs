using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★プレイヤーの状態に関するスクリプトです */
[HideInInspector]
public class Pl_States : MonoBehaviour
{
	[Header("状態")]
	public  States stateNum;               // キャラの状態(0:通常 1:ふわふわ 2:急降下 )

	// キャラ状態の列挙
	public enum States {
		normal,			// 通常
		floating,		// ふわふわ
		swooping,		// 急降下
		landing,		// 地上
		jumping,		// ジャンプ中
		attacking,		// アタック
		damage,			// 被ダメージ
		goaled,			// ゴール後
	}

	[Header("入力判定")]
	[SerializeField] float inpY_up_jdge = 0.6f;     // 上入力時の判定
	[SerializeField] float inpY_dn_jdge = 0.6f;     // 下入力時の判定

	[Header("空気抵抗")]
	[SerializeField] float drag_flt = 0.5f;			// ふわふわ時の空気抵抗
	[SerializeField] float drag_swp = 0;			// 急降下時
	float drag_nml;                                 // 通常時の空気抵抗

	[Header("サイズ")]
	[SerializeField] float size_big = 1.5f;         // 拡大時のサイズ

	/* オブジェクト */
	GameObject gm_obj;

	/* コンポーネント取得用 */
	Rigidbody2D rb;
	SpriteRenderer sr;

	GameManager gm;

	Pl_Action pl_act;
	Pl_HP hp;
	Pl_Hunger hung;

	//-------------------------------------------------------------------

	void Start()
	{
		/* オブジェクト検索 */
		gm_obj	= GameObject.Find("GameManager");

		/* コンポーネント取得 */
		rb		= GetComponent<Rigidbody2D>();
		sr		= GetComponent<SpriteRenderer>();
		
		gm		= gm_obj.GetComponent<GameManager>();

		pl_act	= GetComponent<Pl_Action>();
		hp	= GetComponent<Pl_HP>();
		hung = GetComponent<Pl_Hunger>();

		/* 初期化 */
		stateNum = States.normal;          // 状態
	}

	//-------------------------------------------------------------------

	void FixedUpdate()
	{
		if (stateNum != States.goaled && !gm.isGameOver) {
			StateProc();
		}

		Debug.Log("<b><color=yellow>" + stateNum+"</color></b>");
	}

	//-------------------------------------------------------------------

	void StateProc()
	{
		switch (stateNum) {
			case States.normal:		// 通常
				Normal();
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

			case States.attacking:	// アタック中
				pl_act.Attack();
				break;

			case States.damage:		// 被ダメージ
				pl_act.Damage();
				break;

			case States.goaled:		// ゴール
				Goaled();
				break;
		}
	}

	//-------------------------------------------------------------------

	// 通常状態
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

	// 急降下状態
	public void Swooping()
	{
		// 縦に伸ばす
		transform.localScale = new Vector2(1, 1 + Mathf.Abs(gm.inpHor) * size_big);

		// 空気抵抗を少なくする
		rb.drag = drag_swp;

		// 満腹度処理
		hung.HungDec_State();

		// ノーマル状態に遷移
		if(gm.inpHor > inpY_dn_jdge && gm.inpHor < inpY_up_jdge) {
			stateNum = States.normal;
		}
	}

	// ふわふわ状態
	public void Floating()
	{
		// 横に伸ばす
		transform.localScale = new Vector2(1 + Mathf.Abs(gm.inpHor) * size_big, 1);

		// 空気抵抗追加
		rb.drag = drag_flt;

		// 満腹度処理
		hung.HungDec_State();

		// ノーマル状態に遷移
		if (gm.inpHor > inpY_dn_jdge && gm.inpHor < inpY_up_jdge) {
			stateNum = States.normal;
		}
	}

	// 地上移動
	void Landing()
	{
		transform.localScale = Vector2.one;

		// 左移動時
		if(gm.inpVer < 0) {
			sr.flipX = false;
		}

		// 右移動時
		else if(gm.inpVer > 0) {
			sr.flipX = true;
		}

		else {
			sr.flipX = false;
		}
	}

	// ゴール時の処理
	void Goaled()
	{
		transform.localScale = Vector2.one;
	}

	//-------------------------------------------------------------------

	// ボタン押したときの処理
	public void Act()
    {
		// 地上にいたらジャンプする
		if (stateNum == States.landing) {
			 stateNum = States.jumping;
		}

		// 通常時のみ捕食
		else if (stateNum == States.normal) {
			stateNum = States.attacking;
			hung.HungDec_Atk();
		}
	}

	//-------------------------------------------------------------------
	
	void OnCollisionStay2D(Collision2D col)
	{
		// 敵
		if (col.gameObject.tag == "Enemy") {
			// ダメージ状態
			stateNum = States.damage;
		}

		// 床
		if (col.gameObject.tag == "Floor" && (
			stateNum == States.normal ||
			stateNum == States.floating ||
			stateNum == States.swooping)) {
			stateNum = States.landing;
		}
	}

    void OnCollisionExit2D(Collision2D col)
    {
		if (col.gameObject.tag == "Floor" &&
			stateNum != States.jumping && stateNum != States.damage) {
			stateNum = States.normal;
		}
	}
}
