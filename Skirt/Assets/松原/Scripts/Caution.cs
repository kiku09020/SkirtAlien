using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Caution : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var sqec = DOTween.Sequence();

        sqec.Append(transform.DOScale(new Vector2(0f, 0f), 0f).SetDelay(1f));
        sqec.Append(transform.DOScale(new Vector2(1.2f, 1.2f), 0.3f));
        sqec.Append(transform.DOScale(new Vector2(1f, 1f), 0.1f));
    }

    // Update is called once per frame
    void Update()
    {

    }
}