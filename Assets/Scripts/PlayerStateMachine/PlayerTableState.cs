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
    private bool sameCardTable;
    private List <Card> sameCardList = new List <Card> ();

    
    public override void EnterState(PlayerStateManager player)
    {
        
        cardsSum = 0;
        player.image.enabled = true;
        player.image.sprite = player.selectedCard.sprite;
        foreach (Card card in player.table.cards) 
        {

            if (card.CardValue == player.selectedCard.CardValue)
            {
                sameCardTable = true;
                sameCardList.Add(card);
                var outline = card?.GetComponent<Outline>();
                outline.OutlineColor = Color.green;
                outline.enabled = true;
                Debug.Log("Match found");
            }
        }
        
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
            if (!sameCardTable)
            {
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
            }

            if(card != null && card.CardValue == player.selectedCard.CardValue && sameCardTable)
            {
                cardsToPlay.Add(card);
                MoveCards(player);
                GAMEMANAGER.Instance.hasDrawnEffect = false;
                AudioManager.Instance.Play("PlaceCard");
                GAMEMANAGER.Instance.currentRoundState = RoundState.ENEMYTURN;
                return;
            }
          
                
        }


        // If match is not found in table, then check if cards value sum is equal to the selected card
        
        if (!sameCardTable)
        {
            if (cardsSum == player.selectedCard.CardValue)
            {
                foreach (Card card in cardsToPlay)
                {
                    player.table.cards.Remove(card);
                }

                MoveCards(player);
                GAMEMANAGER.Instance.hasDrawnEffect = false;
                AudioManager.Instance.Play("PlaceCard");
                GAMEMANAGER.Instance.currentRoundState = RoundState.ENEMYTURN;
                return;
            }
            if (cardsSum > player.selectedCard.CardValue)
            {
                foreach (Card card in cardsToPlay)
                {
                    card.transform.position -= Vector3.up * 0.25f;
                }
                cardsToPlay.Clear();

                cardsSum = 0;
            }
        }

    }
    public override void ExitState(PlayerStateManager player)
    {
        foreach (Card card in cardsToPlay)
        {
            var outline = card.GetComponent<Outline>();
            card.transform.position -= Vector3.up * 0.25f;
            outline.enabled = false;
        }
        foreach(Card card in sameCardList)
        {
           var outline = card.GetComponent<Outline>();
           outline.OutlineColor = Color.white;
           outline.enabled = false;
        }
        sameCardTable = false;
        sameCardList.Clear();
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
                

                var cardslot = hit.collider.GetComponent<CardSlot>();

                if (cardslot != null && cardslot.available == true) 
                {
                    player.selectedCard.transform.position = cardslot.transform.position;
                    player.selectedCard.transform.rotation = cardslot.transform.rotation;
                    player.selectedCard.inHand = false;
                    player.table.cards.Add(player.selectedCard);
                    player.playerCards.Remove(player.selectedCard);
                    AudioManager.Instance.Play("PlaceCard");
                    GAMEMANAGER.Instance.hasDrawnEffect = false;
                    GAMEMANAGER.Instance.currentRoundState = RoundState.ENEMYTURN;
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
        player.selectedCard.inHand = false;
        player.SwitchState(player.HandState);
        cardsToPlay.Clear();
        EventManager.InvokePlayerPickup();
        GAMEMANAGER.Instance.CalculateTableTotal();
        
        if(GAMEMANAGER.Instance.tableTotal == 0)
        {
            if(GAMEMANAGER.Instance.playerEffectTokens <= 2)
            {
                GAMEMANAGER.Instance.playerEffectTokens++;

            }
            GAMEMANAGER.Instance.UpdateUI();
        }

        if(GAMEMANAGER.Instance.WasPickupOverride == false)
        {
            Debug.Log("Priority is Set To Player");
            GAMEMANAGER.Instance.currentPrio = PickupPrio.PLAYER;

        }
        GAMEMANAGER.Instance.hasDrawnEffect = false;
        GAMEMANAGER.Instance.currentRoundState = RoundState.ENEMYTURN;

    }

}
