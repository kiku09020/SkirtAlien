using UnityEngine;

public partial class Pl_Action 
{
    // 移動
    void Move()
    {
        // 空腹時,消化時は速度半減
        if (hung.hungFlg || st.stateNum == Pl_States.States.digest) {
            nowSpd = normalSpd / 2;
        }

        // 空腹じゃなかったら、速度戻す
        else {
            nowSpd = normalSpd;
        }

        // 移動
        rb.AddForce(Vector2.right * gm.inpVer * nowSpd);

        SpdLimit();     // 速度制限
        Breaking();     // ブレーキ(慣性無視)
    }

    // 速度制限
    void SpdLimit()
    {
        // 右
        if (vel.x > spdMax) {
            rb.velocity = new Vector2(spdMax, vel.y);
        }

        // 左
        else if (vel.x < -spdMax) {
            rb.velocity = new Vector2(-spdMax, vel.y);
        }

        // 下
        if (vel.y < -100) {
            rb.AddForce(Vector2.up * 30);
        }
    }

    // 速度変更(引数:目標の速度)
    public void SetSpd(float val = 1)
    {
        // 空腹時の速度
        if (val != 1) {
            nowSpd *= val;
        }

        // 通常時の速度
        else {
            nowSpd = normalSpd;
        }
    }

    // ブレーキ(スティックを急に反対方向に傾けたときに慣性を軽減するようにする)
    void Breaking()
    {
        // 左→右
        if (gm.inpVerOld < 0 && gm.inpVer > 0) {
            rb.velocity = new Vector2(vel.x * 0.75f, vel.y);
        }

        // 右→左
        if (gm.inpVerOld > 0 && gm.inpVer < 0) {
            rb.velocity = new Vector2(vel.x * 0.75f, vel.y);
        }

        // 停止
        if (gm.inpVer == 0) {
            rb.velocity = new Vector2(vel.x * 0.96f, vel.y);
        }
    }

    // 回転
    void Rotate()
    {
        // 地上にいる場合、回転しない
        if (st.stateNum == Pl_States.States.landing) {
            transform.rotation = Quaternion.identity;
        }

        // 空中にいる場合、移動方向に合わせて回転
        else if(gm.inpVer!=0){
            transform.rotation = Quaternion.Euler(0, 0, gm.inpVer * moveRot);
        }

        // スティック離したら、少しずつ回転
        else {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, rotDec);
        }
    }

    // はみ出し時の処理
    void OutScr()
    {
        if (pos.x > scrEdge) {        // 右端→左端
            pos.x = -scrEdge;
            transform.position = new Vector2(pos.x, pos.y);
        }

        else if (pos.x < -scrEdge) {  // 左端→右端
            pos.x = scrEdge;
            transform.position = new Vector2(pos.x, pos.y);
        }
    }
}
