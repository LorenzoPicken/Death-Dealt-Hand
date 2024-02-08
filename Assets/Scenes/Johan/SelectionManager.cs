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
    public static event Action OnCardDeselected;
    private void Update()
    {
        // If left click and selection null select a card 
        if (Input.GetMouseButtonDown(0) && selection1 == null && selection2 == null)
        {
            selection1 = SelectCard();
            if (selection1 != null) { OnCardSelected?.Invoke(); }
            return;
        }

        // If left click and selection1 is not null, compare both cards 
        if (Input.GetMouseButtonDown(0) && selection1 != null && selection2 == null)
        {

            selection2 = SelectCard();
            if (selection1.Equals(selection2))
            {
                selection2 = null;
                return;
            }
            if (selection1 != null && selection2 != null)
            {
                CompareCards(selection1, selection2);
            }
        }

        // If right click deselect cards and unable table cards 
        if (Input.GetMouseButtonDown(1))
        {
            Deselect();
            OnCardDeselected?.Invoke();
        }
    }

    private void CompareCards(Card card1, Card card2)
    {
        if (card1.cardValue == card2.cardValue)
        {
            card1.transform.position = playedCards.position;
            card2.transform.position = playedCards.position + Vector3.up;
            selection1 = null;
            selection2 = null;
            OnCardDeselected?.Invoke();
        }
        else
        {
            selection2 = null;
        }
      
    }


    private void Deselect()
    {
        selection1 = null;
    }

    // Select a card which selectable attribute is true through Raycast 
    private Card SelectCard()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "card")
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
            if (hit.collider.tag == "TableSlots")
            {
                var cardSlots = hit.collider.gameObject.GetComponent<CardSlot>();
                if (cardSlots.available)
                {
                    selection1.transform.position = cardSlots.transform.position;
                    OnCardDeselected?.Invoke();
                    selection1 = null;
                }
                else
                {
                    Debug.Log("not available");
                }

            }
        }
            return null;

    }
        private void PutDownCard(Card card)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "TableSlots")
                {
                    var cardSlots = hit.collider.gameObject.GetComponent<CardSlot>();
                    if (cardSlots.available)
                    {
                        card.transform.position = cardSlots.transform.position;
                    }
                    else
                    {
                        Debug.Log("not available");
                    }

                }
            }
        }
    } 



