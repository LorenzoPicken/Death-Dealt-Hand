using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PlayerControls : MonoBehaviour
{
    int me = 350;
    //Cameras
    [SerializeField] CameraSwitch camSwitch;
    [SerializeField] Transform playedCards;

    public LayerMask cards;
    //list of cards collected by the player
    List<GameObject> pickedUP = new List<GameObject>() { };

    private GameObject selectedCardGO;

    private int selectedValue = 0;

    private Card selectedCard;
    private Card tableCard;
    private Card secondTableCard;
    
    

    STATE currentState;

    public int Value { get => selectedValue; set => this.selectedValue = value; }





    // Start is called before the first frame update
    void Start()
    {
        currentState = STATE.HAND;
      
    }

    // Update is called once per frame
    void Update()
    {
        if(GAMEMANAGER.Instance.currentRoundState == RoundState.PLAYERTURN)
        {
           
            switch(currentState)
            {
                case STATE.HAND:
                    if (GAMEMANAGER.Instance.playerHand.Count == 0)
                    {
                        GAMEMANAGER.Instance.currentRoundState = RoundState.CHECKPLAYSTATE;
                    }
                    selectedCard = PlayFromHand();
                    break;

                case STATE.TABLE:
                    tableCard = PlayFromTable();
                    CheckCard(selectedCard, tableCard);
                    break;

                case STATE.SECONDCARD:
                    secondTableCard = ChooseSecondCard();
                    CompareCard(selectedCard, tableCard, secondTableCard);
                    break;

                case STATE.MOVETOTABLE:
                    Invoke("MoveToTable", 0.3f);
                    break;

                case STATE.MOVETOHAND:
                    camSwitch.SwitchToHand();
                    currentState = STATE.HAND;
                    break;
                
            }
        }
    }

    private void CompareCard(Card selectedCard, Card tableCard, Card secondTableCard)
    {
        if(selectedCard != null && tableCard != null && secondTableCard != null)
        {
            int sumOfCards = tableCard.CardValue + secondTableCard.CardValue;

            if(sumOfCards > selectedCard.CardValue)
            {
                Debug.Log("The sum of Cards is greater than the selected card");
                StartCoroutine(transformPositionDown(tableCard.transform));
                StartCoroutine(transformPositionDown(secondTableCard.transform));
                currentState = STATE.TABLE;               
            }
            if (sumOfCards < selectedCard.CardValue)
            {
                Debug.Log("The sum of Cards is less than the selected card");
                StartCoroutine(transformPositionDown(tableCard.transform));
                StartCoroutine(transformPositionDown(secondTableCard.transform));
                currentState = STATE.TABLE;
            }
            if (sumOfCards == selectedCard.CardValue)
            {
                Debug.Log("Correct, the sum of cards is equal to the selected card");
                StartCoroutine(transformPositionDown(tableCard.transform));
                StartCoroutine(transformPositionDown(secondTableCard.transform));
                //MovePlayedCards(selectedCard, tableCard, secondTableCard);
                DeselectCardHand();
                currentState = STATE.MOVETOHAND;
            }
        }
    }

    private Card ChooseSecondCard()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Card selectableCard = SelectCard();
            if (selectableCard != null && selectableCard != tableCard)
            {
                StartCoroutine(transformPositionUp(selectableCard.transform));
                Debug.Log("The second card selected is: " + selectableCard.CardValue + " of " + selectableCard.Suit);
                return selectableCard;
            }
            else
            {
                Debug.Log(" Second card selection unsuccesful");
            }
        }
        return null;       
    }

    Card PlayFromHand()
    {
        if (Input.GetMouseButtonDown(0))
        {
            selectedCard = SelectCard();
            

            if (selectedCard.InHand == true && selectedCard != null)
            {
                selectedValue = selectedCard.CardValue;
                selectedCard.Selected = true;
                ShowSelectedCardHand(selectedCard);
                Debug.Log("You Have Decided To Play The " + selectedValue + " Of " + selectedCard.Suit);
                currentState = STATE.MOVETOTABLE;
                return selectedCard;
            }
            
        }
        return null;
    }
    Card SelectCard()
    {
        Ray ray;

        if (Physics.Raycast(ray = Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 15, cards))
        {
            Card selectedCard = hit.transform?.GetComponent<Card>();
            return selectedCard;
            
        }
        return null;
    }
    void MoveToTable()
    {
        camSwitch.SwitchToTable();
        currentState = STATE.TABLE;
    }

    // ============================================================== //
    private IEnumerator transformPositionUp(Transform transform)
    {
        transform.position += Vector3.up * 0.1f;
        Debug.Log("transforming up");
        yield return new WaitForSeconds(1f);
    }
    private IEnumerator transformPositionDown(Transform transform)
    {
        yield return new WaitForSeconds(1f);
        transform.position -= Vector3.up * 0.1f;
        Debug.Log("transforming down");

    }

    // ================================================================ //
    
    Card PlayFromTable()
    {
        
        if(Input.GetMouseButton(0))
        {
            Ray ray;

            if (Physics.Raycast(ray = Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo))
            {
                if (hitInfo.collider.tag == "cardslots")
                {
                    var cardslot = hitInfo.collider.GetComponent<CardSlot>();

                    if (cardslot != null && cardslot.available == true)
                    {
                        selectedCard.transform.position = cardslot.transform.position;
                        selectedCard.transform.rotation = cardslot.transform.rotation;
                        GAMEMANAGER.Instance.playerHand.Remove(selectedCard);
                        currentState = STATE.MOVETOHAND;
                    }
                }
            }
            
            tableCard = SelectCard();
            if(tableCard != null)
            {

                StartCoroutine(transformPositionUp(tableCard.transform));
                Debug.Log("Your first table card is: " + tableCard.CardValue + " of " + tableCard.Suit);
                return tableCard;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            DeselectCardHand();
            currentState = STATE.MOVETOHAND;
            return null;
        }

        return null;
    }

    void CheckCard(Card selectedCard, Card tableCard)
    {
        if(selectedCard != null && tableCard!= null) 
        {
            if(selectedCard.CardValue > tableCard.CardValue)
            {
                currentState = STATE.SECONDCARD;
            }
            if(selectedCard.CardValue == tableCard.CardValue)
            {
                Debug.Log("You got it");
                StartCoroutine(transformPositionDown(tableCard.transform));
                MovePlayedCards(selectedCard, tableCard);
                currentState = STATE.MOVETOHAND;
            }
            if(selectedCard.CardValue < tableCard.CardValue)
            {
                StartCoroutine(transformPositionDown(tableCard.transform));
                
                Debug.Log("it is minor"); 
                currentState = STATE.MOVETOTABLE;
            }
        }
    }

    private void MovePlayedCards(Card card1, Card card2)
    {
        GAMEMANAGER.Instance.playedCards.Add(card1);
        GAMEMANAGER.Instance.playedCards.Add(card2);
        card1.transform.position = playedCards.transform.position;
        card1.transform.rotation = playedCards.transform.rotation;
        card2.transform.position = playedCards.transform.position;
        card2.transform.rotation = playedCards.transform.rotation;
    }

    void ShowSelectedCardHand(Card selectedCard)
    {
        selectedCard.transform.localPosition += (Vector3.forward * -0.2f) + (Vector3.up * 0.2f);
    }

    void DeselectCardHand()
    {
        if(selectedCard != null) 
        { 
            selectedCard.transform.localPosition += (Vector3.forward * +0.2f) + (Vector3.up * -0.2f);
            selectedCard.Selected = false;
            selectedCard = null;
        }
    }


}


public enum STATE
{
    HAND,
    TABLE,
    MOVETOTABLE,
    MOVETOHAND,
    SECONDCARD,
    CHANGESTATE
}
