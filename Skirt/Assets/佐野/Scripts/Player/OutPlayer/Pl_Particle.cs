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

    public enum PartNames{
        damaged,
        jump,
        eat,
        eating,
        eated,
        swoop
	}

    /* フラグ */


    /* オブジェクト */
    GameObject pl_obj;

    /* コンポーネント取得用 */
    [SerializeField] GameObject part_damaged;
    [SerializeField] GameObject part_jump;
    [SerializeField] GameObject part_eat;       // 捕食空振り
    [SerializeField] GameObject part_eating;    // 捕食中
    [SerializeField] GameObject part_eated;     // 捕食完了
    [SerializeField] GameObject part_swoop;

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
    public void InstPart(PartNames name)
    {
        GameObject pref = part_damaged, inst;

        switch(name){
            case PartNames.damaged:
                pref = part_damaged;
                break;

            case PartNames.jump:
                pref = part_jump;
                break;

            case PartNames.eat:
                pref = part_eat;
                break;

            case PartNames.eating:
                pref = part_eating;
                break;

            case PartNames.eated:
                pref = part_eated;
                break;

            case PartNames.swoop:
                pref = part_swoop;
                break;
		}

        inst = Instantiate(pref, pos, Quaternion.identity);
        inst.GetComponent<ParticleSystem>().Play();
        Destroy(inst, destTime);
    } 
}
