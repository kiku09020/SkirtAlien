using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★〇〇に関するスクリプトです */
//-------------------------------------------------------------------
public class Pl_Particle : MonoBehaviour
{
    /* 値 */
    Vector2 pos;
    [SerializeField] float destTime = 3;        // 削除までの時間

    /* フラグ */


    /* オブジェクト */
    GameObject pl_obj;

    /* コンポーネント取得用 */
    [SerializeField] GameObject part_damaged;
    [SerializeField] GameObject part_jump;
    [SerializeField] GameObject part_eat;
    [SerializeField] GameObject part_swoop;

    GameObject inst_dmg;
    GameObject inst_jump;
    GameObject inst_eat;
    GameObject inst_swp;

//-------------------------------------------------------------------
    void Start()
    {
        FindObj();
        GetComp();        

        /* 初期化 */
        
    }

    /* オブジェクト検索 */
    void FindObj()
    {
        pl_obj = GameObject.Find("Player");
    }

    /* コンポーネント取得 */
    void GetComp()
    {

    }

//-------------------------------------------------------------------

    void FixedUpdate()
    {
        // プレイヤーの位置取得
        pos = pl_obj.transform.position;
    }

//-------------------------------------------------------------------
    // ダメージ
    public void Part_Damaged()
    {
        inst_dmg = Instantiate(part_damaged, pos, Quaternion.identity);         // 生成
        inst_dmg.GetComponent<ParticleSystem>().Play();                         // 再生
        Destroy(inst_dmg, destTime);                                            // 5秒後に削除
    }    
    
    // ジャンプ
    public void Part_Jumping()
    {
        inst_jump = Instantiate(part_jump, pos, Quaternion.identity);           // 生成
        inst_jump.GetComponent<ParticleSystem>().Play();                        // 再生
        Destroy(inst_jump, destTime);                                           // 5秒後に削除
    }    

    // 捕食
    public void Part_Eating()
    {
        inst_eat = Instantiate(part_eat, pos, Quaternion.identity);             // 生成
        inst_eat.GetComponent<ParticleSystem>().Play();                         // 再生
        Destroy(inst_eat, destTime);                                            // 5秒後に削除
    }    

    // パーティクルの生成
    public void Part_Swooping()
    {
        inst_swp = Instantiate(part_swoop, pos, Quaternion.identity);           // 生成
        inst_swp.GetComponent<ParticleSystem>().Play();                         // 再生
        Destroy(inst_swp, destTime);                                            // 5秒後に削除
    }    
}
