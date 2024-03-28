using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;

public class PlayerDrawEffect : MonoBehaviour
{
    [Header("--- Cards ---")]
    [SerializeField] GameObject theTakersGO;
    [SerializeField] GameObject theButcherGO;
    [SerializeField] GameObject wheelOfFortuneGO;
    [SerializeField] GameObject evilEyeGO;
    [SerializeField] GameObject bloodPactGO;
    [SerializeField] GameObject topEffectDeckCard;
    private GameObject currentCard;

    


    [Header("--- Effects ---")]
    [SerializeField] RevealCards evilEyeEffect;
    [SerializeField] StealCardsEffect theTakersEffect;
    [SerializeField] RedrawHand wheelOfFortuneEffect;


    [Header("--- Timers ---")]
    [SerializeField, Range(0, 6)] float effectRevealTime;
    [SerializeField, Range(0, 10)] float transitionTime;
    


    public void DrawEffectCard()
    {
        int cardNum = 0;
        GAMEMANAGER.Instance.canPlay = false;
        

        cardNum = RNGCard(cardNum);
        Debug.Log(cardNum);

        if (cardNum == 1 || cardNum == 2 || cardNum == 3 || cardNum == 4)
        {
            currentCard = wheelOfFortuneGO;
            GAMEMANAGER.Instance.handWasRedrawnByWOF = true;

        }
        else if (cardNum == 5 || cardNum == 6 || cardNum == 7)
        {
            currentCard = bloodPactGO;
        }
        else if (cardNum == 8 || cardNum == 9 || cardNum == 10)
        {
            currentCard = theTakersGO;
        }
        else if (cardNum == 11)
        {
            currentCard = theButcherGO;
        }
        else if(cardNum == 12 || cardNum == 13 || cardNum == 14 || cardNum == 15)
        {
            
            currentCard = evilEyeGO;
        }

        currentCard.transform.position = topEffectDeckCard.transform.position;
        currentCard.transform.rotation = topEffectDeckCard.transform.rotation;
        
        DisplayCard();
    }

    private int RNGCard(int cardNum) 
    { 
        if(GAMEMANAGER.Instance.currentRoundState == RoundState.PLAYERTURN)
        {
            
            return cardNum = UnityEngine.Random.Range(1, 15); 
        }
        else
        {
            
            return cardNum = UnityEngine.Random.Range(1, 11);
        }
    }
    private void DisplayCard()
    {

        
        if(GAMEMANAGER.Instance.currentRoundState == RoundState.PLAYERTURN)
        {
            GAMEMANAGER.Instance.playerEffectTokens--;
            StartCoroutine(PlayerTakeEffectCard(() => {
                DisposeCard();
            }));
        }
        else
        {
            //Make Card burn into existence then brun out
            GAMEMANAGER.Instance.enemyEffectTokens--;
            currentCard.transform.position = GAMEMANAGER.Instance.revealCardsTransform.position;
            currentCard.transform.rotation = GAMEMANAGER.Instance.revealCardsTransform.rotation;
            Invoke(nameof(DisposeCard), effectRevealTime);
        }

        


    }

    private void DisposeCard()
    {
        
        currentCard.transform.position = GAMEMANAGER.Instance.player.playedCardsTransform.position;
        currentCard.transform.rotation = GAMEMANAGER.Instance.player.playedCardsTransform.rotation;
        ApplyEffect();
        
    }

    private void ApplyEffect()
    {
        if(currentCard == wheelOfFortuneGO)
        {
            wheelOfFortuneEffect.Execute();
        }
        else if( currentCard == evilEyeGO)
        {
            evilEyeEffect.Execute();
        }
        else if (currentCard == bloodPactGO)
        {
            OverrideEndGamePickupEffect.OverridePickUp();
        }
        else if (currentCard == theButcherGO)
        {
            
            DeductPointEffect.DeductPoint();
        }
        else if (currentCard == theTakersGO)
        {
            theTakersEffect.Execute();
        }
        GAMEMANAGER.Instance.UpdateUI();
        
    }


    private IEnumerator PlayerTakeEffectCard(System.Action onPlayerReveal)
    {
        float elapsedTime = 0f;

        Vector3 initialPosition = currentCard.transform.position;
        Quaternion initialRotation = currentCard.transform.rotation;

        while(elapsedTime < effectRevealTime)
        {
            currentCard.transform.position = Vector3.Lerp(initialPosition, 
                GAMEMANAGER.Instance.revealCardsTransform.position, 
                elapsedTime / transitionTime);

            currentCard.transform.rotation = Quaternion.Lerp(initialRotation, 
                GAMEMANAGER.Instance.revealCardsTransform.rotation, 
                elapsedTime / transitionTime);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        currentCard.transform.position = GAMEMANAGER.Instance.revealCardsTransform.position;
        currentCard.transform.rotation = GAMEMANAGER.Instance.revealCardsTransform.rotation;

        yield return new WaitForSeconds(effectRevealTime);

        onPlayerReveal?.Invoke();
    }


}

