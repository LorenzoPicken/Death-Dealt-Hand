using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardSlot : MonoBehaviour
{
    public bool available = true;
    Card card;
    private void OnAwake()
    {
        available = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "card")
        available = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "card")
            available = true;
    }

   

}
