using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor2 : MonoBehaviour
{
    int cnter=0;
    public int cnterMax=60;

    Rigidbody2D rb;
    Vector2 pos;

    public int s;

    public GameObject obj; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        cnter++;

        if (cnter == cnterMax)
        {
            cnter=0;
            Instantiate<GameObject>(obj,transform.position,Quaternion.identity);
        }

        rb.MovePosition(new Vector2(pos.x + Mathf.PingPong(Time.time, s), pos.y));
    }
}
