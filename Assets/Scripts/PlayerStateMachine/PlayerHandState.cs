using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandState : PlayerBaseState
{

    
    public override void EnterState(PlayerStateManager player)
    {
        Debug.Log("Enter state Player Hand");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (GAMEMANAGER.Instance.currentRoundState == RoundState.PLAYERTURN)
        {
            if (Input.GetMouseButtonDown(0))
            {
                player.selectedCard = player.SelectCard();

                Debug.Log(player.selectedCard);

                if (player.selectedCard != null && player.selectedCard.inHand)
                {
                    player.SwitchState(player.TableState);
                }
            }

        }
    }
    public override void ExitState(PlayerStateManager player)
    {
        Debug.Log("Exit state Player Hand");
        player.camSwitch.SwitchToTable();
    }

    
    
   

}
