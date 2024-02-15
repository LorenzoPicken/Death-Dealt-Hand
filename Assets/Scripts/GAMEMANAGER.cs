using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoundState { START, PLAYERTURN, CHECKPLAYSTATE, WON, LOST }
public class GAMEMANAGER : MonoBehaviour
{
    public static GAMEMANAGER Instance;
   
    public List<Card> deck = new List<Card>();
    public List<Card> playerHand = new List<Card>();
    public List<Card> playedCards = new List<Card>();

    public Transform[] playerSlots;
    public CardSlot[] cardSlots;

    public RoundState currentRoundState;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
      currentRoundState = RoundState.START;
    }

    private void Update()
    {
        switch(currentRoundState)
        {
            case RoundState.START:
                SetUpGame();
                break; 
            case RoundState.PLAYERTURN:
                break;               
            case RoundState.CHECKPLAYSTATE:
                
                break; 
            case RoundState.LOST:
                break;
            case RoundState.WON: 
                break;
        }
    }

    private void SetUpGame()
    {
        deck = Shuffle(deck);
        SetUpCards(cardSlots, playerSlots);
    }

    public List<Card> Shuffle(List<Card> listToShuffle)
    {
        System.Random _rand = new System.Random();

        for (int i = listToShuffle.Count - 1; i > 0; i--)
        {
            var k = _rand.Next(i + 1);
            var value = listToShuffle[k];
            listToShuffle[k] = listToShuffle[i];
            listToShuffle[i] = value;
        }
        return listToShuffle;
    }
    public void SetUpCards(CardSlot[] cardSlots, Transform[] playerSlots) 
    {

        if (deck.Count >= 0)
        {
            for (int i = 0; i < 3; i++)
            {
                deck[0].gameObject.SetActive(true);
                deck[0].transform.position = playerSlots[i].transform.position;
                deck[0].transform.rotation = playerSlots[i].transform.rotation;
                deck[0].inHand = true;
                playerHand.Add(deck[0]);
                deck.Remove(deck[0]);
                
            }
            for (int j = 0; j < 4; j++)
            {
                deck[0].gameObject.SetActive(true);
                deck[0].transform.position = cardSlots[j].transform.position;
                cardSlots[j].available = false;
                deck.Remove(deck[0]);
            }

            currentRoundState = RoundState.PLAYERTURN;
        }

    }

}
