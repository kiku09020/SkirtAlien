using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector2 pos;
    float force = -1;
    public float spd = 1;
    public float y1 = 0;
    public float y2 = 1;

    // 動力
    public float moveforce=30;

    float edgeX;

    GameObject cam_obj;
    Pl_Camera cam;

    Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        cam_obj=GameObject.Find("PlayerCamera");

        rig=GetComponent<Rigidbody2D>();
        
        cam=cam_obj.GetComponent<Pl_Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // カメラ端の座標取得
        edgeX = cam.scrn_EdgeX;

        pos = transform.position;
        /*

        // （ポイント）マイナスをかけることで逆方向に移動する。
        transform.Translate(transform.up * Time.deltaTime * 3 * force);
                */
        if (pos.y > y1)
        {
            rig.AddForce(Vector2.up * -moveforce);
        }
        if (pos.y < y2)
        {
            rig.AddForce(Vector2.up * moveforce);
        }

        Debug.Log(edgeX);

        if (transform.position.x> edgeX)
        {
            transform.position =  new Vector2(-edgeX,pos.y) ;
        }

        else if(transform.position.x < -edgeX)
        {
            transform.position =  new Vector2(edgeX,pos.y) ;
           
        }
    }
}
