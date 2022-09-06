using UnityEngine;

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
        swoop
	}

    /* オブジェクト */
    GameObject pl_obj;

    /* コンポーネント取得用 */
    [SerializeField] GameObject part_damaged;
    [SerializeField] GameObject part_jump;
    [SerializeField] GameObject part_eat;       // 捕食空振り
    [SerializeField] GameObject part_digit;    // 捕食中
    [SerializeField] GameObject part_eated;     // 捕食完了
    [SerializeField] GameObject part_swoop;

//-------------------------------------------------------------------
    void Start()
    {
        pl_obj = GameObject.Find("Player");
    }

//-------------------------------------------------------------------
    void FixedUpdate()
    {
        // プレイヤーの位置取得
        pos = pl_obj.transform.position;
    }

//-------------------------------------------------------------------
    // パーティクルの生成
    public void InstPart(PartNames name)
    {
        GameObject pref = part_damaged, inst;

        switch(name){
            case PartNames.damaged:     // ダメージ
                pref = part_damaged;    break;

            case PartNames.jump:        // ジャンプ
                pref = part_jump;       break;

            case PartNames.eat:         // 捕食
                pref = part_eat;        break;

            case PartNames.digit:      // 消化
                pref = part_digit;     break;

            case PartNames.eated:       // 消化完了
                pref = part_eated;      break;

            case PartNames.swoop:       // 急降下
                pref = part_swoop;      break;
		}

        inst = Instantiate(pref, pos, Quaternion.identity);     // 生成
        inst.GetComponent<ParticleSystem>().Play();             // 再生
        Destroy(inst, destTime);                                // 削除
    } 
}
