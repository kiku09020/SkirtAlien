using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraEneController : MonoBehaviour
{


    private SpriteRenderer spRenderer;
    private Rigidbody2D rb2d;
    private GameObject player;

    public float speed = 1;

    // 当たり判定
    private HitChecker gChecker; //下の当たり判定

   
    private bool isIdle = false; // 待機中
    
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Player");

        spRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();

        // 下のチェック
        gChecker = transform.Find("groundChecker").gameObject.GetComponent<HitChecker>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = 1;
        //Debug.Log( this.transform.eulerAngles.y);

        if ( this.transform.eulerAngles.y == 180) {
            x = -1;
        }
        else{
            x = 1;
        }

       CheckValue();

        // 待機中
        if( isIdle ){
            rb2d.velocity = new Vector2(0, 0);

            // 動いてるとき
        }else{
            rb2d.AddForce( Vector2.right * x * speed );
        }
    }
    
    private void CheckValue(){
        // 地面にヒットしていないとき　かつ　待機状態ではないとき
        if( !gChecker.isGroundHit & !isIdle ){

            gChecker.isGroundHit = true;
            StartCoroutine("ChangeRotate");
        }
    }

    IEnumerator ChangeRotate(){
        isIdle = true;

        yield return new WaitForSeconds(2.0f);

        if (this.transform.eulerAngles.y == 180){
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }else{
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        isIdle=false;
    }
}

