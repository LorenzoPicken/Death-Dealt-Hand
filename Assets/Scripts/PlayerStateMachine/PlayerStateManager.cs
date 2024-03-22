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
    [SerializeField] private PlayerDrawEffect playerDrawEffect;
   

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

        if (Input.GetMouseButtonDown(0) && currentState == HandState && GAMEMANAGER.Instance.canPlay==true)
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                
                if (hit.collider.tag == "EffectDeck" && GAMEMANAGER.Instance.hasDrawnEffect == false) 
                {
                    
                    if (GAMEMANAGER.Instance.playerEffectTokens > 0)
                    {
                        GAMEMANAGER.Instance.hasDrawnEffect = true;
                        playerDrawEffect.DrawEffectCard();

                    }
                    else
                    {
                        Debug.Log("Not Enough Tokens");
                    }
                }
            }

        }
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
