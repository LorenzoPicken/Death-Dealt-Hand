using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RedrawHand : MonoBehaviour
{
    [SerializeField] PlayerStateManager player;
    [SerializeField] AIBehaviour enemy;
    [SerializeField] Transform topOfDeckTransform;

    private List<Card> cards;

    private int cardsInHand;

    [Header("---Timing---")]
    [SerializeField, Range(0f, 5f)] float transitionTime;
    [SerializeField, Range(0f, 10f)] float returnCardsTime;
    //[SerializeField, Range(0f, 10f)] float shuffleAnimationTime;
    
    
    
    public  void Execute()
    {
        cards = new List<Card>() { };
        cardsInHand = 0;
        if (GAMEMANAGER.Instance.currentRoundState == RoundState.PLAYERTURN)
        {
            foreach(Card card in player.playerCards)
            {
                cards.Add(card);
                
                //Visually Send Card Back To Deck
            }
            cardsInHand = player.playerCards.Count;
            for(int i = 0; i < cardsInHand; i++)
            {
                player.playerCards[0].InHand = false;
                GAMEMANAGER.Instance.deck.Add(player.playerCards[0]);
                player.playerCards.Remove(player.playerCards[0]);
                
            }
            StartCoroutine(SendCardsBackToDeck(() =>{GivePlayerNewCards();}));
                
            StartCoroutine(WaitFinishShuffle(() => { EndEffect(); }));



        }
        else
        {
            foreach (Card card in enemy.handList)
            {
                cards.Add(card);
                
                //Visually Send Card Back To Deck
            }
            cardsInHand = enemy.handList.Count;
            for (int i = 0; i < cardsInHand; i++)
            {
                
                GAMEMANAGER.Instance.deck.Add(enemy.handList[0]);
                enemy.handList.Remove(enemy.handList[0]);

            }
            StartCoroutine(SendCardsBackToDeck(() => { GiveAINewCards(); }));
            


            StartCoroutine(WaitFinishShuffle(() => { EndEffect(); }));
        }

        
        
    }




    private void GivePlayerNewCards()
    {
        int count = 0;
        foreach (Transform playerSlot in GAMEMANAGER.Instance.playerSlots)
        {
            if (count < cardsInHand)
            {
                GAMEMANAGER.Instance.deck[0].transform.position = playerSlot.transform.position;
                GAMEMANAGER.Instance.deck[0].transform.rotation = playerSlot.transform.rotation;
                GAMEMANAGER.Instance.deck[0].inHand = true;
                player.playerCards.Add(GAMEMANAGER.Instance.deck[0]);
                StartCoroutine(GAMEMANAGER.Instance.dissolvingEffect(GAMEMANAGER.Instance.deck[0]));
                GAMEMANAGER.Instance.deck.Remove(GAMEMANAGER.Instance.deck[0]);
                count++;

            }

            
        }
    }

    private void GiveAINewCards()
    {
        int count = 0;
        foreach (Transform playerSlot in GAMEMANAGER.Instance.enemySlots)
        {
            if (count < cardsInHand)
            {
                GAMEMANAGER.Instance.deck[0].transform.position = playerSlot.transform.position;
                GAMEMANAGER.Instance.deck[0].transform.rotation = playerSlot.transform.rotation;
                enemy.handList.Add(GAMEMANAGER.Instance.deck[0]);
                StartCoroutine(GAMEMANAGER.Instance.dissolvingEffect(GAMEMANAGER.Instance.deck[0]));
                GAMEMANAGER.Instance.deck.Remove(GAMEMANAGER.Instance.deck[0]);
                count++;

            }


            
        }
    }

    private void EndEffect()
    {
        GAMEMANAGER.Instance.canPlay = true;
        return;
    }


    #region Coroutines

    private IEnumerator SendCardsBackToDeck(System.Action onSendCardsBack)
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

        yield return new WaitForSeconds(returnCardsTime); 
        onSendCardsBack?.Invoke(); // Invoke the callback
    }

    private IEnumerator WaitFinishShuffle(System.Action onReplenishHand)
    {
        yield return null;
        onReplenishHand?.Invoke();


    }

    

    #endregion


}
