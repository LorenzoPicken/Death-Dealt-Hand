using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public AudioSource m_AudioSourceClap;
    public AudioSource m_AudioSourceLaugh;
    private int n = 0;
    public void clap()
    {
        n++;
        m_AudioSourceClap.Play();
        Debug.Log("clap number" + n);
    }

    public void Laugh()
    {
        m_AudioSourceLaugh.Play();
    }
}
