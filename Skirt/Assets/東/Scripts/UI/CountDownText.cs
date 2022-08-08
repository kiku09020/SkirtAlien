using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownText : MonoBehaviour
{
    public float startTime;     // �J�E���g�_�E���̍ŏ��̐�
    float time;                 // ����
    int sec;                    // �b��

    Text cndnText;



    // Start is called before the first frame update
    void Start()
    {
        cndnText = GetComponent<Text>();
        time = startTime + 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time -= Time.deltaTime;
        sec = (int)time;

        print(sec);
        cndnText.text = sec.ToString();

        if (time < 1) {
            cndnText.text = "�X�^�[�g!";
        }

        if (time < 0) {
            Destroy(gameObject);
        }
    }
}
