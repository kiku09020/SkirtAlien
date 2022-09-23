using UnityEngine;
using UnityEngine.UI;

/* ★プレイヤーのHPに関わるスクリプトです */
public class Pl_HP : MonoBehaviour
{
    /* 値 */
    [Header("HP関係")]
    public  float nowHP;                // 現在のHP
            float maxHP = 100;          // 最大HP

    [Header("表示関係")]
    [SerializeField] float dispVal;     // 表示HPを増減させる量
                     float dispHP;      // 表示HP
                     float flashTimer;  // 点滅用タイマー
    [SerializeField] float cautHP;      // 警告するHPの量
    bool cautFlg;                       // 警告したかどうか

    [Header("回復関係")]
    [SerializeField] float heal;        // 回復量
    
    [Header("ダメージ関係")]
    [SerializeField] float dmg;         // ダメージ

    /* コンポーネント取得用 */
    Image hp_Image;
    Image hp_Image_Light;

    AudioManager aud;
    ParticleManager part;

    //-------------------------------------------------------------------
    void Start()
    {
        GameObject hpGauge = GameObject.Find("HPBar");
        GameObject hpGauge_light = GameObject.Find("HPBar_Light");
        GameObject audObj = GameObject.Find("AudioManager");
        GameObject partObj = GameObject.Find("ParticleManager");

        aud = audObj.GetComponent<AudioManager>();
        part = partObj.GetComponent<ParticleManager>();

        hp_Image = hpGauge.GetComponent<Image>();
        hp_Image_Light = hpGauge_light.GetComponent<Image>();

        // 初期化
        hp_Image.fillAmount = 1;
        hp_Image_Light.fillAmount = 1;

        nowHP = maxHP;
        dispHP = nowHP;
    }

	void FixedUpdate()
	{
        HP_Set();
    }

    //-------------------------------------------------------------------
    // HPセット
    void HP_Set()
    {
        // 表示HPが今のHPよりも大きかったら、表示HP減らす
        if (nowHP < dispHP) {
            dispHP -= dispVal;
        }

        else if (nowHP > dispHP) {
            dispHP += dispVal;      
        }

        // 表示HP = 今のHPだったら揃える
        else if (dispHP == nowHP) {
            dispHP = nowHP;
        }

        // 表示
        hp_Image.fillAmount = nowHP / maxHP;             // 手前のHPバー
        hp_Image_Light.fillAmount = dispHP / maxHP;      // 薄い色のHPバー

        var imgClr = hp_Image.color;
        // 警告
        if (nowHP < cautHP) {
            if (!cautFlg) {
                aud.PlaySE(AudLists.SETypeList.ui, (int)AudLists.SEList_UI.caution);
                cautFlg = true;
            }

            // 点滅
            var alpha = Mathf.Cos(2 * Mathf.PI * (flashTimer / 0.3f));
            hp_Image.color = new Color(imgClr.r, imgClr.g, imgClr.b, alpha);

            flashTimer += Time.deltaTime;
        }

        else {
            cautFlg = false;
            flashTimer = 0;

            hp_Image.color = new Color(imgClr.r, imgClr.g, imgClr.b, 1);
        }
    }

    //-------------------------------------------------------------------
    // 回復
    public void HP_Heal()
    {
        // 回復
        nowHP += heal;

        // 最大HPより大きかったら、戻す
        if (nowHP > maxHP) {
            nowHP = maxHP;
        }

        aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.heal);       // 効果音再生
        part.InstPart(ParticleManager.PartNames.heal, transform.position);      // パーティクル
    }

    //-------------------------------------------------------------------
    // ダメージ
    public void HP_Damage()
    {
        nowHP -= dmg;

        aud.PlaySE(AudLists.SETypeList.pl, (int)AudLists.SEList_Pl.damage);     // 効果音
        part.InstPart(ParticleManager.PartNames.damaged, transform.position);   // パーティクル
    }
}
