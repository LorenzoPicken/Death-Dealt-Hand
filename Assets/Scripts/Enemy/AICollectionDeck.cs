using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICollectionDeck : MonoBehaviour
{
    [SerializeField] List<GameObject> cards = new List<GameObject>();

    private void OnEnable()
    {
        EventManager.onEnemyPickUp += PickupCards;
        EventManager.onEnemyLoseCard += LoseCards;
        EventManager.onRoundEnd += ClearPlayerDeck;
    }

    private void OnDisable()
    {
        EventManager.onEnemyPickUp -= PickupCards;
        EventManager.onEnemyLoseCard -= LoseCards;
        EventManager.onRoundEnd -= ClearPlayerDeck;
    }

    private void PickupCards()
    {
        int index = 0;
        foreach (Card card in GAMEMANAGER.Instance.enemy.collectedCards)
        {
            cards[index].SetActive(true);
            index++;
        }
    }

    private void LoseCards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (i >= GAMEMANAGER.Instance.enemy.collectedCards.Count)
            {
                cards[i].SetActive(false);
            }
        }
    }

    private void ClearPlayerDeck()
    {
        foreach (GameObject card in cards)
        {
            card.SetActive(false);
        }
    }
}
