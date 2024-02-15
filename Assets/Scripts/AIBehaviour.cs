using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehaviour : MonoBehaviour
{
    public enum ENEMYSTATE{PLAYING, WAITING }

    public ENEMYSTATE currentState;
    private bool knowsHand = false;

    public List<Card> enemyHand = new List<Card>();

    [SerializeField] float minWait;
    [SerializeField] float maxWait;


    
    // Update is called once per frame
    void Update()
    {
        if (GAMEMANAGER.Instance.currentRoundState == RoundState.ENEMYTURN)
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
       //If deckList.Count == 0 then knowshand == true

        
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
