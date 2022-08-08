using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class insekimadoshi : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 pos;

    public GameObject obj;
    public int s;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pos = transform.position;
    }

    // Update is called once per frame
    public Rigidbody projectile;
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Rigidbody clone;
            clone = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
            clone.velocity = transform.TransformDirection(Vector3.forward * 100);
        }
    }
}