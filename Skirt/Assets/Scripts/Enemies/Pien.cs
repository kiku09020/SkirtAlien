using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pien : MonoBehaviour
{
    [Header("�l")]
    [SerializeField] float speed = 1;
    [SerializeField] float idleTime = 2;
    bool isIdle; // �ҋ@��

    Rigidbody2D rb;
    Pien_Bottom underChecker;    //���̓����蔻��
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // ���̃`�F�b�N
        underChecker = transform.Find("groundChecker").gameObject.GetComponent<Pien_Bottom>();
    }

    void FixedUpdate()
    {
        float x = 1;

        // ���]
        if (transform.eulerAngles.y == 180) {
            x = -1;
        }
        else{
            x = 1;
        }

        // �n�ʂɃq�b�g���Ă��Ȃ��Ƃ��@���@�ҋ@��Ԃł͂Ȃ��Ƃ�
        if (!underChecker.isGroundHit && !isIdle) {
            underChecker.isGroundHit = true;
            StartCoroutine("ChangeRotate");
        }

        // �ҋ@��
        if ( isIdle ){
            rb.velocity = new Vector2(0, 0);
        }

        // �����Ă�Ƃ�
        else {
            rb.AddForce( Vector2.right * x * speed );
        }
    }

    IEnumerator ChangeRotate(){
        isIdle = true;      // �ҋ@��

        yield return new WaitForSeconds(2.0f);      // �҂�

        // ���]
        if (transform.eulerAngles.y == 180){
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }else{
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        // �ҋ@����
        isIdle=false;
    }
}

