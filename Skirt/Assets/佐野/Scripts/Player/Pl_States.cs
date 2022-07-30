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

	public bool isLanding;      // 地上にいるか
	public bool isJamping;		// ジャンプ中か
	public bool isAttacking;	// 捕食中か
	public bool isDamaging;		// ダメージ中か

	[Header("入力判定")]
	[SerializeField] float inpY_up_jdge = 0.6f;     // 上入力時の判定
	[SerializeField] float inpY_dn_jdge = 0.6f;     // 下入力時の判定

	[Header("空気抵抗")]
	[SerializeField] float drag_flt = 0.5f;			// ふわふわ時の空気抵抗
	[SerializeField] float drag_swp = 0;			// 急降下時
	float drag_nml;                                 // 通常時の空気抵抗

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
			TransStates();
			StateProc();
			StateSizeChange();
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
				pl_act.LandMove();      // 地上移動
				Landing();				// 地上にいるときの処理
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
				break;
		}
	}

	void TransStates()
    {
		// 地上
        if (isLanding) {
			stateNum = States.landing;
        }

		// 攻撃中
        else if (isAttacking) {
			stateNum = States.attacking;
        }

		else if(isJamping) {
			stateNum = States.jumping;
		}

		// ダメージ
		else if (isDamaging) {
			stateNum = States.damage;
        }

		// 判定値(上)よりも大きくなったら、ふわふわ
		else if (gm.inpHor > inpY_up_jdge) {
			stateNum = States.floating;
		}
		// 判定値(上)より小さく、判定値(下)より大きい
		else if (gm.inpHor > -inpY_dn_jdge) {
			stateNum = States.normal;
		}

		// 判定値(下)よりも小さくなったら、急降下
		else if (gm.inpHor < -inpY_dn_jdge) {
			stateNum = States.swooping;
		}
	}

	// 入力値ごとに大きさ変える
	void StateSizeChange()
	{
		// 攻撃時
		if (stateNum == States.attacking) {

		}

		// ダメージ時
		else if (stateNum == States.damage) {
			transform.localScale = new Vector2(3, 3);
		}

		// 地上にいるとき
		else if (stateNum == States.landing) {
			transform.localScale = new Vector2(3, 3);
		}

		else {
			if (gm.inpHor < 0) {        // 上
				transform.localScale = new Vector2(3, 3 + Mathf.Abs(gm.inpHor) * 3);
			}
			else if (gm.inpHor > 0) {   // 下
				transform.localScale = new Vector2(3 + Mathf.Abs(gm.inpHor) * 3, 3);
			}
		}
	}

	//-------------------------------------------------------------------

	// 通常状態
	public void Normal()
    {
		// 元の大きさに戻す
		transform.localScale = new Vector2(3, 3);

		// 色を不透明に
		sr.color = new Color(1, 1, 1, 1);

		rb.drag = drag_nml;                 // 空気抵抗を元に戻す
	}

	// 急降下状態
	public void Swooping()
	{
		rb.drag = drag_swp;
	}

	// ふわふわ状態
	public void Floating()
	{
		rb.drag = drag_flt;             // 空気抵抗追加
	}

	//-------------------------------------------------------------------
	// 地上にいる状態
	public void Landing()
	{
		// 回転しない
		transform.rotation = Quaternion.identity;
	}

	//-------------------------------------------------------------------
	void OnCollisionStay2D(Collision2D col)
	{
		// 敵
		if (col.gameObject.tag == "Enemy") {
			// ダメージ状態
			isDamaging = true;
		}

		// 床
		if (col.gameObject.name == "Floor") {
			isLanding = true;
		}
	}

	void OnCollisionExit2D(Collision2D col)
	{
		// 床
		if (col.gameObject.name == "Floor") {
			isLanding = false;
		}
	}
}
