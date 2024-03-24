using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeductPointEffect
{
    public static void DeductPoint()
    {
        if (GAMEMANAGER.Instance.currentRoundState == RoundState.PLAYERTURN && GAMEMANAGER.Instance.enemyPoint > 0)
        {
            GAMEMANAGER.Instance.enemyPoint--;
        }
        else if(GAMEMANAGER.Instance.currentRoundState == RoundState.ENEMYTURN && GAMEMANAGER.Instance.playerPoints > 0)
        {
                GAMEMANAGER.Instance.playerPoints--;

        }
        GAMEMANAGER.Instance.canPlay = true;
    }
}
