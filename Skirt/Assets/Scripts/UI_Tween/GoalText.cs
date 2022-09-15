using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoalText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // var sqec = DOTween.Sequence();

        transform.DOPunchScale(Vector2.one, 0.5f);

        /*
        sqec.Append(transform.DOScale(scale + Vector2.one, 0.1f));
        sqec.Append(transform.DOScale(scale + new Vector2(0.25f, 0.25f), 0.1f));
        sqec.Append(transform.DOScale(scale + new Vector2(1.5f, 1.5f), 0.1f));
        sqec.Append(transform.DOScale(scale + new Vector2(0.5f, 0.5f), 0.1f));
        sqec.Append(transform.DOScale(scale, 0.1f));
        */
        
        /*
        transform.DOScale(0.1f, 1f)
        .SetRelative(true)
        .SetEase(Ease.OutQuart)
        .SetLoops(-1, LoopType.Restart);
        */

        /*
        transform.DOLocalMove(new Vector3(1, 2), 0.5f)
         .SetDelay(1f)
         .SetRelative()
         .SetEase(Ease.OutQuad)
         .SetLoops(2, LoopType.Incremental);
        */
    }

    // Update is called once per frame
    void Update()
    {

    }
}
