using UnityEngine;
using System.Collections.Generic;

/* ★パーティクルに関するスクリプトです */
public class Pl_Particle : MonoBehaviour
{
    /* 値 */
    Vector2 pos;
    [SerializeField] float destTime = 3;        // 削除までの時間

    public enum PartNames{
        damaged,
        jump,
        eat,
        digit,
        eated,
        swoop,
        hungry,
        heal,
        goal
	}

    /* コンポーネント取得用 */
    [SerializeField] List<GameObject> particles;

//-------------------------------------------------------------------
    // パーティクルの生成
    public void InstPart(PartNames name,Vector2 genPos)
    {
        GameObject pref = particles[(int)name];
        GameObject inst = Instantiate(pref, genPos, Quaternion.identity);      // 生成
        inst.GetComponent<ParticleSystem>().Play();                 // 再生
        Destroy(inst, destTime);                                    // 削除
    } 
}
