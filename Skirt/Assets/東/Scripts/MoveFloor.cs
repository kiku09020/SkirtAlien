using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    
    Rigidbody2D rb;
    Vector2 pos;

    public int s;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(new Vector2(pos.x + Mathf.PingPong(Time.time, s), pos.y));
    }
}
