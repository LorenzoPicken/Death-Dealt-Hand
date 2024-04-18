using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonManager : MonoBehaviour
{
    int index = 0;
    [SerializeField] Canvas MainMenuCanvas;
    [SerializeField] Canvas ExtrasCanvas;
    [SerializeField] Canvas RuleCanvas;

    public void Start()
    {
        ExtrasCanvas.enabled = false;
        RuleCanvas.enabled = false;
    }

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

    public void Extras()
    {
        MainMenuCanvas.enabled = false;
        ExtrasCanvas.enabled = true;
    }

    public void BackFromExtras()
    {
        MainMenuCanvas.enabled = true;
        ExtrasCanvas.enabled = false;
    }

    public void Rules()
    {
        RuleCanvas.enabled = true;
        MainMenuCanvas.enabled = false;
        ExtrasCanvas.enabled = false;
    }
    public void LeaveRules()
    {
        RuleCanvas.enabled = false;
        MainMenuCanvas.enabled = false;
        ExtrasCanvas.enabled = true;
    }
}
