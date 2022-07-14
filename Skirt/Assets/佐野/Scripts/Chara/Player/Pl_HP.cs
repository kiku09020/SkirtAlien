using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* ★プレイヤーのHPに関わるスクリプトです */
public class Pl_HP : MonoBehaviour
{
    /* 値 */
    [Header("HP関係")]
    public  float nowHP = 100;                  // 現在のHP
            float maxHP = 100;                  // 最大HP

    [Header("表示関係")]
    [SerializeField] float dispDec = 0.1f;      // 表示HPを減らす量
                     float dispHP;              // 表示HP

    [Header("回復関係")]
    [SerializeField] float heal  =  10;         // 回復量
    
    [Header("ダメージ関係")]
    [SerializeField] float dmg   = 20;          // ダメージ
    [SerializeField] float decDmg;              // 形状変化中のダメージ

    /* フラグ */


    /* オブジェクト */
    GameObject hpGauge;
    GameObject hpGauge_light;

    /* コンポーネント取得用 */
    Image hp_Image;
    Image hp_Image_Light;

    //-------------------------------------------------------------------

    void Start()
    {
        hpGauge = GameObject.Find("HPBar");
        hpGauge_light = GameObject.Find("HPBar_Light");

        hp_Image = hpGauge.GetComponent<Image>();
        hp_Image_Light = hpGauge_light.GetComponent<Image>();

        // 初期化
        hp_Image.fillAmount = 1;
        hp_Image_Light.fillAmount = 1;

        nowHP = 100;
        maxHP = 100;
        dispHP = nowHP;
    }

	void Update()
	{
        HP_Set();
    }

	// HPセット
	void HP_Set()
    {
        // 表示HPが今のHPよりも大きかったら、表示HP減らす
        if(nowHP < dispHP) {
            dispHP -= 0.1f;
        }

        // 表示HP = 今のHPだったら揃える
        if (dispHP == nowHP) {
            dispHP = nowHP;
        }

        // 表示
        hp_Image.fillAmount = nowHP/ maxHP;
        hp_Image_Light.fillAmount = dispHP/ maxHP;
        
    }

    // 回復
    public void HP_Heal()
    {
        // 最大HP以下のとき回復
        if(nowHP < maxHP) {
            nowHP += heal;
        }

        // 最大HP以上だったら、最大HPにもどす
        else {
            nowHP = maxHP;
        }
    }

    // ダメージ
    public void HP_Damage()
    {
        nowHP -= dmg;
    }

    // 経過ダメージ
    public void HP_DecDamage()
    {
        nowHP -= decDmg;
    }
}
