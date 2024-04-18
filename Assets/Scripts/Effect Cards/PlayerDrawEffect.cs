using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.TimeZoneInfo;

public class PlayerDrawEffect : MonoBehaviour
{
    [SerializeField] DeckOfCards effectsCardsDeck;

    [Header("--- Cards ---")]
    [SerializeField] EffectCard theTakers;
    [SerializeField] EffectCard theButcher;
    [SerializeField] EffectCard wheelOfFortune;
    [SerializeField] EffectCard evilEye;
    [SerializeField] EffectCard bloodPact;
    [SerializeField] Transform topEffectDeckCard;
    private EffectCard currentCard;


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
            currentCard = wheelOfFortune;
            GAMEMANAGER.Instance.handWasRedrawnByWOF = true;

        }
        else if (cardNum == 5 || cardNum == 6 || cardNum == 7)
        {
            currentCard = bloodPact;
        }
        else if (cardNum == 8 || cardNum == 9 || cardNum == 10)
        {
            currentCard = theTakers;
        }
        else if (cardNum == 11)
        {
            currentCard = theButcher;
        }
        else if(cardNum == 12 || cardNum == 13 || cardNum == 14 || cardNum == 15)
        {

            currentCard = evilEye;
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
        FindAnyObjectByType<AudioManager>().Play("DrawCard");
        if(GAMEMANAGER.Instance.currentRoundState == RoundState.PLAYERTURN)
        {
            GAMEMANAGER.Instance.playerEffectTokens--;
            if(GAMEMANAGER.Instance.playerEffectTokens == 0)
            {
                effectsCardsDeck.ToggleColor();
            }
            StartCoroutine(burningEffect(currentCard));
            
            StartCoroutine(PlayerTakeEffectCard(() => {
                DisposeCard();
            }));
        }
        else
        {
            //Make Card burn into existence then burn out
            GAMEMANAGER.Instance.enemyEffectTokens--;
            currentCard.transform.position = GAMEMANAGER.Instance.revealCardsTransform.position;
            currentCard.transform.rotation = GAMEMANAGER.Instance.revealCardsTransform.rotation;
            StartCoroutine(burningEffect(currentCard));
            
            Invoke(nameof(DisposeCard), effectRevealTime);
        }

        


    }
    public IEnumerator burningEffect(EffectCard card)
    {
        yield return new WaitForSeconds(effectRevealTime / 1.25f);
        
        AudioManager.Instance.Play("CardBurnOut");
        for (int i = 0; i < 160; i++)
        {
            yield return new WaitForSeconds(1 / 100000000000f);

            card.frontMaterial.SetFloat("_Dissolve_Value", i / 100f - 0.8f);
            card.backMaterial.SetFloat("_Dissolve_Value", i / 100f - 0.8f);

        }

        Debug.Log("burnt");
    }

    private void DisposeCard()
    {
        currentCard.transform.position = GAMEMANAGER.Instance.player.playedCardsTransform.position;
        currentCard.transform.rotation = GAMEMANAGER.Instance.player.playedCardsTransform.rotation;
        currentCard.backMaterial.SetFloat("_Dissolve_Value", - 1f);
        currentCard.frontMaterial.SetFloat("_Dissolve_Value", - 1f);
        ApplyEffect();
        
    }

    private void ApplyEffect()
    { 
        currentCard.Execute();
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

