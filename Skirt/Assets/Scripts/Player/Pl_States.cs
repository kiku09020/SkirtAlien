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

	[Header("空気抵抗")]
	[SerializeField] float drag_nml;        // 通常時
	[SerializeField] float drag_dig;        // 捕食時

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
	ParticleManager	part;

	Rigidbody2D		rb;
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
		sr		= GetComponent<SpriteRenderer>();
		act	= GetComponent<Pl_Action>();
		hung	= GetComponent<Pl_Hunger>();

		/* 初期化 */
		nowState = States.normal;          // 状態
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
		switch (nowState) {
			case States.normal:		// 通常
				Normal();		break;

			case States.eating:     // 捕食中
				act.Eating();
				break;

			case States.digest:     // 消化
				Digest();		break;
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
		transform.localScale = Vector2.one;		// 大きさを戻す
		sr.color = Color.white;					// 色を不透明に
		rb.drag = drag_nml;						// 空気抵抗を元に戻す
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
			if (nowState != States.eating) {
				transform.localScale = Vector2.one;         // 大きさ
			}
			
			sr.color = hungColor;                       // 色変更
			
			hungTimer += Time.deltaTime;

			if (hungTimer > 0.5f) {
				part.InstPart(ParticleManager.PartNames.hungry, transform.position + Vector3.up * 2.5f,transform.rotation, transform);
				hungTimer = 0;
            }
		}

		// 満腹度減らす
		else if (!lndFlg && !gm.isStarting) {
			hung.HungDec_State();
			hungTimer = 0;
		}
	}

	//-------------------------------------------------------------------
	// 押した瞬間
	public void ActBtnProc()
    {
		// 消化時
		if (nowState == States.digest) {
			act.Digest_Btn();
		}

		// 地上にいたらジャンプする
		else if (nowState == States.normal && lndFlg) {
			act.Jump();
		}
	}

	// 長押し
	public void ActBtn_Downing()
    {
		// 捕食状態にする
		if ((nowState != States.digest) && !lndFlg && canEat) {
			 nowState = States.eating;
		}
	}

	// 離した瞬間
	public void ActBtn_Up()
    {
		// 通常状態に遷移
        if (nowState == States.eating) {
			nowState = States.normal;
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
			dmgFlg = true;
        }
    }
}
