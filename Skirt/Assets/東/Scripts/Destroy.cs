using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float Destroy_time = 3f;
    float time = 0f;
    private string target_tag = "Player";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == target_tag)
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        print (time);
        if(time>Destroy_time)
        {
            Destroy(this.gameObject);
        }
    }
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
