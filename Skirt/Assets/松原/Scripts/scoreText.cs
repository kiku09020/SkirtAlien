using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class scoreText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {
        var sqec = DOTween.Sequence();

        sqec.Append(transform.DOLocalMoveY(600, 1).SetEase(Ease.OutCubic));
        sqec.Append(transform.DOScale(0, 0.5f).SetEase(Ease.InOutCubic));

        sqec.Play();
    }
}
