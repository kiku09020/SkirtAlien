using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGravity : MonoBehaviour 
{
    [SerializeField] private Vector2 localGravity;
    private Rigidbody rb;

    // Use this for initialization
    private void Start () 
    {
        rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false; //�ŏ���rigidBody�̏d�͂��g��Ȃ�����
    }

    private void FixedUpdate () 
    {
        SetLocalGravity (); //�d�͂�AddForce�ł����郁�\�b�h���ĂԁBFixedUpdate���D�܂����B
    }

    private void SetLocalGravity()
    {
        rb.AddForce (localGravity, ForceMode.Acceleration);
    }
}