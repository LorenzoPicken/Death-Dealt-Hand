using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverrideEndGamePickupEffect : EffectCard
{
    public override void Execute()
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
        GAMEMANAGER.Instance.canPlay = true;
    }
}