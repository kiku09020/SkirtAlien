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

    // BGM再生
    public void PlayBGM(int bgmNum,bool loop)
	{
        if (loop) {
            as_BGM.loop = true;
        }

        else {
            as_BGM.loop = false;
        }

        AudioClip clip = BGM[bgmNum];

        as_BGM.clip = clip;
        as_BGM.Play();
	}

    // 効果音再生
    public void PlaySE(AudLists.SETypeList seTypeNum, int seNum)
	{
        AudioClip clip = null;

        switch((int)seTypeNum) {
            // プレイヤー
            case (int)AudLists.SETypeList.pl:
                clip = SE_Pl[seNum];
                break;

                // 敵
            case (int)AudLists.SETypeList.enm:
                clip = SE_enm[seNum];
                break;

                // UI
            case (int)AudLists.SETypeList.ui:
                clip = SE_ui[seNum];
                break;
		}

        // audioSoucreのclipに音声を設定して再生
        as_SE.clip = clip;
        as_SE.Play();
	}
}

// ------------------------------------------------------------------------

// 音声の列挙体クラス
public class AudLists:MonoBehaviour 
{
    // BGM
    public enum BGMList {
        stg_intro,
        stg_normal,     // 通常
        stg_fast,       // 速い
        stg_orgel,      // オルゴール調
        gameOver,       // ゲームオーバー
        clear,          // クリア
	}
    // ------------------------------------------

    // SEの種類
    public enum SETypeList {
        pl,
        enm,
        ui,
	}

    // プレイヤー
    public enum SEList_Pl {
        damage,
        dig,
        digDone,
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
