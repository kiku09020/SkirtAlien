using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /* 値 */


    /* フラグ */


    /* オブジェクト */
    [Header("AudioSource")]
    [SerializeField] AudioSource as_BGM;        // BGM用AudioSource
    [SerializeField] AudioSource as_SE;         // SE用AudioSource

    [Header("AudioClip")]
    [SerializeField] List<AudioClip> BGM;       // BGM
    [SerializeField] List<AudioClip> SE_Pl;     // プレイヤー効果音
    [SerializeField] List<AudioClip> SE_enm;    // 敵効果音
    [SerializeField] List<AudioClip> SE_ui;     // ボタンなどのUIの効果音

    /* コンポーネント取得用 */



//-------------------------------------------------------------------

    void Start()
    {
        /* コンポーネント取得 */


        /* 初期化 */
        
    }

//-------------------------------------------------------------------

    void Update()
    {
        
    }

    //-------------------------------------------------------------------

    public void PlaySE(int seTypeNum, int seNum)
	{
        AudioClip clip = null;

        switch(seTypeNum) {
            // プレイヤー
            case (int)SELists.SETypeList.pl:
                clip = SE_Pl[seNum];
                break;

                // 敵
            case (int)SELists.SETypeList.enm:
                clip = SE_enm[seNum];
                break;

                // UI
            case (int)SELists.SETypeList.ui:
                clip = SE_ui[seNum];
                break;
		}

        // audioSoucreのclipに音声を設定して再生
        as_SE.clip = clip;
        as_SE.Play();
	}
}

// 効果音
public class SELists:MonoBehaviour 
{
    // SEの種類
    public enum SETypeList {
        pl,
        enm,
        ui,
	}

    // プレイヤー
    public enum SEList_Pl {
        damage,
        eat,
        heal,
        jump,
    }

    // 敵
    public enum SEList_Enm {

	}

    // UI
    public enum SEList_UI {

    }
}
