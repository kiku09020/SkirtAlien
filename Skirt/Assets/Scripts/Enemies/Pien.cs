using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pien : MonoBehaviour
{
    [Header("値")]
    [SerializeField] float speed;           // 移動速度
    [SerializeField] float idleTime;        // 待機時間
    bool isIdle;                            // 待機中かどうか

    Rigidbody2D rb;
    Pien_Bottom btmCheck;                   //床との当たり判定
    
    void Start()
    {
        GameObject pienBtmObj = transform.Find("groundChecker").gameObject;
        btmCheck = pienBtmObj.GetComponent<Pien_Bottom>();

        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float x = 1;

        // 反転
        if (transform.eulerAngles.y == 180) {
            x = -1;
        }
        else{
            x = 1;
        }

        // 端っこにきたとき
        if (!btmCheck.isLanding && !isIdle) {
            StartCoroutine(ChangeDirection());
        }

        // 待機中
        if ( isIdle ){
            rb.velocity = Vector2.zero;
        }

        // 動いてるとき
        else {
            rb.AddForce( Vector2.right * x * speed );
        }
    }

    IEnumerator ChangeDirection(){
        btmCheck.isLanding = true;    // 触れている状態にする
        isIdle = true;                  // 待機中

        yield return new WaitForSeconds(idleTime);      // 待つ

        // 反転
        if (transform.eulerAngles.y == 180){
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }else{
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        // 待機解除
        isIdle=false;
    }
}

