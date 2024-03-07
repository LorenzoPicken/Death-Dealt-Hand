using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEditor.PackageManager.Requests;

public enum RoundState { START, PLAYERTURN, CHECKPLAYSTATE, COUNTPOINTS, ENEMYTURN, WON, LOST }
public class GAMEMANAGER : MonoBehaviour
{

    [SerializeField] public TMP_Text textMeshPro;
    [SerializeField] public TMP_Text enemyPoints;
    [SerializeField] public TMP_Text round_number_tmp;
    
    // Reference to the player and table
    [SerializeField] public PlayerStateManager player;
    [SerializeField] public AIBehaviour enemy;
    [SerializeField] public Table table;
    private int tableTotal = 0;

    private int round_number = 1;
    public static GAMEMANAGER Instance;
    public int playerPoints = 0;
    public int enemyPoint= 0;

    public bool wasExecuted = false;
   
   
    
    public List<Card> deck = new List<Card>();
    
    

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
      round_number_tmp.text = "Round: " + round_number.ToString();
    }

    private void Update()
    {
        
        switch (currentRoundState)
        {
            case RoundState.START:
                SetUpGame();
                break; 
            case RoundState.PLAYERTURN:
                wasExecuted = false;

                break;               
            case RoundState.CHECKPLAYSTATE:
                
                CheckPlayerHand();
                break;
            case RoundState.COUNTPOINTS:
                CountPoints();
                break;
            case RoundState.ENEMYTURN:
                if(wasExecuted == false)
                {
                    wasExecuted = true;
                    CalculateTableTotal();
                    enemy.CountDown();
                    
                }
                
                break;
            case RoundState.LOST:
                Application.Quit();
                break;
            case RoundState.WON:
                ResetLists();
                round_number++;
                round_number_tmp.text = "Round: " + round_number.ToString();
                break;
        }
    }

    private void CalculateTableTotal()
    {
        tableTotal = 0;
        foreach(Card card in table.cards)
        {
            tableTotal += card.CardValue;
        }
        Debug.Log("tabelTotal is " + tableTotal);
    }
    private void ResetLists()
    {
        List<Card> cardsToRemoveFromPlayed = new List<Card>();
        foreach (Card card in table.playedCards)
        {
            deck.Add(card);
            cardsToRemoveFromPlayed.Add(card);
        }

        foreach (Card card in cardsToRemoveFromPlayed)
        {
            table.playedCards.Remove(card);
        }

        List<Card> cardsToRemoveFromCollected = new List<Card>();
        foreach (Card card in enemy.collectedCards)
        {
            deck.Add(card);
            cardsToRemoveFromCollected.Add(card);
        }

        foreach (Card card in cardsToRemoveFromCollected)
        {
            enemy.collectedCards.Remove(card);
        }

        List<Card> cardsToRemoveFromTable = new List<Card>();
        foreach (Card card in table.cards)
        {
            card.transform.position += Vector3.right * 7;
            deck.Add(card);
            cardsToRemoveFromTable.Add(card);
        }

        foreach (Card card in cardsToRemoveFromTable)
        {
            table.cards.Remove(card);
        }

        currentRoundState = RoundState.START;
    }

    private void CountPoints()
    {

        int playerSuns = 0;
        int playerSevens = 0;
        bool foundSevenSuns = false;
        
        foreach(Card card in table.playedCards)
        {
            if(card.Suit == Suit.SUNS)
            {
                playerSuns++;
               
            }

            if(card.Suit == Suit.SUNS && card.CardValue == 7) { playerPoints++; foundSevenSuns = true; Debug.Log("You got the seven of SUNS"); }
            if(card.CardValue == 7) { playerSevens++;  }
        }
        if(foundSevenSuns ==false)
        {
           enemyPoint++; 
        }
        
        if (table.playedCards.Count >= 21) { playerPoints++; Debug.Log("You got the highest number of cards" + table.playedCards.Count); } else if(table.playedCards.Count < 20) { enemyPoint++; }
        if (playerSuns >= 6) { playerPoints++; Debug.Log("You got the highest number of suns" + playerSuns); } else if(playerSuns < 5) { enemyPoint++; }
        if (playerSevens >= 3) {  playerPoints++; Debug.Log("You got the highest number of sevens" + playerSevens); } else if( playerSevens < 2){ enemyPoint++; }

        textMeshPro.text = "Player: " + playerPoints;
        enemyPoints.text = "Opponent: " + enemyPoint;

        currentRoundState = RoundState.WON;

    }

    private void CheckPlayerHand()
    {
        if (deck.Count != 0)
        {
            if (enemy.handList.Count == 0)
            {
                for (int i = 0; i < playerSlots.Length; i++)
                {
                    deck[0].gameObject.SetActive(true);
                    deck[0].transform.position = playerSlots[i].transform.position;
                    deck[0].transform.rotation = playerSlots[i].transform.rotation;
                    deck[0].inHand = true;
                    player.playerCards.Add(deck[0]);
                    deck.Remove(deck[0]);

                }
                for(int i =0; i <3; i++)
                {
                    enemy.handList.Add(deck[0]);
                    deck.Remove(deck[0]);
                }
            }
            
            currentRoundState = RoundState.PLAYERTURN;
        }
        else if(deck.Count == 0 && player.playerCards.Count > 0 && enemy.handList.Count > 0)
        {
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
                player.playerCards.Add(deck[0]);
                deck.Remove(deck[0]);
                
            }

            for(int i =0; i < 3; i++)
            {
                enemy.handList.Add(deck[0]);
                deck.Remove(deck[0]);
            }

            // Place four cards in the table
            for (int j = 0; j < 4; j++)
            {
                deck[0].gameObject.SetActive(true);
                deck[0].transform.position = cardSlots[j].transform.position;
                deck[0].transform.rotation = cardSlots[j].transform.rotation;
                cardSlots[j].available = false;
                table.cards.Add(deck[0]);
                deck.Remove(deck[0]);
            }

            currentRoundState = RoundState.PLAYERTURN;
        }

    }

}
