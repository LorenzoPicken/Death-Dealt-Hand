using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class RedrawHand : MonoBehaviour
{
    [SerializeField] PlayerStateManager player;
    [SerializeField] AIBehaviour enemy;
    [SerializeField] Transform topOfDeckTransform;
    [SerializeField] Material frontMaterial;
    [SerializeField] Material backMaterial;
    

    private List<Card> cards;

    private int cardsInHand;

    public bool handWasRedrawn;
    
   

    [Header("---Timing---")]
    [SerializeField, Range(0f, 5f)] float transitionTime;
    [SerializeField, Range(0f, 10f)] float returnCardsTime;
    [SerializeField, Range (0f, 10f)] float respawnCardDelay;
   
    
    
    
    public void Execute()
    {
        if(GAMEMANAGER.Instance.deck.Count > 0)
        {

            cards = new List<Card>() { };
            cardsInHand = 0;
            if (GAMEMANAGER.Instance.currentRoundState == RoundState.PLAYERTURN)
            {
                foreach(Card card in player.playerCards)
                {

                    cards.Add(card);

                }
                cardsInHand = player.playerCards.Count;
                for (int i = cardsInHand - 1; i >= 0; i--)
                {
                    player.playerCards[i].InHand = false;
                    GAMEMANAGER.Instance.deck.Add(player.playerCards[i]);
                    player.playerCards.RemoveAt(i);
                }
                
                GivePlayerNewCards();

                StartCoroutine(SendCardsBackToDeck());
                EndEffect();



            }
            else
            {
                foreach (Card card in enemy.handList)
                {
                    cards.Add(card);                                      
                }
                cardsInHand = enemy.handList.Count;
                for (int i = cardsInHand - 1; i >= 0; i--)
                {
                    GAMEMANAGER.Instance.deck.Add(enemy.handList[i]);
                    enemy.handList.RemoveAt(i);
                }
                
                GiveAINewCards();

                StartCoroutine(SendCardsBackToDeck());
                EndEffect();

                
            }
        }
            GAMEMANAGER.Instance.canPlay = true;
    }




    private void GivePlayerNewCards()
    {
        int count = 0;
        foreach (Transform playerSlot in GAMEMANAGER.Instance.playerSlots)
        {
            if (count < cardsInHand)
            {
               
                GAMEMANAGER.Instance.deck[0].inHand = true;
                player.playerCards.Add(GAMEMANAGER.Instance.deck[0]);
                GAMEMANAGER.Instance.deck.Remove(GAMEMANAGER.Instance.deck[0]);
                count++;

            }

            
        }
        Invoke("SpawnPlayerCards", respawnCardDelay);
    }

    private void GiveAINewCards()
    {
        int count = 0;
        foreach (Transform playerSlot in GAMEMANAGER.Instance.enemySlots)
        {
            if (count < cardsInHand)
            {
                
                enemy.handList.Add(GAMEMANAGER.Instance.deck[0]);
                GAMEMANAGER.Instance.deck.Remove(GAMEMANAGER.Instance.deck[0]);
                count++;

            }


            
        }
        Invoke("SpawnEnemyCards", respawnCardDelay);




    }

    private void SpawnPlayerCards()
    {
        int index = 0;
        foreach(Transform playerSlot in GAMEMANAGER.Instance.playerSlots)
        {
            if(index < cardsInHand)
            {
                player.playerCards[index].transform.position = playerSlot.transform.position;
                player.playerCards[index].transform.rotation = playerSlot.transform.rotation;
                StartCoroutine(GAMEMANAGER.Instance.dissolvingEffect(player.playerCards[index]));
                index++;

            }
        }
        
    }

    private void SpawnEnemyCards()
    {
        int index = 0;
        foreach (Transform playerSlot in GAMEMANAGER.Instance.enemySlots)
        {
            if (index < cardsInHand)
            {
                enemy.handList[index].transform.position = playerSlot.transform.position;
                enemy.handList[index].transform.rotation = playerSlot.transform.rotation;
                StartCoroutine(GAMEMANAGER.Instance.dissolvingEffect(enemy.handList[index]));
                index++;

            }
        }
        
    }

    private void EndEffect()
    {
        GAMEMANAGER.Instance.canPlay = true;
        handWasRedrawn = true;
       
        
        return;
    }


    #region Coroutines

    private IEnumerator SendCardsBackToDeck()
    {
        
        int count = 0;
        foreach (Card card in cards)
        {
            Vector3 initialPosition;
            Quaternion initialRotation;
            if (GAMEMANAGER.Instance.currentRoundState == RoundState.PLAYERTURN)
            {
                initialPosition = GAMEMANAGER.Instance.playerSlots[count].transform.position;
                initialRotation = GAMEMANAGER.Instance.playerSlots[count].transform.rotation;
                

            }
            else
            {
                initialPosition = GAMEMANAGER.Instance.enemySlots[count].transform.position;
                initialRotation = GAMEMANAGER.Instance.enemySlots[count].transform.rotation;
                
            }
            count++;
            float elapsedTime = 0f;

            while (elapsedTime < transitionTime)
            {
                // Move and rotate towards the reveal position and rotation
                card.transform.position = Vector3.Lerp(initialPosition, topOfDeckTransform.position, elapsedTime / transitionTime);
                card.transform.rotation = Quaternion.Lerp(initialRotation, topOfDeckTransform.rotation, elapsedTime / transitionTime);

                elapsedTime += Time.deltaTime;
                yield return new WaitForSeconds(0);
                
            }

            // Ensure card is positioned at the destination after the loop completes
            card.transform.position = topOfDeckTransform.position;
            card.transform.rotation = topOfDeckTransform.rotation;
            
        }
        
        EventManager.InvokeShuffleDeck();
        foreach(Card card in cards)
        {
            card.transform.position = GAMEMANAGER.Instance.player.playedCardsTransform.position;
        }



        
        
    }

    

    

    #endregion


}
