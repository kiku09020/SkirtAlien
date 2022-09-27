using UnityEngine;

public partial class Pl_Action
{
    // ★ダメージ
    public void Damage()
    {
        // ダメージくらった瞬間
        if (dmgTimer == 0) {
            InstantDamage();
        }

        else {
            transform.localScale = Vector2.one;     // 大きさ戻す

            // 点滅
            var alpha = Mathf.Cos(2 * Mathf.PI * dmgTimer / flashCycle);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);
        }

        dmgTimer += Time.deltaTime;       // タイマー増加

        // 時間経過後
        if (dmgTimer > dmgTimeLim) {
            sr.color = Color.white;
            st.dmgFlg = false;
            dmgTimer = 0;
        }
    }

    // ダメージくらった瞬間
    public void InstantDamage()
    {
        hp.HP_Damage();                                     // HP減らす
        combo.ComboSetter(ComboManager.CmbEnum.reset);      // 消化コンボ数リセット
        rb.AddForce(Vector2.up * dmgJumpForce);             // 少し飛ばす

        Vibration.Vibrate(300);                             // スマホ振動
    }

    //------------------------------------------

}
