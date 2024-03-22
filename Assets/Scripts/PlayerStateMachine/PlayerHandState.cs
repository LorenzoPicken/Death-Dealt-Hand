using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandState : PlayerBaseState
{

    
    public override void EnterState(PlayerStateManager player)
    {
       
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (GAMEMANAGER.Instance.currentRoundState == RoundState.PLAYERTURN && GAMEMANAGER.Instance.canPlay == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                player.selectedCard = player.SelectCard();

                

                if (player.selectedCard != null && player.selectedCard.inHand)
                {
                    player.SwitchState(player.TableState);
                }

                
            }


        }
    }
    public override void ExitState(PlayerStateManager player)
    {
        
        player.camSwitch.SwitchToTable();
    }

    
    
   

}
