using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public enum RoundState { START, PLAYERTURN, CHECKPLAYSTATE, COUNTPOINTS, ENEMYTURN, WON, LOST }
public class GAMEMANAGER : MonoBehaviour
{

    [SerializeField] public TMP_Text textMeshPro;
    public int tableTotal;
    public static GAMEMANAGER Instance;
    public int playerPoints = 0;
   

    public List<Card> deck = new List<Card>();
    public List<Card> playerHand = new List<Card>();
    public List<Card> playedCards = new List<Card>();
    public List<Card> tableList = new List<Card>();

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

    // Set Cards
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
                CheckPlayerHand();
                break;
            case RoundState.COUNTPOINTS:
                CountPoints();
                break;
            case RoundState.ENEMYTURN:
                break;
            case RoundState.LOST:
                break;
            case RoundState.WON: 
                break;
        }
    }

    private void CountPoints()
    {

        int playerSuns = 0;
        int playerSevens = 0;
        
        foreach(Card card in playedCards)
        {
            if(card.Suit == Suit.SUNS)
            {
                playerSuns++;
                if(card.CardValue == 7) { playerPoints++; }
            }
            if(card.CardValue == 7) { playerSevens++; }
        }
        
        if (playedCards.Count >= 21) { playerPoints++;}
        if (playerSuns >= 6) { playerPoints++; }
        if (playerSevens >= 6) {  playerPoints++; }

        textMeshPro.text = "Points: " + playerPoints;

        currentRoundState = RoundState.WON;

    }

    private void CheckPlayerHand()
    {
        if (deck.Count != 0)
        {
            if (playerHand.Count == 0)
            {
                for (int i = 0; i < playerSlots.Length; i++)
                {
                    deck[0].gameObject.SetActive(true);
                    deck[0].transform.position = playerSlots[i].transform.position;
                    deck[0].transform.rotation = playerSlots[i].transform.rotation;
                    deck[0].inHand = true;
                    playerHand.Add(deck[0]);
                    deck.Remove(deck[0]);

                }
            }
                 currentRoundState = RoundState.PLAYERTURN;
        }
        else
        {
            currentRoundState = RoundState.COUNTPOINTS;
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
            for (int i = 0; i < playerSlots.Length; i++)
            {
                deck[0].gameObject.SetActive(true);
                deck[0].transform.position = playerSlots[i].transform.position;
                deck[0].transform.rotation = playerSlots[i].transform.rotation;
                deck[0].inHand = true;
                playerHand.Add(deck[0]);
                deck.Remove(deck[0]);
                
            }
            // Place four cards in the table
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
