using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] List<GameObject> cards = new List<GameObject> { };
    [SerializeField] float moveSpeed;
    [SerializeField] float hoverHeight;
    [SerializeField] GameObject playerDeck;
    [SerializeField] float stackOffset;
    int hoverCount = 0;

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) { Hover(); }

        //if (Input.GetKeyDown(KeyCode.P)) { StartCoroutine(PickUP()); }
    }

    public void Hover()
    {
        if (hoverCount == 0)
        {
            foreach (GameObject card in cards)
            {
                Vector3 cardPosition = card.transform.position;
                cardPosition.y += hoverHeight;
                card.transform.position = cardPosition;
            }
            hoverCount++;
        }
    }

    //IEnumerator PickUP()
    //{
    //    
    //}
}
