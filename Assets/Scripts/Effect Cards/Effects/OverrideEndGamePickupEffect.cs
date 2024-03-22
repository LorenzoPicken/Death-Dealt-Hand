using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class OverrideEndGamePickupEffect
{
    public static void OverridePickUp()
    {
        GAMEMANAGER.Instance.WasPickupOverride = true;

        if (GAMEMANAGER.Instance.currentRoundState == RoundState.PLAYERTURN)
        {
            GAMEMANAGER.Instance.currentPrio = PickupPrio.PLAYER;
            
        }
        else
        {
            GAMEMANAGER.Instance.currentPrio = PickupPrio.ENEMY;
            
        }
    }
}