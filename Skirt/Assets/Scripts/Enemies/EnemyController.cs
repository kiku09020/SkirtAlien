using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector2 pos;
    public float spd = 1;
    public float y1;
    public float y2;

    // “®—Í
    public float moveforce = 30;

    float edgeX;

    PlayerCamera cam;
    Rigidbody2D rb;

    void Start()
    {
        GameObject cam_obj=GameObject.Find("PlayerCamera");

        rb=GetComponent<Rigidbody2D>();
        
        cam=cam_obj.GetComponent<PlayerCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        // ƒJƒƒ‰’[‚ÌÀ•WŽæ“¾
        edgeX = cam.scrnWidthWld;
        pos = transform.position;

        if (pos.y > y1) {
            rb.AddForce(Vector2.up * -moveforce);
        }

        else if (pos.y < y2) {
            rb.AddForce(Vector2.up * moveforce);
        }

        if (transform.position.x> edgeX) {
            transform.position =  new Vector2(-edgeX,pos.y) ;
        }

        else if(transform.position.x < -edgeX) { 
            transform.position =  new Vector2(edgeX,pos.y) ;
           
        }
    }
}
