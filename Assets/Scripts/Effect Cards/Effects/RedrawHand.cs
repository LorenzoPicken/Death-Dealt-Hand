using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedrawHand : MonoBehaviour
{
    [SerializeField] PlayerStateManager player;
    [SerializeField] AIBehaviour enemy;
    
    
    
    public  void Execute()
    {
        int cardsInHand = 0;
        if (GAMEMANAGER.Instance.currentRoundState == RoundState.PLAYERTURN)
        {
            foreach(Card card in player.playerCards)
            {
                card.transform.position = player.playedCardsTransform.position;
                //Visually Send Card Back To Deck
            }
            cardsInHand = player.playerCards.Count;
            for(int i = 0; i < cardsInHand; i++)
            {
                player.playerCards[0].InHand = false;
                GAMEMANAGER.Instance.deck.Add(player.playerCards[0]);
                player.playerCards.Remove(player.playerCards[0]);
                
            }

            //Reshuffle Deck Visualy
            //GAMEMANAGER.Instance.deck = GAMEMANAGER.Instance.Shuffle(GAMEMANAGER.Instance.deck);
            int count = 0;
            foreach(Transform playerSlot in GAMEMANAGER.Instance.playerSlots)
            {
                if(count < cardsInHand)
                {
                    GAMEMANAGER.Instance.deck[0].transform.position = playerSlot.transform.position;
                    GAMEMANAGER.Instance.deck[0].transform.rotation = playerSlot.transform.rotation;
                    GAMEMANAGER.Instance.deck[0].inHand = true;
                    player.playerCards.Add(GAMEMANAGER.Instance.deck[0]);
                    StartCoroutine(GAMEMANAGER.Instance.dissolvingEffect(GAMEMANAGER.Instance.deck[0]));
                    GAMEMANAGER.Instance.deck.Remove(GAMEMANAGER.Instance.deck[0]);
                    count++;

                }


                //Replace Card Teleport With Animation
            }



        }
        else
        {
            foreach (Card card in enemy.handList)
            {
                card.transform.position = player.playedCardsTransform.position;
                //Visually Send Card Back To Deck
            }
            cardsInHand = enemy.handList.Count;
            for (int i = 0; i < cardsInHand; i++)
            {
                
                GAMEMANAGER.Instance.deck.Add(enemy.handList[0]);
                enemy.handList.Remove(enemy.handList[0]);

            }

            //Reshuffle Deck Visualy
            //GAMEMANAGER.Instance.deck = GAMEMANAGER.Instance.Shuffle(GAMEMANAGER.Instance.deck);
            int count = 0;
            foreach (Transform playerSlot in GAMEMANAGER.Instance.enemySlots)
            {
                if (count < cardsInHand)
                {
                    GAMEMANAGER.Instance.deck[0].transform.position = enemy.transform.position;
                    GAMEMANAGER.Instance.deck[0].transform.rotation = enemy.transform.rotation;
                    enemy.handList.Add(GAMEMANAGER.Instance.deck[0]);
                    StartCoroutine(GAMEMANAGER.Instance.dissolvingEffect(GAMEMANAGER.Instance.deck[0]));
                    GAMEMANAGER.Instance.deck.Remove(GAMEMANAGER.Instance.deck[0]);
                    count++;

                }


                //Replace Card Teleport With Animation
            }
        }
        
        GAMEMANAGER.Instance.canPlay = true;
    }

    
}
