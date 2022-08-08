using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* ★ゲーム全般の処理を行うスクリプトです
 * ・シーン管理
 * ・ステージ管理
 * ・スティック管理
 * 
 * ※肥大化してきたら細分化します
 */
public class GameManager : MonoBehaviour
{
    /* 値 */
    [Header("スマホ用の値")]
    public float inpVer;                    // 垂直(縦)
    public float inpHor;                    // 平行(横)
    public float inpVerOld, inpHorOld;      // ひとつ前の入力値
    Joystick stick;                         // スティック

    [Header("フラグ")]
    public bool isGameOver;        // ゲームオーバー
    public bool isPaused;

    /* オブジェクト */

    /* コンポーネント取得用 */

//-------------------------------------------------------------------

    void Awake()
    {
        /* オブジェクト検索 */

        /* コンポーネント取得 */
        stick = GameObject.Find("Stick").GetComponent<Joystick>();

        /* 初期化 */

        isPaused = false;
    }

//-------------------------------------------------------------------

    void Update()
    {
        // 入力値
        inpVerOld = inpVer; inpHorOld = inpHor;

        inpVer = stick.Horizontal;
        inpHor = stick.Vertical;
    }

//-------------------------------------------------------------------
}
