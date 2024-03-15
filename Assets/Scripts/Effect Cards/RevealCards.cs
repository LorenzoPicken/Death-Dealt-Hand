using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RevealCards : EffectCard
{
    //THIS EFFECT WILL NOT BE ABLE TO BE DRAWN BY THE AI DUE TO A LACK OF TIME PROGRAMMING THE AI TO EXECUTE THIS
    private Transform initialTransform;
    [SerializeField, Range(0, 10)] private float transitionTime = 2f;
    [SerializeField, Range(0, 10)] private float displayTime = 3f;

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
            initialTransform = GAMEMANAGER.Instance.enemyCardsTransform;

            GAMEMANAGER.Instance.enemyCardsTransform.position = Vector3.MoveTowards(GAMEMANAGER.Instance.enemyCardsTransform.position, 
                GAMEMANAGER.Instance.revealCardsTransform.position, 
                Time.deltaTime * transitionTime);


            GAMEMANAGER.Instance.enemyCardsTransform.rotation = Quaternion.RotateTowards(GAMEMANAGER.Instance.enemyCardsTransform.rotation, 
                GAMEMANAGER.Instance.revealCardsTransform.rotation, 
                Time.deltaTime * transitionTime);

            StartCoroutine(ReturnCards(GAMEMANAGER.Instance.enemyCardsTransform, initialTransform));


        }
    }

    private IEnumerator ReturnCards(Transform currentPosition , Transform returnPosition)
    {
        yield return new WaitForSeconds(displayTime);
        
        GAMEMANAGER.Instance.enemyCardsTransform.position = Vector3.MoveTowards(currentPosition.position,
                returnPosition.position,
                Time.deltaTime * transitionTime);


        GAMEMANAGER.Instance.enemyCardsTransform.rotation = Quaternion.RotateTowards(currentPosition.rotation,
            returnPosition.rotation,
            Time.deltaTime * transitionTime);
    

    }
}
