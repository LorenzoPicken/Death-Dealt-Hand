using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    int index = 0;

    public void Play()
    {
        index = 1;
        Debug.Log("Play");
        SceneManager.LoadScene(index);
    }

    public void Close()
    {
        Application.Quit();
    }
}
