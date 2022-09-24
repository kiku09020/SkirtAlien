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
		eating,			// アタック
		digest,			// 消化
		damage,			// 被ダメージ
	}

	[Header("フラグ")]
	public bool landFlg;					// 地上にいるか
	public bool dmgFlg;						// 無敵時間中かどうか

	[Header("入力判定")]
	[SerializeField] float inpY_up_jdge;    // 上入力時の判定
	[SerializeField] float inpY_dn_jdge;    // 下入力時の判定

	[Header("空気抵抗")]
	[SerializeField] float drag_flt;		// ふわふわ時
	[SerializeField] float drag_nml;        // 通常時
	[SerializeField] float drag_swp;        // 急降下時
	[SerializeField] float drag_dig;        // 捕食時

	[Header("サイズ")]
	[SerializeField] float size_big;        // 拡大時のサイズ

	[Header("空腹")]
	[SerializeField] Color hungColor;
	float hungTimer;

	[Header("捕食関係")]
	[SerializeField] float ctLim;			// クールタイム
	float ct;								// クールタイムタイマー
	bool ctFlg;								// 
	bool canEat;							// 


	/* コンポーネント取得用 */
	GameManager		gm;
	StageManager    stg;
	ParticleManager		part;

	Rigidbody2D		rb;
	Collider2D		col;
	SpriteRenderer	sr;

	Pl_Action		act;
	Pl_Hunger		hung;
	//-------------------------------------------------------------------

	void Start()
	{
		/* オブジェクト検索 */
		GameObject gm_obj  = GameObject.Find("GameManager");
		GameObject partObj = gm_obj.transform.Find("ParticleManager").gameObject;

		/* コンポーネント取得 */
		gm		= gm_obj.GetComponent<GameManager>();
		stg     = gm_obj.GetComponent<StageManager>();
		part    = partObj.GetComponent<ParticleManager>();

		rb		= GetComponent<Rigidbody2D>();
		col = GetComponent<Collider2D>();
		sr		= GetComponent<SpriteRenderer>();
		act	= GetComponent<Pl_Action>();
		hung	= GetComponent<Pl_Hunger>();

		/* 初期化 */
		stateNum = States.normal;          // 状態
		canEat = true;

		// 位置をステージの長さに合わせる
		transform.position = new Vector2(0, stg.stg_length);
	}

	//-------------------------------------------------------------------

	void FixedUpdate()
	{
		if (!gm.isGameOver && !gm.isGoaled) {
			StateProc();        // メイン処理
			Hungry();           // 満腹度処理

            if (ctFlg) {
				ct += Time.deltaTime;

                if (ct > ctLim) {
					canEat = true;
					ctFlg = false;
					ct = 0;
                }
            }
		}
	}

	//-------------------------------------------------------------------
	// 各状態時に行う処理
	void StateProc()
	{
		switch (stateNum) {
			case States.normal:		// 通常
				Normal();		break;

			case States.floating:	// ふわふわ
				Floating();		break;

			case States.swooping:	// 急降下
				Swooping();		break;

			case States.landing:	// 地上
				Landing();		break;

			case States.eating:     // 捕食中
				hung.HungDec_Atk();
				break;

			case States.digest:     // 消化
				Digest();		break;

			case States.damage:		// 被ダメージ
				act.Damage_Proc();	break;
		}
	}

	//-------------------------------------------------------------------

	// ★通常状態
	public void Normal()
	{
		transform.localScale = Vector2.one;		// 大きさを戻す
		sr.color = Color.white;					// 色を不透明に
		rb.drag = drag_nml;						// 空気抵抗を元に戻す

		// 判定値(上)よりも大きくなったら、ふわふわ
		if (gm.inpHor > inpY_up_jdge) {
			stateNum = States.floating;
		}
		// 判定値(下)よりも小さくなったら、急降下
		else if (gm.inpHor < inpY_dn_jdge) {
			stateNum = States.swooping;
		}

		act.ResetValues();
	}

	// ★急降下状態
	public void Swooping()
	{
		// 縦に伸ばす
		transform.localScale = new Vector2(1, 1 + Mathf.Abs(gm.inpHor) * size_big);

		// 空気抵抗を減らす
        if (!hung.hungFlg) {
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

		// 空気抵抗を増やす
		if (!hung.hungFlg) {
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
		transform.localScale = Vector2.one;

		// 右移動時
		if(gm.inpVer > 0) {
			sr.flipX = true;					// 反転
		}

		// 左移動時、停止時
		else {
			sr.flipX = false;					// 反転を元に戻す
		}
	}

	// ★消化時の処理
	void Digest()
    {
		rb.drag = drag_dig;                     // 空気抵抗を減らす
		sr.color = Color.white;
	}

	// ★空腹度処理
	void Hungry()
    {
		// 空腹時の処理
		if (hung.hungFlg) {
            if (stateNum != States.eating) {
				transform.localScale = Vector2.one;         // 大きさ
            }

            if (stateNum != States.damage) {
				sr.color = hungColor;                       // 色変更
            }
			hungTimer += Time.deltaTime;

			if (hungTimer > 0.5f) {
				part.InstPart(ParticleManager.PartNames.hungry, transform.position + Vector3.up * 2.5f,transform.rotation, transform);
				hungTimer = 0;
            }
		}

		// 満腹度減らす
		else if (!landFlg && !gm.isStarting) {
			hung.HungDec_State();
			hungTimer = 0;
		}
	}

	//-------------------------------------------------------------------

	// 押した瞬間
	public void ActBtnProc()
    {
		// 消化時
		if (stateNum == States.digest) {
			act.Digest_Btn();
		}

		// 地上にいたらジャンプする
		else if (landFlg && stateNum != States.eating) {
			act.Jump();
		}
	}

	// 押してる最中
	public void ActBtn_Downing()
    {
		// 通常時のみ、捕食状態にする
		if ((stateNum != States.landing &&
			 stateNum != States.digest) && canEat) {
			stateNum = States.eating;
			act.Eating();
		}
	}

	// 離した瞬間
	public void ActBtn_Up()
    {
		// 通常状態に遷移
        if (stateNum == States.eating) {
			stateNum = States.normal;
			act.Eatend();

			ctFlg = true;
			canEat = false;
		}
    }

    //-------------------------------------------------------------------

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
			stateNum = States.damage;
        }
    }
}
