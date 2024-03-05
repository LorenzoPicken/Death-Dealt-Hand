using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerTableState : PlayerBaseState
{
  
    private List <Card> cardsToPlay = new List <Card> ();
    private int cardsSum;

    
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Enter state Player Table");
        cardsSum = 0;
        player.image.enabled = true;
        player.image.sprite = player.selectedCard.sprite;
        
    }

    public override void UpdateState(PlayerStateManager player)
    {
        // Return to Hand State, right click 
        if(Input.GetMouseButtonDown(1))
        {
            player.SwitchState(player.HandState);
        }

        // Pick Cards from table
        if (Input.GetMouseButtonDown(0))
        {
            Card card = player.SelectCard();
            
            // If the card selected is not already into the list
            if (card != null && !cardsToPlay.Contains(card))
            {
                cardsToPlay.Add(card);
                card.transform.position += Vector3.up * 0.25f;
                cardsSum += card.CardValue;
            }
            else
            {
                putCardDown(player);
            }

            Debug.Log(card);
        }

        // Confirm selection and Check, with Spacebar

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(cardsSum == player.selectedCard.CardValue)
            {
                foreach (Card card in cardsToPlay)
                {
                    player.table.cards.Remove(card);
                }
                Debug.Log("correct");
                MoveCards(player);
                return;
            }
            if(cardsSum != player.selectedCard.CardValue) 
            {
                foreach(Card card in cardsToPlay)
                {
                    card.transform.position -= Vector3.up * 0.25f;
                }
                cardsToPlay.Clear();
                Debug.Log("Incorrect: " + cardsSum);
                cardsSum = 0;
            }
        }

    }
    public override void ExitState(PlayerStateManager player)
    {
        foreach (Card card in cardsToPlay)
        {
            card.transform.position -= Vector3.up * 0.25f;
        }
        Debug.Log("Exit state Player Table");
        Debug.Log(cardsSum);
        cardsToPlay.Clear();
        player.image.enabled = false;
        player.camSwitch.SwitchToHand();
    }

    // Raycast that hits cardslot colliders, if there is no card in collider it will put selected card down
    private void putCardDown(PlayerStateManager player)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.tag == "cardslots")
            {
                Debug.Log("card slot collider hit");

                var cardslot = hit.collider.GetComponent<CardSlot>();

                if (cardslot != null && cardslot.available == true) 
                {
                    player.selectedCard.transform.position = cardslot.transform.position;
                    player.selectedCard.transform.rotation = cardslot.transform.rotation;
                    player.table.cards.Add(player.selectedCard);
                    player.playerCards.Remove(player.selectedCard);
                    GAMEMANAGER.Instance.currentRoundState = RoundState.CHECKPLAYSTATE;
                    player.SwitchState(player.HandState);

                }
            }
        }
    }

    // If cards are correct, they will move to a predetermined transform position
    private void MoveCards(PlayerStateManager player)
    {
        foreach (Card card in cardsToPlay)
        {
            card.transform.position = player.playedCardsTransform.transform.position;
            card.transform.rotation = player.playedCardsTransform.transform.rotation;
            player.table.playedCards.Add(card);
            player.table.cards.Remove(card);
        }
        player.table.playedCards.Add(player.selectedCard);
        player.playerCards.Remove(player.selectedCard);
        player.selectedCard.transform.position = player.playedCardsTransform.transform.position;
        player.selectedCard.transform.rotation = player.playedCardsTransform.transform.rotation;
        player.SwitchState(player.HandState);
        cardsToPlay.Clear();
        GAMEMANAGER.Instance.currentRoundState = RoundState.CHECKPLAYSTATE;

    }

}
