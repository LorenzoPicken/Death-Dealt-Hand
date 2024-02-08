using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    //Cameras
    [SerializeField] CameraSwitch camSwitch;

    //List of Cards Currently In player's hand
    [SerializeField] Card FOURSABERS;
    [SerializeField] Card ACESUNS;
    [SerializeField] Card TENCLUBS;

    [SerializeField] List<Card> handList = new List<Card>() { };

    //list of cards collected by the player
    List<GameObject> pickedUP = new List<GameObject>() { };

    private GameObject selectedCardGO;

    private int selectedValue = 0;

     private Card selectedCard;

    STATE currentState;

    public int Value { get => selectedValue; set => this.selectedValue = value; }





    // Start is called before the first frame update
    void Start()
    {
        currentState = STATE.HAND;
        handList.Add(FOURSABERS);
        handList.Add(ACESUNS);
        handList.Add (TENCLUBS);
    }

    // Update is called once per frame
    void Update()
    {
        if(GAMEMANAGER.currentTurn == 1)
        {
           
            switch(currentState)
            {
                case STATE.HAND:
                    selectedCard = PlayFromHand();
                    break;

                case STATE.TABLE:
                    PlayFromTable();
                    break;

                case STATE.MOVETOTABLE:
                    Invoke("MoveToTable", 0.5f);
                    break;

                case STATE.MOVETOHAND:
                    camSwitch.SwitchToHand();
                    currentState = STATE.HAND;
                    break;
            }
        }
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

        if (Physics.Raycast(ray = Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
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

    void PlayFromTable()
    {
        Card tableCard = SelectCard();

        if (tableCard)
        {
            Debug.Log("empty");
        }

        if (Input.GetMouseButtonDown(1))
        {
            
            DeselectCardHand();
            currentState = STATE.MOVETOHAND;
        }
    }

    void ShowSelectedCardHand(Card selectedCard)
    {
        selectedCard.transform.localPosition += (Vector3.forward * -0.2f) + (Vector3.up * 0.2f);
    }

    void DeselectCardHand()
    {
        //foreach(Card card in handList)
        //{
        //    if(card.Selected == true)
        //    {
        //        card.transform.localPosition += (Vector3.forward * +0.2f) + (Vector3.up * -0.2f);
        //        card.Selected = false;
        //    }
            
        //}
        //selectedValue = 0;
        //Debug.Log("Selection Cancelled");
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
    MOVETOHAND

}
