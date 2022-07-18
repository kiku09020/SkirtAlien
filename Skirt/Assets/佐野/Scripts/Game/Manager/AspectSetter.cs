using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* ★Androidの解像度を揃えるスクリプトです */
public class AspectSetter : MonoBehaviour
{
    [Header("値")]
    [SerializeField] Vector2 trgAspctVec;       // 目的の解像度

    /* オブジェクト */
    [Header("オブジェクト")]
    [SerializeField] Camera cam;                // カメラ

    //-------------------------------------------------------------------

    void Start()
    {
        AspSet();
    }

    //-------------------------------------------------------------------

    void Update()
    {

    }

    //-------------------------------------------------------------------

    void AspSet()
    {
        // アスペクト比
        float nowAspct = (float)Screen.width / (float)Screen.height;    // 現在のアスペクト比
        float trgAspct = trgAspctVec.x / trgAspctVec.y;                 // 目的のアスペクト比

        // 目的のアスペクト比にするための倍率を求める
        float aspctRate = trgAspct / nowAspct;

        // Camera の ViewPortRectにするための変数
        Rect camVPR = new Rect(0, 0, 1, 1);

        // 倍率が1より小さかったら幅、そうじゃなかったら高さを指定
        if (aspctRate < 1) {
            camVPR.width = aspctRate;
            camVPR.x = 0.5f - camVPR.width * 0.5f;
        }

        else {
            camVPR.height = 1 / aspctRate;
            camVPR.y = 0.5f - camVPR.height * 0.5f;
        }

        // カメラに適用
        cam.rect = camVPR;
    }
}
