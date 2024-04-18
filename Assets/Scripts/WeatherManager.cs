using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public event Action onStartRain;

    public event Action onStartThunder;

    int count = 0;
    
    
    // Update is called once per frame
    void Update()
    {
        if(GAMEMANAGER.Instance.enemyPoints >=4 && count == 0)
        {
            count++;
            onStartRain();
        }
        if(GAMEMANAGER.Instance.enemyPoints >=7 && count == 1)
        {
            count++;
            InvokeThunder();
        }
    }

    private void InvokeRain()
    {
        onStartRain?.Invoke();
    }

    private void InvokeThunder()
    {
        onStartThunder?.Invoke();
    }
}
