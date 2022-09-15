﻿using UnityEngine;

public partial class Pl_Action 
{
    // 移動
    void Move()
    {
        SpdSetter();    // 速度調節

        // 移動
        rb.AddForce(Vector2.right * gm.inpVer * nowSpd);
        
        SpdLimit();     // 速度制限
        Breaking();     // ブレーキ(慣性無視)
    }

    // 速度調整
    void SpdSetter()
    {
        if (hung.hungFlg) {     // 空腹時
            nowSpd = nmlSpd / 2;
        }

        else if (st.stateNum == Pl_States.States.digest) {      // 消化時
            nowSpd = nmlSpd / 6;
        }

        else {      // 通常時
            nowSpd = nmlSpd; // 速度戻す
        }
    }

    // 速度制限
    void SpdLimit()
    {
        if (vel.x > spdMax) {       // 右
            rb.velocity = new Vector2(spdMax, vel.y);
        }

        else if (vel.x < -spdMax) { // 左
            rb.velocity = new Vector2(-spdMax, vel.y);
        }

        if (vel.y < -fallSpdMax) {  // 下
            rb.velocity = new Vector2(vel.x, -fallSpdMax);
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
        if (st.stateNum == Pl_States.States.landing) {
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
            pos.x = -scrEdge;
            transform.position = new Vector2(pos.x, pos.y);
        }

        else if (pos.x < -scrEdge) {  // 左端→右端
            pos.x = scrEdge;
            transform.position = new Vector2(pos.x, pos.y);
        }
    }
}