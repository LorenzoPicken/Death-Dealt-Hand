using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public Transform[] cardslots;
    public bool[] availableCardSlots;
    public Text decksizeText;
    public void DrawCard()
    {
        if(deck.Count >= 0)
        {
            Card randCard = deck[Random.Range(0,deck.Count)];
            for(int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i] == true)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.transform.position = cardslots[i].position;
                    availableCardSlots[i] = false;
                    deck.Remove(randCard);
                    return;
                }
            }
        }
    }

    private void Update()
    {
            decksizeText.text = deck.Count.ToString();
    }
}
