using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckOfCards : MonoBehaviour
{
    Outline outline;
    private void Awake()
    {
       outline = gameObject.AddComponent<Outline>();
       outline.OutlineWidth = 5f;
       outline.OutlineMode = Outline.Mode.OutlineVisible;
       outline.OutlineColor = Color.red; 
       outline.enabled = false;
    }

    void OnMouseOver()
    {
        outline.enabled =true;
    }

    private void OnMouseExit()
    {
        outline.enabled =false; 
    }

    public void ToggleColor()
    {
        if(outline.OutlineColor == Color.red) {
            outline.OutlineColor = Color.green;
        }
        else if(outline.OutlineColor == Color.green) { outline.OutlineColor = Color.red;}
    }
    
}
