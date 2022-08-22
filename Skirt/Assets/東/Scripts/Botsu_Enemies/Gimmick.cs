using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gimmick : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D col;

    public int x, y;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(x, y);
    }
    
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}