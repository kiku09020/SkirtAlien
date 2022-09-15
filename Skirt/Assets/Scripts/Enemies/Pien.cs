using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pien : MonoBehaviour
{
    [Header("値")]
    [SerializeField] float speed = 1;
    [SerializeField] float idleTime = 2;
    bool isIdle; // 待機中

    Rigidbody2D rb;
    Pien_Bottom underChecker;    //下の当たり判定
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // 下のチェック
        underChecker = transform.Find("groundChecker").gameObject.GetComponent<Pien_Bottom>();
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

        // 地面にヒットしていないとき　かつ　待機状態ではないとき
        if (!underChecker.isGroundHit && !isIdle) {
            underChecker.isGroundHit = true;
            StartCoroutine("ChangeRotate");
        }

        // 待機中
        if ( isIdle ){
            rb.velocity = new Vector2(0, 0);
        }

        // 動いてるとき
        else {
            rb.AddForce( Vector2.right * x * speed );
        }
    }

    IEnumerator ChangeRotate(){
        isIdle = true;      // 待機中

        yield return new WaitForSeconds(2.0f);      // 待つ

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

