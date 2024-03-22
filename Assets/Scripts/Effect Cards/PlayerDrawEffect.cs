using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    public void DrawEffectCard()
    {
        int cardNum = 0;
        GAMEMANAGER.Instance.canPlay = false;
        GAMEMANAGER.Instance.playerEffectTokens--;

        cardNum = RNGCard(cardNum);

        if (cardNum == 1 || cardNum == 2 || cardNum == 3)
        {
            currentCard = wheelOfFortuneGO;
            
        }
        else if (cardNum == 4 || cardNum == 5 || cardNum == 6)
        {
            currentCard = bloodPactGO;
        }
        else if (cardNum == 7 || cardNum == 8 || cardNum == 9)
        {
            currentCard = evilEyeGO;
        }
        else if (cardNum == 10 || cardNum == 11)
        {
            currentCard = theTakersGO;
        }
        else if(cardNum == 12)
        {
            Debug.Log("Butcher");
            currentCard = theButcherGO;
        }

        currentCard.transform.position = topEffectDeckCard.transform.position;
        currentCard.transform.rotation = topEffectDeckCard.transform.rotation;
        //topEffectDeckCard?.SetActive(false);
        DisplayCard();
    }

    private void DisplayCard()
    {

        currentCard.transform.position = GAMEMANAGER.Instance.revealCardsTransform.position;
        currentCard.transform.rotation = GAMEMANAGER.Instance.revealCardsTransform.rotation;
        Invoke(nameof(DisposeCard), 4);

        //trigger Animation


    }

    private void DisposeCard()
    {
        //topEffectDeckCard?.SetActive(true);
        currentCard.transform.position = GAMEMANAGER.Instance.player.playedCardsTransform.position;
        currentCard.transform.rotation = GAMEMANAGER.Instance.player.playedCardsTransform.rotation;
        ApplyEffect();
        //Disolve Card
        //Move Card Away
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
        GAMEMANAGER.Instance.canPlay = true;
    }

    private int RNGCard(int cardNum) 
    { 
        return cardNum = Random.Range(10, 12); 
    }
   


}

