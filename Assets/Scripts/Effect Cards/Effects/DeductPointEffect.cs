using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeductPointEffect : EffectCard
{
    public override void Execute()
    {
        if (GAMEMANAGER.Instance.currentRoundState == RoundState.PLAYERTURN && GAMEMANAGER.Instance.enemyPoints > 0)
        {
            GAMEMANAGER.Instance.enemyPoints--;
        }
        else if(GAMEMANAGER.Instance.currentRoundState == RoundState.ENEMYTURN && GAMEMANAGER.Instance.playerPoints > 0)
        {
                GAMEMANAGER.Instance.playerPoints--;

        }
        GAMEMANAGER.Instance.canPlay = true;
    }
}
