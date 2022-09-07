using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /* オブジェクト */
    [Header("AudioSource")]
    [SerializeField] AudioSource as_BGM;        // BGM用AudioSource
    [SerializeField] AudioSource as_SE;         // SE用AudioSource

    [Header("AudioClip")]
    [SerializeField] List<AudioClip> BGM;       // BGM
    [SerializeField] List<AudioClip> SE_Pl;     // プレイヤー効果音
    [SerializeField] List<AudioClip> SE_enm;    // 敵効果音
    [SerializeField] List<AudioClip> SE_ui;     // ボタンなどのUIの効果音

    //-------------------------------------------------------------------
    // BGM再生
    public void PlayBGM(int bgmNum,bool loop)
	{
        // ループの有無
        if (loop) {
            as_BGM.loop = true;
        }
        else {
            as_BGM.loop = false;
        }

        as_BGM.clip = BGM[bgmNum];          // クリップ入れる
        as_BGM.Play();                      // 再生
	}

    // 効果音再生
    public void PlaySE(AudLists.SETypeList seTypeNum, int seNum)
	{
        AudioClip clip = null;

        switch((int)seTypeNum) {
            case (int)AudLists.SETypeList.pl:   // プレイヤー
                clip = SE_Pl[seNum];    break;

            case (int)AudLists.SETypeList.enm:  // 敵
                clip = SE_enm[seNum];   break;

            case (int)AudLists.SETypeList.ui:   // UI
                clip = SE_ui[seNum];    break;
		}

        as_SE.clip = clip;      // クリップ入れる
        as_SE.Play();           // 再生
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
        push,       // 押したとき
        cancel,     // キャンセル
        decision    // 決定
    }
}
