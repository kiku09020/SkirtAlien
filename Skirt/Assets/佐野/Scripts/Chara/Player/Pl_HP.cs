using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pl_HP : MonoBehaviour
{
    /* 値 */
    [Header("HP関係")]
    public float nowHP=100;
    float maxHP=100;
    public float heal;

    [Header("ダメージ関係")]
    public float dmg;      // ダメージ
    public float decDmg;   // 形状変化中のダメージ

    /* フラグ */


    /* オブジェクト */
    GameObject hpGauge;

    /* コンポーネント取得用 */
    Image hp_Image;
    

    //-------------------------------------------------------------------

    void Start()
    {
        hpGauge = GameObject.Find("HPBar");
        hp_Image = hpGauge.GetComponent<Image>();

        // 初期化
        hp_Image.fillAmount = 1;

        nowHP = 100;
        maxHP = 100;
    }

	void Update()
	{
        HP_Set();
	}

	// HPセット
	void HP_Set()
    {
        hp_Image.fillAmount = nowHP/ maxHP;
    }
}
