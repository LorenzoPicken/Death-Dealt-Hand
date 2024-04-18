using UnityEngine.Audio;
using UnityEngine;
using System;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    int randNum;
    int lastRandInt;
    float randTime;
    float currentTime;
    
    public static AudioManager Instance;
    public Sound[] sounds;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);

        
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.loop = s.loop;
        }
        randTime = 60; currentTime = 0;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > randTime) 
        { 
            PlayRandomSound();
        }
    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s?.source.Play();
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;
        
    }

    private void PlayRandomSound()
    {
        
        currentTime = 0 ;
        randTime = UnityEngine.Random.Range(60, 90);
        randNum = UnityEngine.Random.Range(6, sounds.Length);
        if (randNum == lastRandInt && lastRandInt == sounds.Length - 1)
        {
            randNum = lastRandInt - 1;
        }
        else if(randNum == lastRandInt) 
        {
            randNum += 1;
        }
        Play(sounds[randNum].name);
        lastRandInt = randNum;

    }
}
