using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuntractPointEffect : EffectCard
{
    
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
        if(GAMEMANAGER.Instance.currentRoundState == RoundState.PLAYERTURN)
        {
            GAMEMANAGER.Instance.enemyPoint--;
        }
        else
        {
            GAMEMANAGER.Instance.playerPoints--;
        }
    }
}
