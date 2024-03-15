using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateManager : MonoBehaviour
{
    // Player variables
    public CameraSwitch camSwitch;
    public Card selectedCard;
    public List<Card> playerCards;
    public Table table;
    public Transform playedCardsTransform;
   

    // UI 
    public Image image;
    
    // Raycast LayerMask
    public LayerMask cards;

    // Finite State Machine configuration 
    public PlayerBaseState currentState;
    public PlayerTableState TableState = new PlayerTableState();
    public PlayerHandState HandState = new PlayerHandState();
    void Start()
    {
        currentState = HandState;
        currentState.EnterState(this);
        image.enabled = false;
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState.ExitState(this);

        currentState = state;

        state.EnterState(this);

    }


    public Card SelectCard()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 15, cards))
        {
            Card selectedCard = hit.transform?.GetComponent<Card>();
            return selectedCard;
        }
        return null;
    }

}
