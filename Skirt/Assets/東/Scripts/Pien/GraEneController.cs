using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraEneController : MonoBehaviour
{


    private SpriteRenderer spRenderer;
    private Rigidbody2D rb2d;
    private GameObject player;

    public float speed = 1;

    // �����蔻��
    private HitChecker gChecker; //���̓����蔻��

   
    private bool isIdle = false; // �ҋ@��
    
    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("Player");

        spRenderer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();

        // ���̃`�F�b�N
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

        // �ҋ@��
        if( isIdle ){
            rb2d.velocity = new Vector2(0, 0);

            // �����Ă�Ƃ�
        }else{
            rb2d.AddForce( Vector2.right * x * speed );
        }
    }
    
    private void CheckValue(){
        // �n�ʂɃq�b�g���Ă��Ȃ��Ƃ��@���@�ҋ@��Ԃł͂Ȃ��Ƃ�
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

