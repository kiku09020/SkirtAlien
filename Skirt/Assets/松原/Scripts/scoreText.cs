using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class scoreText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var sqec=DOTween.Sequence();

        sqec.Append(transform.DOLocalMoveY(150, 1).SetEase(Ease.OutCirc));
        sqec.Append(transform.DOScale(0,0.3f).SetEase(Ease.InQuart));

        sqec.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
