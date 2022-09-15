using UnityEngine;
using System.Collections.Generic;

/* ★パーティクルに関するスクリプトです */
public class ParticleManager : MonoBehaviour
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
        hungry,
        heal,
        goal,
        dead
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

    // 親オブジェクト指定
    public void InstPart(PartNames name, Vector2 genPos,Quaternion qua, Transform parent)
    {
        GameObject pref = particles[(int)name];
        GameObject inst = Instantiate(pref, genPos, qua, parent);      // 生成
        inst.GetComponent<ParticleSystem>().Play();                 // 再生
        Destroy(inst, destTime);                                    // 削除
    }

    // 削除フラグ
    public void InstPart(PartNames name, Vector2 genPos,bool delFlg)
    {
        GameObject pref = particles[(int)name];
        GameObject inst = Instantiate(pref, genPos, Quaternion.identity);      // 生成
        inst.GetComponent<ParticleSystem>().Play();                 // 再生

        if (delFlg) {
            Destroy(inst, destTime);                                    // 削除
        }
    }
}
