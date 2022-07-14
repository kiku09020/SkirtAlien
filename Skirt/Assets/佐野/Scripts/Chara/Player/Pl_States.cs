using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★プレイヤーの状態に関するスクリプトです */
public class Pl_States : MonoBehaviour
{
	/* 値 */
	[Header("状態")]
	public int state;               // キャラの状態(0:通常 1:ふわふわ 2:急降下 )
	[SerializeField] string state_name;     // キャラの状態名(表示のみ)

	// キャラ状態の列挙
	public enum states
	{
		nml,        // 通常(normal)
		flt,        // ふわふわ(floating)
		swp,        // 急降下(swooping)
		lnd,		// 地上
		atk,        // アタック
		damage,		// 被ダメージ
		goaled,     // ゴール後
	}

	[Header("入力判定")]
	[SerializeField] float inpY_up_jdge = 0.6f;     // 上入力時の判定
	[SerializeField] float inpY_dn_jdge = 0.6f;     // 下入力時の判定

	[Header("空気抵抗")]
	[SerializeField] float drag_flt = 0.5f;			// ふわふわ時の空気抵抗
	[SerializeField] float drag_swp = 0;			// 急降下時
	float drag_nml;                                 // 通常時の空気抵抗

	[Header("フラグ")]
	public bool isFloating;		// ふわふわ中か
	public bool isSwooping;		// 急降下中か
	public bool isLanding;		// 着地中
	public bool isAttacking;    // 捕食中
	public bool isDamaged;		// ダメージ状態
	public bool isJumping;		// ジャンプ中

	/* オブジェクト */
	GameObject goal_obj;
	GameObject gm_obj;

	/* コンポーネント取得用 */
	SpriteRenderer sr;

	Rigidbody2D rb;
	Pl_HP pl_hp;
	Animator anim;


	GameManager gm;
	Goal_Ctrl goal;

	//-------------------------------------------------------------------

	void Start()
	{
		goal_obj = GameObject.Find("Goal");
		gm_obj = GameObject.Find("GameManager");

		/* コンポーネント取得 */
		sr = GetComponent<SpriteRenderer>();
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		pl_hp = GetComponent<Pl_HP>();

		gm=gm_obj.GetComponent<GameManager>();
		goal = goal_obj.GetComponent<Goal_Ctrl>();

		/* 初期化 */
		state = (int)states.nml;          // 状態

		isAttacking = false;
		isLanding = false;
		isFloating = false;
		isSwooping = false;
	}

	//-------------------------------------------------------------------

	void FixedUpdate()
	{
		State();        // 状態
	}

	//-------------------------------------------------------------------

	void State()
	{
		// ゴール
		if(goal.isGoaled) {
			state = (int)states.goaled;
		}

		// 攻撃
		else if (isAttacking) {
			state = (int)states.atk;
		}

		// ダメージ
		else if(isDamaged) {
			state = (int)states.damage;
		}

		// 地上
		else if(isLanding) {
			state = (int)states.lnd;
		}

		else {
			sr.color = new Color(1, 1, 1, 1);

			// 判定値(上)よりも大きくなったら、ふわふわ
			if(gm.inpHor > inpY_up_jdge) {
				state = (int)states.flt;
			}
			// 判定値(上)より小さく、判定値(下)より大きい
			else if(gm.inpHor > -inpY_dn_jdge) {
				state = (int)states.nml;
			}

			// 判定値(下)よりも小さくなったら、急降下
			else if(gm.inpHor < -inpY_dn_jdge) {
				state = (int)states.swp;
			}
		}

		// ------------------------------------------------

		// 状態ごとの処理
		switch (state) {
			// 通常
			case (int)states.nml:
				state_name = "Normal";              // 状態名
				isFloating = false;                 // フラグ降ろす
				isSwooping = false;
				isDamaged = false;
				anim.SetBool("isFloat", false);
				anim.SetBool("isSwoop", false);
				anim.SetBool("isDamaged", false);
				anim.SetBool("isAttack", false);
				anim.SetBool("isLanding", false);


				rb.drag = drag_nml;                 // 空気抵抗を元に戻す

				// 元の大きさに戻す
				transform.localScale = new Vector2(3, 3);
				break;

			// -------------------------------------------
			// ふわふわ
			case (int)states.flt:
				state_name = "Floating";
				isFloating = true;              // ふわふわ
				anim.SetBool("isFloat", true);
				rb.drag = drag_flt;             // 空気抵抗追加

				pl_hp.nowHP -= pl_hp.decDmg;
				break;

			// -------------------------------------------
			// 急降下
			case (int)states.swp:
				state_name = "Swooping";
				isSwooping = true;              // 急降下
				anim.SetBool("isSwoop", true);
				rb.drag = drag_swp;
				pl_hp.nowHP -= pl_hp.decDmg;
				break;

			// -------------------------------------------
			// 地上
			case (int)states.lnd:
				state_name = "Langing";
				isFloating = false;
				isSwooping = false;
				anim.SetBool("isLanding", true);
				anim.SetBool("isWalk", false);

				// 歩行
				if (gm.inpVer!=0){
					anim.SetBool("isWalk", true);
					anim.SetBool("isLanding", false);
				}
				break;

			// -------------------------------------------
			// アタック中
			case (int)states.atk:
				state_name = "Attacking";
				anim.SetBool("isAttack", true);
				break;

			// -------------------------------------------
			// 被ダメージ
			case (int)states.damage:
				state_name = "Damaged";
				anim.SetBool("isDamaged", true);
				break;

			// -------------------------------------------
			// ゴール
			case (int)states.goaled:
				state_name = "Goaled";
				break;
		}
	}

	// ボタンで攻撃
	public void Btn_Attack()
	{
		isAttacking = true;
	}
}
