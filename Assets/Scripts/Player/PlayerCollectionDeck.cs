using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectionDeck : MonoBehaviour
{
    [SerializeField] public List<GameObject> cards = new List<GameObject>();
    
    private void OnEnable()
    {
        EventManager.onPlayerPickUp += PickupCards;
        EventManager.onPlayerLoseCard += LoseCards;
        EventManager.onRoundEnd += ClearPlayerDeck;
    }

    private void OnDisable()
    {
        EventManager.onPlayerPickUp -= PickupCards;
        EventManager.onPlayerLoseCard -= LoseCards;
        EventManager.onRoundEnd -= ClearPlayerDeck;
    }

    private void PickupCards()
    {
        int index = 0;
        foreach(Card card in GAMEMANAGER.Instance.table.playedCards)
        {
            cards[index].SetActive(true);
            index++;
        }
    }

    private void LoseCards()
    {
        for(int i = 0; i < cards.Count; i++)
        {
            if (i >= GAMEMANAGER.Instance.table.playedCards.Count)
            {
                cards[i].SetActive(false);
            }
        }
    }

    private void ClearPlayerDeck()
    {
        foreach(GameObject card in cards)
        {
            card.SetActive(false);
        }
    }







}
