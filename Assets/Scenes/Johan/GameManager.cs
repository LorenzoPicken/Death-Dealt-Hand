using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEditor;
using System.Security.Cryptography;


    public class GameManager : MonoBehaviour
    {
        public List<Card> deck = new List<Card>();
        public List<Card> player1Hand = new List<Card>();
        public List<Card> player2Hand = new List<Card>();
        public List<Card> tableHand = new List<Card>();

     
        


        public Transform[] cardslotsPlayer1;
        public Transform[] cardslotsPlayer2;
        public Transform[] cardslotsTable;
        public bool[] availableCardSlotsPlayer1;
        public bool[] availableCardSlotsPlayer2;
        public bool[] availableCardSlotsTable;
       


        public static GameManager instance;
        //public event Action OnCardsPlayed;


        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            instance = this;
            
        }

    private void OnEnable()
    {
        SelectionManager.OnCardSelected += MakeTableCardsAvailable;
    }
    private void OnDisable()
    {
        SelectionManager.OnCardSelected -= MakeTableCardsAvailable;

    }

    private void MakeTableCardsAvailable()
    {
        foreach (Card card in tableHand) 
        { 
            card.selectable = true;
        }
    }

    private void MakePlayerCardsAvailable()
    {
        foreach (Card card in player1Hand)
        {
            card.selectable = true;
            Debug.Log(card.cardValue);
        }
    }
    private void Start()
        {
            Shuffle(deck);
            DrawCard(availableCardSlotsPlayer2, cardslotsPlayer2, player2Hand);
            DrawCard(availableCardSlotsPlayer1, cardslotsPlayer1, player1Hand);
            DrawCard(availableCardSlotsTable, cardslotsTable, tableHand);
            MakePlayerCardsAvailable();
        }

        
        // Shuffle list
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

        
        // Draw randomly cards and place in both player hands
        public void DrawCard(bool[] availableSlots, Transform[] cardSlots, List<Card> playerHand)
        {
            if (deck.Count >= 0)
            {
                for (int i = 0; i < availableSlots.Length; i++)
                {
                    if (availableSlots[i] == true)
                    {
                        deck[0].gameObject.SetActive(true);
                        deck[0].transform.position = cardSlots[i].transform.position;
                        availableSlots[i] = false;
                        playerHand.Add(deck[0]);
                        deck.Remove(deck[0]);
                        
                    }

                }
            }
        }

      

    }

