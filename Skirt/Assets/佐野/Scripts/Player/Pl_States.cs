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
	Pl_HP pl_hp;

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
		pl_hp	= GetComponent<Pl_HP>();

		/* 初期化 */
		stateNum = States.normal;          // 状態
	}

	//-------------------------------------------------------------------

	void FixedUpdate()
	{
		if (stateNum != States.goaled && !gm.isGameOver) {
			StateProc();
		}
	}

	//-------------------------------------------------------------------

	void StateProc()
	{
		switch (stateNum) {
			// 通常
			case States.normal:
				Normal();
				break;

			// -------------------------------------------
			// ふわふわ
			case States.floating:
				Floating();
				break;

			// -------------------------------------------
			// 急降下
			case States.swooping:
				Swooping();
				break;

			// -------------------------------------------
			// 地上
			case States.landing:
				pl_hp.Flg();
				Landing();
				break;

			// -------------------------------------------
			// ジャンプ
			case States.jumping:
				pl_act.Jump();
				break;

			// -------------------------------------------
			// アタック中
			case States.attacking:
				pl_act.Attack();
				break;

			// -------------------------------------------
			// 被ダメージ
			case States.damage:
				pl_act.Damage();
				break;

			// -------------------------------------------
			// ゴール
			case States.goaled:
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
		if(gm.inpHor > inpY_up_jdge) {
			stateNum = States.floating;
		}
		// 判定値(下)よりも小さくなったら、急降下
		else if(gm.inpHor < -inpY_dn_jdge) {
			stateNum = States.swooping;
		}
	}

	// 急降下状態
	public void Swooping()
	{
		// 大きさを縦に伸ばす
		transform.localScale = new Vector2(1, 1 + Mathf.Abs(gm.inpHor) * size_big);

		// 空気抵抗を少なくする
		rb.drag = drag_swp;

		// ノーマル状態に遷移
		if(gm.inpHor > -inpY_dn_jdge && gm.inpHor < inpY_up_jdge) {
			stateNum = States.normal;
		}

	}

	// ふわふわ状態
	public void Floating()
	{
		rb.drag = drag_flt;             // 空気抵抗追加

		// ノーマル状態に遷移
		if(gm.inpHor > -inpY_dn_jdge && gm.inpHor < inpY_up_jdge) {
			stateNum = States.normal;
		}

		if(gm.inpHor > 0) {   // 下
			transform.localScale = new Vector2(1 + Mathf.Abs(gm.inpHor) * size_big, 1);
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

	void Goaled()
	{
		transform.localScale = Vector2.one;
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
        if (col.gameObject.tag == "Floor") {
			if(stateNum != States.jumping) {
				stateNum = States.landing;
			}

        }
	}

	void OnCollisionExit2D(Collision2D col)
	{
		// 床から離れたら、通常状態に戻す
		if(col.gameObject.tag == "Floor" && stateNum == States.landing) {
			stateNum = States.normal;
		}
	}
}
