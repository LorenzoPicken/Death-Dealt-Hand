using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] List<GameObject> cards = new List<GameObject>();

    private void OnEnable()
    {
        EventManager.onShuffleDeck += ShuffleAnimation;
        EventManager.onDrawCards += DeckEmptyCards;
        EventManager.onReturnCards += DeckAddCards;
    }

    private void OnDisable()
    {
        EventManager.onShuffleDeck -= ShuffleAnimation;
        EventManager.onDrawCards -= DeckEmptyCards;
        EventManager.onReturnCards -= DeckAddCards;
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
}
