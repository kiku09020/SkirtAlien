using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	[Header("値")]
	[SerializeField] float move_force	= 7;		// 移動に加える力
	[SerializeField] float jump_force	= 50;		// ジャンプ力
	[SerializeField] float spd_max		= 7.5f;		// 最大速度
	[SerializeField] float hp_max		= 100;      // 最大HP

	float hp = 100;                 // 体力

	Vector2 pos, size;              // 位置、大きさ
	Vector2 vel;                    // 速度

	float scrn_Edge;                // 画面端のX座標
	int cnt;

	[Header("攻撃")]
	[SerializeField] float attack_time = 60;		// 攻撃の長さ

	[Header("ダメージ関係")]
	[SerializeField] int invTime;                   // 無敵時間
	int dmgCnt;
	[SerializeField] int damage = 20;				// ダメージ

	// ------------------------------------------------------------------------
	[Header("フラグ")]
	public bool isGameOver;		// ゲームオーバー

	// ------------------------------------------------------------------------
	[Space(15)]
	[Header("スマホ用の値")]
	[SerializeField] Joystick stick;    // スティック
	public float inp_ver, inp_hor;      // スティックの入力値

	// ------------------------------------------------------------------------

	/* オブジェクト検索 */
	GameObject gm_obj;
	GameObject cam_obj;
	GameObject goal_obj;
	GameObject hp_obj;

	/* コンポーネント取得用 */
	Rigidbody2D		rb;
	Collider2D		col;
	SpriteRenderer	sr;

	Pl_States	pl_st;			// プレイヤーの状態
	GameManager gm;             // GameManager
	Btn_Ctrl	btn_ctrl;       // ボタン
	Goal_Ctrl	goal;           // ゴール
	Pl_Camera	cam;            // カメラ
	Pl_HP		hpGauge;		// 体力

	//-------------------------------------------------------------------

	void Start()
	{
		/* オブジェクト検索 */
		gm_obj		= GameObject.Find("GameManager");
		goal_obj	= GameObject.Find("Goal");
		cam_obj		= GameObject.Find("PlayerCamera");
		hp_obj		= GameObject.Find("HPBar");

		/* コンポーネント取得 */
		rb			= GetComponent<Rigidbody2D>();
		col			= GetComponent<Collider2D>();
		sr			= GetComponent<SpriteRenderer>();

		pl_st		= GetComponent<Pl_States>();
		gm			= gm_obj.GetComponent<GameManager>();
		btn_ctrl	= gm_obj.GetComponent<Btn_Ctrl>();
		goal		= goal_obj.GetComponent<Goal_Ctrl>();
		cam			= cam_obj.GetComponent<Pl_Camera>();
		hpGauge		= hp_obj.GetComponent<Pl_HP>();

		// --------------------------------------------------------------------

		/* 初期化 */

		// Transform関連   
		transform.position = new Vector2(0, gm.stg_length);        // スタート地点を、ステージの長さに合わせる
	}

	//-------------------------------------------------------------------

	void FixedUpdate()
	{
		if(!btn_ctrl.isPause && !goal.isGoaled) {
			GameOver();
			hpGauge.HP_Set(hp_max, hp);

			if(!isGameOver) {
				Damage();
				Attack();       // 攻撃
				Landing_Proc(); // 地上にいるときの処理
				Move_Side();    // 横移動

				// スクリーン端の座標更新
				scrn_Edge = cam.scrn_EdgeX;

				// 入力値ごとに大きさ変える
				if(!pl_st.isAttacking && !pl_st.isLanding && !pl_st.isDamaged) {
					if(inp_hor < -0.1f) {
						transform.localScale = new Vector2(3, 3 + Mathf.Abs(inp_hor) * 3);
					}
					else if(inp_hor > 0.1f) {
						transform.localScale = new Vector2(3 + Mathf.Abs(inp_hor) * 3, 3);
					}
				}
			}
		}
	}

	//-------------------------------------------------------------------

	// 左右移動
	void Move_Side()
	{
		pos = transform.position;          // 位置
		size = transform.localScale;        // 大きさ
		vel = rb.velocity;                 // 速度

		// 入力値
		inp_ver = stick.Horizontal;
		inp_hor = stick.Vertical;

		// ----------------------------------------------------------

		// 移動速度が最高速度以下のとき
		if(Mathf.Abs(vel.x) < spd_max) {
			// 移動
			rb.AddForce(Vector2.right * inp_ver * move_force);

			if(!pl_st.isLanding) {
				// 移動方向に合わせて回転
				transform.rotation = Quaternion.Euler(0, 0, vel.x * 3f);
			}
		}

		// 少しずつ減速、角度を元に戻す
		if(inp_ver == 0) {
			rb.velocity = new Vector2(vel.x * 0.96f, vel.y);
			transform.rotation = Quaternion.Euler(0, 0, vel.x * 2.96f);
		}

		// 画面外はみ出し時
		if(pos.x > scrn_Edge) {        // 右端→左端
			pos.x = -scrn_Edge;
			transform.position = new Vector2(pos.x, pos.y);
		}

		else if(pos.x < -scrn_Edge) {  // 左端→右端
			pos.x = scrn_Edge;
			transform.position = new Vector2(pos.x, pos.y);
		}

		// 地上での横移動
		if(pl_st.isLanding) {
			transform.localScale = new Vector2(3, 3);

			// 左移動時
			if(inp_ver < 0) {
				sr.flipX = false;
			}

			// 右移動時
			else if(inp_ver > 0) {
				sr.flipX = true;
			}

			else {
				sr.flipX = false;
			}
		}

		else {
			sr.flipX = false;
		}
	}

	//---------------------------------------------------------------------------------------------

	// アタック
	void Attack()
	{
		// 通常状態のみ、アタックできる
		if(pl_st.isAttacking && !pl_st.isLanding && (pl_st.state == (int)Pl_States.states.nml || pl_st.state == (int)Pl_States.states.atk)) {
			cnt++;      // カウンター増加

			// 大きくなる
			transform.localScale = new Vector2(3 + cnt * 0.05f, 3 + cnt * 0.05f);

			// 時間経過後、通常状態へ戻る
			if(cnt > attack_time) {
				cnt = 0;
				pl_st.isAttacking = false;

				pl_st.state = (int)Pl_States.states.nml;
			}
		}
	}

	// ダメージ
	void Damage()
	{
		if(pl_st.isDamaged) {
			dmgCnt++;

			// ダメージくらった瞬間
			if(dmgCnt == 1) {
				hp -= damage;									// HP減らす
				transform.localScale = new Vector2(3, 3);		// 大きさ調整
				rb.AddForce(Vector2.up * 300);					// 少し飛ばす
			}

			// 点滅
			if(dmgCnt % 2 == 0) {
				sr.color = new Color(1, 1, 1, 0);
			}

			else {
				sr.color = new Color(1, 1, 1, 1);
			}

			// ダメージ処理終了
			if(dmgCnt > invTime) {
				dmgCnt = 0;
				pl_st.isDamaged = false;
				pl_st.state = (int)Pl_States.states.nml;		// 通常状態に戻す
			}
		}
	}

	void GameOver()
	{
		// HPが0以下になったら終了
		if(hp <= 0) {
			isGameOver = true;
		}

		if(isGameOver) {
			col.enabled = false;        // 当たり判定無効化

			StartCoroutine("GmOv");
		}

	}

	IEnumerator GmOv()
	{
		yield return new WaitForSeconds(3);
		Debug.Log("GameOver");
	}


	// 地上にいるときの処理
	void Landing_Proc()
	{
		if(pl_st.isLanding) {
			// 通常状態
			pl_st.state = (int)Pl_States.states.nml;
			pl_st.isJumping = false;

			// 回転しない
			transform.rotation = Quaternion.identity;

			// ボタンでジャンプ
			if(pl_st.isAttacking) {
				pl_st.isAttacking = false;
				pl_st.isJumping = true;
				rb.AddForce(Vector2.up * jump_force);
			}
		}
	}

	//---------------------------------------------------------------------------------------------


	// 敵に触れたとき
	void OnCollisionStay2D(Collision2D col)
	{
		if(col.gameObject.tag == "Enemy") {
			// ダメージ状態
			pl_st.isDamaged = true;
		}
	}
}
