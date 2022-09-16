using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pien : MonoBehaviour
{
    [Header("�l")]
    [SerializeField] float speed;           // �ړ����x
    [SerializeField] float idleTime;        // �ҋ@����
    bool isIdle;                            // �ҋ@�����ǂ���

    Rigidbody2D rb;
    Pien_Bottom btmCheck;                   //���Ƃ̓����蔻��
    
    void Start()
    {
        GameObject pienBtmObj = transform.Find("groundChecker").gameObject;
        btmCheck = pienBtmObj.GetComponent<Pien_Bottom>();

        rb = GetComponent<Rigidbody2D>();
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

        // �[�����ɂ����Ƃ�
        if (!btmCheck.isLanding && !isIdle) {
            StartCoroutine(ChangeDirection());
        }

        // �ҋ@��
        if ( isIdle ){
            rb.velocity = Vector2.zero;
        }

        // �����Ă�Ƃ�
        else {
            rb.AddForce( Vector2.right * x * speed );
        }
    }

    IEnumerator ChangeDirection(){
        btmCheck.isLanding = true;    // �G��Ă����Ԃɂ���
        isIdle = true;                  // �ҋ@��

        yield return new WaitForSeconds(idleTime);      // �҂�

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

