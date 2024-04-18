using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherEvents : MonoBehaviour
{
    [SerializeField] private AudioSource wind;
    [SerializeField] private AudioSource thunder;
    [SerializeField] private AudioSource rain;
    [SerializeField] private AudioSource storm;
    [SerializeField] private WeatherManager manager;


    [Range(0, 5)] public float timeBeforeWindStop = 0;
    

    private void OnEnable()
    {
        manager.onStartRain += Rain;
        manager.onStartThunder += Thunder;
    }

    private void OnDisable()
    {
        manager.onStartRain -= Rain;
        manager.onStartThunder -= Thunder;
    }
    // Start is called before the first frame update
    void OnStart()
    {
        wind.Play();
        thunder.Stop();
        rain.Stop();
        storm.Stop();
        
    }

    

    private void Rain()
    {
        StartCoroutine(StopWind());
        thunder.Play();
        rain.Play();
    }

    private void Thunder()
    {

        thunder.Play();
        rain.Play();
        storm.Play();
        
    }

    public IEnumerator StopWind()
    {
        yield return new WaitForSeconds(timeBeforeWindStop);

        wind.Stop();
    }

    public IEnumerator StopRain()
    {
        yield return new WaitForSeconds(timeBeforeWindStop);
        rain.Stop();
    }



}
