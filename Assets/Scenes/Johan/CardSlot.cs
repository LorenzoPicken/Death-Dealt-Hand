using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardSlot : MonoBehaviour
{
    public bool available = true;
    Card card;
    Outline outline;
  
    private void Awake()
    {

        available = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        card = other.GetComponent<Card>();
        if(card != null )
        {
            outline = card.GetComponent<Outline>();
            outline.enabled = false;
            outline.OutlineWidth = 5f;
            outline.OutlineMode = Outline.Mode.OutlineVisible;
        }
        if(other.tag == "card")
        available = false;

    }

    private void OnTriggerExit(Collider other)
    {
        card = null;
        if (other.tag == "card")
            available = true;
    }

    private void OnMouseOver()
    {
      if(card != null)
      outline.enabled = true;
    }

    private void OnMouseExit()
    {
        if (card != null)
            outline.enabled = false;
    }

}
