using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine.Rendering;
//using UnityEditor.PackageManager.Requests;

public enum RoundState { START, PLAYERTURN, CHECKPLAYSTATE, COUNTPOINTS, ENEMYTURN, WON, LOST }

public enum PickupPrio { PLAYER, ENEMY }
public class GAMEMANAGER : MonoBehaviour
{

    [SerializeField] public TMP_Text textMeshPro;
    [SerializeField] public TMP_Text enemyPointText;
    [SerializeField] public TMP_Text round_number_tmp;
    [SerializeField] public TMP_Text playerTokens;
    //[SerializeField] public TMP_Text enemyTokens;

    // Reference to the player and table
    [SerializeField] public PlayerStateManager player;
    [SerializeField] public AIBehaviour enemy;
    [SerializeField] public Table table;
    public int tableTotal = 0;

    private int round_number = 1;
    public static GAMEMANAGER Instance;
    public int playerPoints = 0;
    public int enemyPoints= 0;

    public bool wasExecuted = false;
    public bool hasDrawnEffect = false;
    public bool canPlay = true;
    public bool handWasRedrawnByWOF = false;

    private bool wasPickupOverride = false;
    private bool endRoundChecked = false;
    public bool WasPickupOverride { get => wasPickupOverride; set => wasPickupOverride = value; }

    public PickupPrio currentPrio;

  
    public List<Card> deck = new List<Card>();

    [Header("--- Card Transforms ---")]
    public Transform revealCardsTransform;
    public Transform playerCardsTransform;
    public Transform enemyCardsTransform;
    [SerializeField] Transform playerCollectionDeckTransform;
    [SerializeField] Transform enemyCollectionDeckTransform;

    [Header("--- Slot Transforms ---")]
    public Transform[] playerSlots;
    public Transform[] enemySlots;
    public CardSlot[] cardSlots;

    [Header("--- State ---")]
    public RoundState currentRoundState;


    [Header("--- Effect Tokens ---")]
    public int enemyEffectTokens = 0;
    public int playerEffectTokens = 0;


    [Header("--- Timing ---")]
    [SerializeField, Range(0f, 5f)] float transitionTime;
    [SerializeField, Range(0f, 5f)] float cardsFloatTime;



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

    public void UpdateUI()
    {
        
        
        playerTokens.text = "x" + playerEffectTokens;
        textMeshPro.text = "Player: " + playerPoints;
        enemyPointText.text = "Opponent: " + enemyPoints;
    }

    public void CalculateTableTotal()
    {
        tableTotal = 0;
        foreach(Card card in table.cards)
        {
            tableTotal += card.CardValue;
        }
       
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
            //deck.Add(card);
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
        int currentPlayerPoints = 0;
        int currentEnemyPoints = 0;
        
        foreach(Card card in table.playedCards)
        {
            if(card.Suit == Suit.SUNS)
            {
                playerSuns++;
               
            }

            if(card.Suit == Suit.SUNS && card.CardValue == 7) { currentPlayerPoints++; foundSevenSuns = true; Debug.Log("You got the seven of SUNS"); }
            if(card.CardValue == 7) { playerSevens++;  }
        }
        if(foundSevenSuns ==false)
        {
            Debug.Log("Enemy Had Seven Of Suns");
           currentEnemyPoints++; 
        }
        
        if (table.playedCards.Count >= 21) { currentPlayerPoints++; Debug.Log("You got the highest number of cards" + table.playedCards.Count); } else if(table.playedCards.Count < 20) { currentEnemyPoints++; Debug.Log("Enemy Had " + (40 - table.playedCards.Count) + " Total Cards"); }
        if (playerSuns >= 6) { currentPlayerPoints++; Debug.Log("You got the highest number of suns" + playerSuns); } else if(playerSuns < 5) { currentEnemyPoints++; Debug.Log("Enemy Had " + (10 - playerSuns) + " suns"); }
       
        if(currentPlayerPoints > currentEnemyPoints)
        {
            if(enemyEffectTokens < 2)
            {
                enemyEffectTokens += 2;

            }
            else if(enemyEffectTokens == 2) 
            {
                enemyEffectTokens++;
            }
        }
        else if(currentPlayerPoints < currentEnemyPoints)
        {
            if (playerEffectTokens < 2)
            {
                playerEffectTokens += 2;

            }
            else if (playerEffectTokens == 2)
            {
                playerEffectTokens++;
            }
        }
        else if(currentPlayerPoints == currentEnemyPoints)
        {
            if(enemyEffectTokens <=2)
            {
                 enemyEffectTokens++;

            }
            if(playerEffectTokens <= 2)
            {
                playerEffectTokens++;

            }
        }

        playerPoints += currentPlayerPoints;
        enemyPoints += currentEnemyPoints;

        UpdateUI();

        currentRoundState = RoundState.WON;

    }

    private void CheckPlayerHand()
    {
        if (endRoundChecked == false)
        {

            if (deck.Count != 0)
            {
                if (enemy.handList.Count == 0)
                {
                    AIEffectProbability.hasDrawnThisTurn = false;
                    for (int i = 0; i < playerSlots.Length; i++)
                    {
                        deck[0].gameObject.SetActive(true);
                        StartCoroutine(dissolvingEffect(deck[0]));
                        deck[0].transform.position = playerSlots[i].transform.position;
                        deck[0].transform.rotation = playerSlots[i].transform.rotation;
                        deck[0].inHand = true;
                        player.playerCards.Add(deck[0]);
                        deck.Remove(deck[0]);
                        EventManager.InvokeDrawCards();

                    }
                    for (int i = 0; i < 3; i++)
                    {
                        enemy.handList.Add(deck[0]);
                        deck[0].gameObject.SetActive(true);
                        var outline = deck[0].GetComponent<Outline>();
                        outline.enabled = false;
                        StartCoroutine(dissolvingEffect(deck[0]));
                        deck[0].transform.position = enemySlots[i].transform.position;
                        deck[0].transform.rotation = enemySlots[i].transform.rotation;
                        deck[0].dissolveMaterialBack.SetFloat("_Dissolve_Value", -1f);
                        deck[0].dissolveMaterialFront.SetFloat("_Dissolve_Value", -1f);
                        deck.Remove(deck[0]);
                        EventManager.InvokeDrawCards();
                    }
                }

                currentRoundState = RoundState.PLAYERTURN;
            }
            else if (deck.Count == 0 && player.playerCards.Count > 0 && enemy.handList.Count > 0)
            {
                currentRoundState = RoundState.PLAYERTURN;
            }
            else
            {


                endRoundChecked = true;
                if (currentPrio == PickupPrio.PLAYER)
                {
                    foreach (Card card in table.cards)
                    {
                        table.playedCards.Add(card);
                        //card.transform.position += Vector3.right * 7;
                        Debug.Log("Player Cleaned Up Table");
                        StartCoroutine(MoveRemainingCards(playerCollectionDeckTransform));
                    }

                }
                else
                {
                    foreach (Card card in table.cards)
                    {
                        enemy.collectedCards.Add(card);
                        //card.transform.position += Vector3.right * 7;
                        Debug.Log("Enemy Cleaned Up Table");
                        StartCoroutine(MoveRemainingCards(enemyCollectionDeckTransform));
                    }
                }




            }
        }
       


    }
   
    private void SetUpGame()
    {
        EventManager.InvokeRoundEnd();
        deck = Shuffle(deck);
        SetUpCards(cardSlots, playerSlots);
        EventManager.InvokeReturnCards();
        EventManager.InvokeDrawCards();
        wasPickupOverride = false;
    }

    public List<Card> Shuffle(List<Card> listToShuffle)
    {
        System.Random _rand = new System.Random();

        EventManager.InvokeShuffleDeck();

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
                StartCoroutine(dissolvingEffect(deck[0]));
                player.playerCards.Add(deck[0]);
                deck.Remove(deck[0]);
                
            }

            for(int i=0; i < 3; i++)
            {
                enemy.handList.Add(deck[0]);
                deck[0].gameObject.SetActive(true);
                var outline = deck[0].GetComponent<Outline>();
                outline.enabled = false;
                deck[0].dissolveMaterialBack.SetFloat("_Dissolve_Value", -1f);
                deck[0].dissolveMaterialFront.SetFloat("_Dissolve_Value", -1f);
                
                deck[0].transform.position = enemySlots[i].transform.position;
                deck[0].transform.rotation = enemySlots[i].transform.rotation;
                StartCoroutine(dissolvingEffect(deck[0]));
                deck.Remove(deck[0]);
            }

            // Place four cards in the table
            for (int j = 0; j < 4; j++)
            {
                deck[0].dissolveMaterialBack.SetFloat("_Dissolve_Value",1f);
                deck[0].dissolveMaterialFront.SetFloat("_Dissolve_Value",1f);
                deck[0].gameObject.SetActive(true);
                deck[0].transform.position = cardSlots[j].transform.position;
                deck[0].transform.rotation = cardSlots[j].transform.rotation;
                StartCoroutine(dissolvingEffect(deck[0]));
                cardSlots[j].available = false;
                table.cards.Add(deck[0]);
                
                deck.Remove(deck[0]);
            }

            currentRoundState = RoundState.PLAYERTURN;
        }

    }


    public IEnumerator dissolvingEffect(Card card)
    {

        for (int i = 0; i < 160; i++)
        {
            yield return new WaitForSeconds(1/100000000000f);

            card.dissolveMaterialFront.SetFloat("_Dissolve_Value", -i / 100f + 0.8f);
            card.dissolveMaterialBack.SetFloat("_Dissolve_Value", -i / 100f + 0.8f);
        }

    }

    public IEnumerator burningEffect(Card card)
    {

        for (int i = 0; i < 160; i++)
        {
            yield return new WaitForSeconds(1 / 100000000000f);

            card.dissolveMaterialFront.SetFloat("_Dissolve_Value", i / 100f - 0.8f);
            card.dissolveMaterialBack.SetFloat("_Dissolve_Value", i / 100f  - 0.8f);
        }

    }

    private IEnumerator MoveRemainingCards(Transform collectionDeck)
    {
        float waitTime = 0;
        
        foreach (Card card in table.cards)
        {
            waitTime++;
            float elapsedTime = 0;
            Vector3 initialPosition = card.transform.position;
            Quaternion initialRotation = card.transform.rotation;
            Vector3 cardFloat = card.transform.position + new Vector3(0, 0.1f, 0);
            while (elapsedTime < cardsFloatTime)
            {
                float t = elapsedTime / cardsFloatTime;
                card.transform.position = Vector3.Lerp(initialPosition,
                    cardFloat, t);
                
                elapsedTime += Time.deltaTime;
                yield return new WaitForSeconds(0f);
            }

            //yield return new WaitForSeconds(1);
            elapsedTime = 0;
            

            while (elapsedTime < transitionTime)
            {
                float t = elapsedTime / transitionTime;
                card.transform.position = Vector3.Lerp(card.transform.position, 
                    collectionDeck.position, t);
                card.transform.rotation = Quaternion.Lerp(card.transform.rotation, 
                    collectionDeck.rotation, t);

                elapsedTime += Time.deltaTime;
                yield return null; 
            }

            
        }
            


        yield return new WaitForSeconds(1);
        foreach (Card card in table.cards)
        {
            
            card.transform.position = player.playedCardsTransform.position;
        }
        table.cards.Clear();
        currentRoundState = RoundState.COUNTPOINTS;
        endRoundChecked = false;
        Debug.Log("table cleared");
    }




}
