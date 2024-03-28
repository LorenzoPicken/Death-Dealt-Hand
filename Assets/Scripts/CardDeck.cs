using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] List<GameObject> cards = new List<GameObject>();
    [SerializeField] PlayerCollectionDeck playerDeck;
    [SerializeField] AICollectionDeck enemyDeck;

    [Header("transitionTime")]
    [SerializeField, Range(0f, 5f)] float transitionTime;

    private void OnEnable()
    {
        EventManager.onShuffleDeck += ShuffleAnimation;
        EventManager.onDrawCards += DeckEmptyCards;
        EventManager.onReturnCards += DeckAddCards;
        EventManager.onFinalCardsDistributed += RefillDeckFunction;
    }

    private void OnDisable()
    {
        EventManager.onShuffleDeck -= ShuffleAnimation;
        EventManager.onDrawCards -= DeckEmptyCards;
        EventManager.onReturnCards -= DeckAddCards;
        EventManager.onFinalCardsDistributed -= RefillDeckFunction;
    }

    private void ShuffleAnimation()
    {
        animator.SetTrigger("TriggerSuffle");
    }

    private void DeckEmptyCards()
    {
        for(int i =0; i < cards.Count; i++)
        {
            if(i>=GAMEMANAGER.Instance.deck.Count)
            {
                cards[i].SetActive(false);
            }
        }
    }

    private void DeckAddCards()
    {
        int index = 0;
        foreach(Card card in GAMEMANAGER.Instance.deck)
        {
            cards[index].SetActive(true);
            index++;
        }
    }

    private void RefillDeckFunction()
    {
        StartCoroutine(RefillDeck());
    }

    private IEnumerator RefillDeck()
    {
        int count = 0;
        for(int i = 0; i < cards.Count / 2; i++)
        {
            if(i < playerDeck.cards.Count)
            {
                GameObject card = playerDeck.cards[i];
                Transform initialPosition = playerDeck.cards[i].transform;
                Transform finalPosition = cards[count].transform;
                StartCoroutine(MoveCard(card, initialPosition, finalPosition));
                yield return new WaitForSeconds(0.1f);
                count++;
            }
            if(i < enemyDeck.cards.Count)
            {
                GameObject card = enemyDeck.cards[i];
                Transform initialPosition = enemyDeck.cards[i].transform;
                Transform finalPosition = cards[count].transform;
                
                StartCoroutine(MoveCard(card, initialPosition, finalPosition));
                yield return new WaitForSeconds(0.1f);
                count++;
            }
            if(i >= enemyDeck.cards.Count && i>= playerDeck.cards.Count)
            {
                break;
            }

           
            Debug.Log(i);
        }
        
        yield return new WaitForSeconds(1);

    }

    private IEnumerator MoveCard(GameObject card, Transform initialPosition, Transform finalPosition)
    {
        float elapsedTime = 0;
        while(elapsedTime < transitionTime)
        {
            card.transform.position = Vector3.Lerp(initialPosition.position, 
                finalPosition.position, 
                elapsedTime/ transitionTime);


            card.transform.rotation = Quaternion.Lerp(initialPosition.rotation, 
                finalPosition.rotation, 
                elapsedTime/ transitionTime);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        card.transform.position = finalPosition.position;
        card.transform.rotation = finalPosition.rotation;
    }
    
}
