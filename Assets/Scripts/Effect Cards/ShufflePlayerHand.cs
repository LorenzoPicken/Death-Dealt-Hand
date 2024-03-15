using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShufflePlayerHand : EffectCard
{
    [SerializeField] PlayerStateManager player;
    [SerializeField] AIBehaviour enemy;
    public override void Draw()
    {
        base.Draw();
    }

    public override void Dispose()
    {
        base.Dispose();

    }

    public override void Execute()
    {
        if (GAMEMANAGER.Instance.currentRoundState == RoundState.PLAYERTURN)
        {
            
        }
        else
        {
            
        }
    }
}
