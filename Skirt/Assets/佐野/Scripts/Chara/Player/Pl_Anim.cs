using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pl_Anim : MonoBehaviour
{
	/* 値 */


	/* フラグ */


	/* オブジェクト */


	/* コンポーネント取得用 */
	Animator anim;

	Player pl;
	Pl_States pl_st;

	//-------------------------------------------------------------------

	void Start()
	{
		/* コンポーネント取得 */
		anim = GetComponent<Animator>();

		pl = GetComponent<Player>();
		pl_st = GetComponent<Pl_States>();

		/* 初期化 */

	}

	//-------------------------------------------------------------------

	void FixedUpdate()
	{
		// ふわふわ
		if (pl_st.isFloating) {
			anim.SetBool("isFloat", true);
		}

		// 急降下
		else if (pl_st.isSwooping) {
			anim.SetBool("isSwoop", true);
		}

		// 通常
		else {
			anim.SetBool("isFloat", false);
			anim.SetBool("isSwoop", false);

			// 捕食
			if (pl_st.isAttacking) {
				anim.SetBool("isAttack", true);
			}
			else {
				anim.SetBool("isAttack", false);
			}

			// 地上
			if (pl_st.isLanding) {
				anim.SetTrigger("isLand_Trigger");      // 着地した瞬間
				anim.SetBool("isLanding", true);        // 地上モーション

				// 地上移動中
				if (pl.inp_ver != 0) {
					anim.SetBool("isWalk", true);
				}
				else {
					anim.SetBool("isWalk", false);
				}
			}

			else {
				anim.SetBool("isLanding", false);
			}
		}
	}

	//-------------------------------------------------------------------

}
