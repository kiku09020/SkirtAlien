using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick2 : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D col;

   // public int x, y;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
       rb.AddForce(new Vector2(0,-1));
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}