using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballon : MonoBehaviour
{
    [SerializeField] float x, y;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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