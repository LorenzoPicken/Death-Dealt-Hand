using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEffectProbability : MonoBehaviour
{
    [SerializeField] Table player;
    [SerializeField] AIBehaviour enemy;
    
    [SerializeField] private PlayerDrawEffect drawEffect;
    public static bool hasDrawnThisTurn = false;
    public bool CheckForDraw()
    {
        int playerCollectionSize = player.playedCards.Count;
        int aiCollectionSize = enemy.collectedCards.Count;
        int randNum = 0;
        if(GAMEMANAGER.Instance.enemyEffectTokens > 0)
        {
            randNum = DrawRNG();
            int difference = playerCollectionSize - aiCollectionSize;
            
            if(difference < 3 && hasDrawnThisTurn == false)
            {
                if(randNum == 1)
                {
                    drawEffect.DrawEffectCard();
                    hasDrawnThisTurn=true;
                    return true;
                }
            }
            else if(difference >=3  && hasDrawnThisTurn == false && GAMEMANAGER.Instance.playerPoints - GAMEMANAGER.Instance.enemyPoints <= 4)
            {
                if(randNum > 0 && randNum < 5)
                {
                    drawEffect.DrawEffectCard();
                    hasDrawnThisTurn=true;
                    return true;
                }
            }
            else if(GAMEMANAGER.Instance.playerPoints - GAMEMANAGER.Instance.enemyPoints > 4)
            {
                if (randNum > 0 && randNum < 6)
                {
                    drawEffect.DrawEffectCard();
                    hasDrawnThisTurn = true;
                    return true;
                }
            }
            return false;
        }
        return false;
    }

    private int DrawRNG()
    {
        int randomNum = Random.Range(1, 9);
        return randomNum;
    }
}
