using UnityEngine;

public partial class Pl_Action 
{
    // 移動
    void Move()
    {
        SpdSetter();    // 速度調節

        // 移動
        if (Mathf.Abs(vel.x) < spdMax) {
            rb.AddForce(Vector2.right * gm.inpVer * nowSpd);
        }

        // 落下速度制限
        if (vel.y < -fallSpdMax) {  
            rb.velocity = new Vector2(vel.x, -fallSpdMax);
        }

        Breaking();     // ブレーキ(慣性無視)
    }

    // 速度調整
    void SpdSetter()
    {
        if (hung.hungFlg) {     // 空腹時
            nowSpd = nmlSpd * 0.75f;
        }

        else if (st.nowState == Pl_States.States.digest) {      // 消化時
            nowSpd = nmlSpd / 6;
        }

        else {      // 通常時
            nowSpd = nmlSpd; // 速度戻す
        }
    }

    // debug用
    public Vector2 GetSpd() { return vel; }

    // ブレーキ(スティックを急に反対方向に傾けたときに慣性を軽減するようにする)
    void Breaking()
    {
        if (gm.inpVerOld < 0 && gm.inpVer > 0) {        // 左→右
            rb.velocity = new Vector2(vel.x * breakDec, vel.y);
        }

        else if (gm.inpVerOld > 0 && gm.inpVer < 0) {   // 右→左
            rb.velocity = new Vector2(vel.x * breakDec, vel.y);
        }

        else if (gm.inpVer == 0) {                      // 停止
            rb.velocity = new Vector2(vel.x * moveDec, vel.y);
        }
    }

    // 回転
    void Rotate()
    {
        // 地上にいる場合、回転しない
        if (st.lndFlg) {
            transform.rotation = Quaternion.identity;
        }

        // 空中にいる場合、移動方向に合わせて回転
        else if (gm.inpVer != 0) {
            transform.rotation = Quaternion.Euler(0, 0, gm.inpVer * moveRot);
        }

        // スティック離したら、元の向きに少しずつ回転
        else {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, rotDec);
        }
    }

    // はみ出し時の処理
    void OutScr()
    {
        if (pos.x > scrEdge) {        // 右端→左端
            transform.position = new Vector2(-scrEdge, pos.y);
        }

        else if (pos.x < -scrEdge) {  // 左端→右端
            transform.position = new Vector2(scrEdge, pos.y);
        }
    }

    // ★ジャンプ
    public void Jump()
    {
        if (hung.hungFlg) { // 空腹時
            nowJumpForce = nmlJumpForce * 0.75f;
        }
        else {              // 通常時
            nowJumpForce = nmlJumpForce;
        }

        rb.AddForce(Vector2.up * nowJumpForce);                         // ジャンプ

        aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.jump);       // 効果音再生
        part.InstPart(ParticleManager.PartNames.jump, transform.position);      // パーティクル生成
        anim.Jump();                                                            // アニメーション
        sr.color = Color.white;                                                 // 色変更
    }
}
