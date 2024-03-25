using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0,100)]
    public float volume;

    [Range(0,5)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
