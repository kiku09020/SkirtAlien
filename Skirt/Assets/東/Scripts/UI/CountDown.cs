using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] Text countdown;
    [SerializeField] Transform canvas;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(countdown, canvas);
    }
    
    // Update is called once per frame
    void Update()
    {

    }
}
