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


        public Transform[] cardslots;
        public bool[] availableCardSlots;
        public TMP_Text decksizeText;


        public static GameManager instance;
        public event Action onCardsPlayed;


        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            instance = this;
            
        }


    // Shuffle list
    private  System.Random _rand;
    public void RandomizeGenericListsMethods()
    {
        _rand = new System.Random();
    }

    public List<Card> Shuffle(List<Card> listToShuffle)
    {
        for (int i = listToShuffle.Count - 1; i > 0; i--)
        {
            var k = _rand.Next(i + 1);
            var value = listToShuffle[k];
            listToShuffle[k] = listToShuffle[i];
            listToShuffle[i] = value;
        }
        return listToShuffle;
    }


    public void DrawCard()
        {
            if (deck.Count >= 0)
            {


                for (int i = 0; i < availableCardSlots.Length; i++)
                {
                    Card randCard = deck[UnityEngine.Random.Range(0, deck.Count)];
                    if (availableCardSlots[i] == true)
                    {
                        randCard.gameObject.SetActive(true);
                        randCard.transform.position = cardslots[i].position;
                        availableCardSlots[i] = false;
                        deck.Remove(randCard);
                    // playerDeck.Add(randCard);
               


                    }
                }
            }
        }

        public void OnCardsPlayed()
        {
            onCardsPlayed?.Invoke();
            //Change player turn
            //Find cards and add them to player
        }


        private void Update()
        {
            decksizeText.text = deck.Count.ToString();
        }

    }

