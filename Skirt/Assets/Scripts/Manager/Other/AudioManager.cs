using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /* オブジェクト */
    [Header("AudioSource")]
    [SerializeField] AudioSource as_BGM;        // BGM用AudioSource
    [SerializeField] AudioSource as_SE;         // SE用AudioSource
    [SerializeField] AudioSource as_SE_UI;      // UI用

    [Header("AudioClip")] 
    [SerializeField] List<AudioClip> BGM;       // BGM
    [SerializeField] List<AudioClip> SE_Pl;     // プレイヤー効果音
    [SerializeField] List<AudioClip> SE_enm;    // 敵効果音
    [SerializeField] List<AudioClip> SE_ui;     // ボタンなどのUIの効果音
    [SerializeField] List<AudioClip> SE_Score;

    //-------------------------------------------------------------------
    // BGM再生
    public void PlayBGM(AudLists.BGMList bgmNum, bool loop)
	{
        // ループの有無
        if (loop) {
            as_BGM.loop = true;
        }
        else {
            as_BGM.loop = false;
        }

        as_BGM.clip = BGM[(int)bgmNum];          // クリップ入れる
        as_BGM.Play();                      // 再生
	}

    // 効果音再生
    public void PlaySE(AudLists.SETypeList seTypeNum, int seNum)
	{
        AudioClip clip = null;

        switch((int)seTypeNum) {
            case (int)AudLists.SETypeList.pl:   // プレイヤー
                clip = SE_Pl[seNum];    
                as_SE.clip = clip;
                as_SE.Play();
                break;

            case (int)AudLists.SETypeList.ui:   // UI
                clip = SE_ui[seNum];    
                as_SE_UI.clip = clip;
                as_SE_UI.Play();
                break;

            case (int)AudLists.SETypeList.score:
                clip = SE_Score[seNum];
                as_SE_UI.PlayOneShot(clip);
                break;
        }

	}

    //-------------------------------------------------------------------
    // 音声の一時停止
    public void PauseAudio(bool stopFlg)
    {
        // 停止時
        if (stopFlg) {
            as_BGM.Pause();
            as_SE.Pause();
        }

        // 停止再開時
        else {
            as_BGM.Play();
            as_SE.Play();
        }
    }
}

// ------------------------------------------------------------------------
// 音声の列挙体クラス
public class AudLists:MonoBehaviour 
{
    // BGM
    public enum BGMList {
        stg_intro,      // イントロ
        stg_normal,     // 通常
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
        score,
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