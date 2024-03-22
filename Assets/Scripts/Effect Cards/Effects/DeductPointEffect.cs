using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeductPointEffect
{
    public static void DeductPoint()
    {
        if (GAMEMANAGER.Instance.currentRoundState == RoundState.PLAYERTURN)
        {
            GAMEMANAGER.Instance.enemyPoint--;
        }
        else
        {
            GAMEMANAGER.Instance.playerPoints--;
        }
    }
}
