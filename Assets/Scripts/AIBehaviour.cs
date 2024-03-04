using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    public enum ENEMYSTATE{PLAYING, WAITING }

    public ENEMYSTATE currentState;
    private bool knowsHand = false;

    public List<Card> enemyHand = new List<Card>();
    private Card currentCard;
    private int index = 0;
    int handCount = 0;

    [SerializeField] float minWait;
    [SerializeField] float maxWait;


    
    // Update is called once per frame
    void Update()
    {
        if(GAMEMANAGER.Instance.currentRoundState == RoundState.ENEMYTURN)
        {
            switch (currentState)
            {
                case ENEMYSTATE.WAITING:
                    RandomWaitTime();
                    
                    break;

                case ENEMYSTATE.PLAYING:
                    
                    break;


                
            }
        }
    }

    private void RandomWaitTime()
    {
        float waitTime = Random.Range(minWait, maxWait);
        StartCoroutine(WaitRoutine(waitTime));



    }


    private void Play()
    {
        //Check if enemy knows the players hand
        bool skipCalcs = false;
       if(knowsHand) 
       { 
            
       }
       
       
       else
       {
            while(handCount < enemyHand.Count)
            {
                skipCalcs = IsThisCardEqualToCardOnTable();

                //If the current card is equal to any cards on table, skip calculating other possibilities
                if(skipCalcs)
                {

                }
                else
                {

                }
                handCount++;
            }
            
       }

        
    }


    #region Checks if card in hand is equal to any cards on the table
    private bool IsThisCardEqualToCardOnTable()
    {
        
        //foreach(Card card in )
        //{
        //    if(currentCard.CardValue == card.CardValue)
        //    {
        //        return true;
        //    }
            
        //}
        return false;
    }
    #endregion



    private void CalculateRisk(Card currentCard)
    {
        
    }

























    private IEnumerator WaitRoutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        

        currentState = ENEMYSTATE.PLAYING;
        
        
        
    }

    private bool HasSuns()
    {
        return true;
    }
}
