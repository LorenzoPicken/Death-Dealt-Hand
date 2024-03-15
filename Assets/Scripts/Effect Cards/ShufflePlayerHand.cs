using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShufflePlayerHand : EffectCard
{
    [SerializeField] PlayerStateManager player;
    [SerializeField] AIBehaviour enemy;
    
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
        int cardsInHand = 0;
        if (GAMEMANAGER.Instance.currentRoundState == RoundState.PLAYERTURN)
        {
            foreach(Card card in player.playerCards)
            {
                //Visually Send Card Back To Deck
            }
            cardsInHand = player.playerCards.Count;
            for(int i = 0; i < cardsInHand; i++)
            {
                player.playerCards[0].InHand = false;
                GAMEMANAGER.Instance.deck.Add(player.playerCards[0]);
                player.playerCards.Remove(player.playerCards[0]);
                
            }

            //Reshuffle Deck Visualy
            GAMEMANAGER.Instance.deck = GAMEMANAGER.Instance.Shuffle(GAMEMANAGER.Instance.deck);

            foreach(Transform playerSlot in GAMEMANAGER.Instance.playerSlots)
            {
                GAMEMANAGER.Instance.deck[0].transform.position = playerSlot.transform.position;
                GAMEMANAGER.Instance.deck[0].transform.rotation = playerSlot.transform.rotation;
                GAMEMANAGER.Instance.deck[0].inHand = true;
                GAMEMANAGER.Instance.deck.Remove(GAMEMANAGER.Instance.deck[0]);


                //Replace Card Teleport With Animation
            }



        }
        else
        {
            foreach (Card card in enemy.handList)
            {
                //Visually Send Card Back To Deck
            }
            cardsInHand = enemy.handList.Count;

            for (int i = 0; i < cardsInHand; i++)
            {
                
                GAMEMANAGER.Instance.deck.Add(enemy.handList[0]);
                enemy.handList.Remove(enemy.handList[0]);

            }

            //Reshuffle Deck Visualy
            GAMEMANAGER.Instance.deck = GAMEMANAGER.Instance.Shuffle(GAMEMANAGER.Instance.deck);


            foreach (Transform enemySlot in GAMEMANAGER.Instance.enemySlots)
            {
                GAMEMANAGER.Instance.deck[0].transform.position = enemySlot.transform.position;
                GAMEMANAGER.Instance.deck[0].transform.rotation = enemySlot.transform.rotation;
                
                GAMEMANAGER.Instance.deck.Remove(GAMEMANAGER.Instance.deck[0]);


                //Replace Card Teleport With Animation
            }
        }
    }

    
}
