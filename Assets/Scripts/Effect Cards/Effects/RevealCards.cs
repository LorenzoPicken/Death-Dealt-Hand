
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RevealCards : MonoBehaviour
{
    //THIS EFFECT WILL NOT BE ABLE TO BE DRAWN BY THE AI DUE TO A LACK OF TIME PROGRAMMING THE AI TO EXECUTE THIS
    private Transform initialTransform;
    [SerializeField, Range(0, 10)] private float transitionTime = 2f;
    [SerializeField, Range(0, 10)] private float displayTime = 3f;
    //[SerializeField, Range(0, 10)] private float cantPlayTime;
    [SerializeField] AIBehaviour enemy;
    private event Action onComplete;

    private int count = 0;

    public void Execute()
    {
        //float timer = 0;
        if(GAMEMANAGER.Instance.currentRoundState == RoundState.PLAYERTURN)
        {
            StartCoroutine(DisplayCards(() => {
                GAMEMANAGER.Instance.canPlay = true;
            }));


        }
    }

    private IEnumerator DisplayCards(Action onComplete)
    {
        int slotIndex = 0;
        count = 0;
        
        foreach (Card card in enemy.handList)
        {
            float elapsedTime = 0f;

            // Calculate the initial position and rotation
            Vector3 initialPosition = card.transform.position;
            Quaternion initialRotation = card.transform.rotation;

            while (elapsedTime < transitionTime)
            {
                // Move and rotate towards the reveal position and rotation
                card.transform.position = Vector3.Lerp(initialPosition, GAMEMANAGER.Instance.revealCardsTransform.position, elapsedTime / transitionTime);
                card.transform.rotation = Quaternion.Lerp(initialRotation, GAMEMANAGER.Instance.revealCardsTransform.rotation, elapsedTime / transitionTime);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Set the final position and rotation
            card.transform.position = GAMEMANAGER.Instance.revealCardsTransform.position;
            card.transform.rotation = GAMEMANAGER.Instance.revealCardsTransform.rotation;

            // Wait for display time
            yield return new WaitForSeconds(displayTime);

            // Return the card to its original position and rotation
            StartCoroutine(ReturnCards(card.transform, GAMEMANAGER.Instance.enemySlots[slotIndex].transform));

            slotIndex++;
            count++;
            if (count == enemy.handList.Count)
            {

                onComplete?.Invoke();
            }

        }
    }

    private IEnumerator ReturnCards(Transform currentPosition, Transform returnPosition)
    {
        
        float elapsedTime = 0f;

        while (elapsedTime < transitionTime)
        {
            // Move and rotate towards the return position and rotation
            currentPosition.position = Vector3.Lerp(currentPosition.position, returnPosition.position, elapsedTime / transitionTime);
            currentPosition.rotation = Quaternion.Lerp(currentPosition.rotation, returnPosition.rotation, elapsedTime / transitionTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set the final position and rotation
        currentPosition.position = returnPosition.position;
        currentPosition.rotation = returnPosition.rotation;
        

        
    }
}
