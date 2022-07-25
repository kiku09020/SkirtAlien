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
        rb.useGravity = false; //最初にrigidBodyの重力を使わなくする
    }

    private void FixedUpdate () 
    {
        SetLocalGravity (); //重力をAddForceでかけるメソッドを呼ぶ。FixedUpdateが好ましい。
    }

    private void SetLocalGravity()
    {
        rb.AddForce (localGravity, ForceMode.Acceleration);
    }
}