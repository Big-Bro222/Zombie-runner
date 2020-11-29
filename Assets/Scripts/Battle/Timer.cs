using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer:MonoBehaviour
{
    public UnityEvent onCalledTimer;
    public bool isCounting;
    private static Timer _instance;
    public static Timer Instance { get { return _instance; } }


    private void Awake()
    {
        isCounting = true;
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Start()
    {
        StartCoroutine(TimerCount());
    }


    IEnumerator TimerCount()
    {
        while (isCounting)
        {
            onCalledTimer.Invoke();
        }
        yield return new WaitForSeconds(15);
    }



}
