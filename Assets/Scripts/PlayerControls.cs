using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    //Cameras
    [SerializeField] CameraSwitch camSwitch;

    //List of Cards Currently In player's hand
    [SerializeField] List<GameObject> handList = new List<GameObject>() { };

    //list of cards collected by the player
    List<GameObject> pickedUP = new List<GameObject>() { };

    private GameObject selectedCardGO;

    private int selectedValue = 0;


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
        if(GAMEMANAGER.currentTurn == 1)
        {
           
            switch(currentState)
            {
                case STATE.HAND:
                    PlayFromHand();
                    break;

                case STATE.TABLE:
                    PlayFromTable();
                    break;

                case STATE.MOVETOTABLE:
                    camSwitch.SwitchToTable();
                    currentState = STATE.TABLE;
                    break;

                case STATE.MOVETOHAND:
                    camSwitch.SwitchToHand();
                    currentState = STATE.HAND;
                    break;
            }
        }
    }

    void PlayFromHand()
    {
        if (Input.GetMouseButtonDown(0))
        {
            

            Vector3 mousePosition = Input.mousePosition;


            Ray ray = Camera.main.ScreenPointToRay(mousePosition);


            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                
                

                Card selectedCard = hit.transform?.GetComponent<Card>();

                if(selectedCard.InHand == true) 
                {
                    selectedValue = selectedCard.CardValue;
                    selectedCard.Selected = true;
                    ShowSelectedCardHand();
                    currentState = STATE.MOVETOTABLE;
                }

                
            }
        }



    }


    void PlayFromTable()
    {
        if (Input.GetMouseButtonDown(1))
        {
            selectedValue = 0;
            currentState = STATE.MOVETOHAND;
        }
    }

    void ShowSelectedCardHand()
    {
        foreach (GameObject selectedCard in handList)
        {
            Card card = selectedCard.GetComponent<Card>();
            if(card.Selected == true)
            {
                Vector3 cardPosition = selectedCard.transform.localPosition;
                cardPosition.z += 0.2f;
            }
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
