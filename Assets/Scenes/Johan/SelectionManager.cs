using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public Transform playedCards;
    private RaycastHit hit;
    public Card selection1;
    public Card selection2;

    public static event Action OnCardSelected;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && selection1 == null && selection2 == null)
        {
            selection1 = SelectCard();
            OnCardSelected?.Invoke();
            return;
        }

        if (Input.GetMouseButtonDown(0) && selection1 != null && selection2 == null)
        {

            selection2 = SelectCard();
            if(selection1.Equals(selection2))
            {
                selection2 = null; 
                return;
            }
            CompareCards(selection1, selection2);
        }
    }

    private void CompareCards(Card card1, Card card2)
    {
        if(card1.cardValue == card2.cardValue)
        {
            card1.transform.position = playedCards.position;
            card2.transform.position = playedCards.position + Vector3.up;
            selection1 = null;
            selection2 = null;
        }
        else
        {
            selection2 = null;
        }
        if(card1 == null) { return; }
        if(card2 == null) { return; }
    }

    private Card SelectCard()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            var card = hit.transform.GetComponent<Card>();
            if (card.selectable)
            {
                return card;
            }
            else
            {
                Debug.Log("here");
            }
        }
        return null;
    }
}



