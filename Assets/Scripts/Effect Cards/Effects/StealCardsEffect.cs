using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealCardsEffect: EffectCard
{
    [SerializeField] private AIBehaviour enemy;
    [SerializeField] private Table player;
   

    public override void Execute()
    {
        if(GAMEMANAGER.Instance.currentRoundState == RoundState.PLAYERTURN)
        {
            List<int> list = GetRandomEnemyCards();

            

            if(list.Count == 0)
            {
                
                //Show that enemy has no cards

            }
            else
            {
                
                List<Card> cards = new List<Card>();
                foreach(int index in list)
                {
                    
                    cards.Add(enemy.collectedCards[index]);
                }

                foreach(Card card in cards)
                {
                    Debug.Log(card.CardValue.ToString() + card.Suit.ToString());
                    player.playedCards.Add(card);
                    enemy.collectedCards.Remove(card);
                }
            }


            EventManager.InvokeEnemyLoseCard();
            EventManager.InvokePlayerPickup();
        }
        else
        {
            List<int> list = GetRandomPlayerCards();

            if (list.Count == 0)
            {

                //Show that enemy has no cards

            }
            else
            {

                List<Card> cards = new List<Card>();
                foreach (int index in list)
                {
                    Debug.Log(index);
                    cards.Add(player.playedCards[index]);
                }

                foreach (Card card in cards)
                {
                    Debug.Log(card.CardValue.ToString() + card.Suit.ToString());
                    enemy.collectedCards.Add(card);
                    player.playedCards.Remove(card);
                }
            }
            EventManager.InvokePlayerLoseCard();
            EventManager.InvokeEnemyPickup();
        }
        GAMEMANAGER.Instance.canPlay = true;
    }


    private List<int> GetRandomPlayerCards()
    {
        int max = player.playedCards.Count;
        int count = 0;
        List<int> indexList = new List<int>() { };


        if (max == 0)
        {

            return indexList;
        }
        else
        {

            if (max > 3)
            {
                max = 3;
            }
            while (count < max)
            {
                int index;
                while (indexList.Contains(index = Random.Range(0, player.playedCards.Count)))
                {
                    index = Random.Range(0, player.playedCards.Count);
                }
                indexList.Add(index);
                count++;
            }
            return indexList;
        }
    }



    private List<int> GetRandomEnemyCards()
    {
        int max = enemy.collectedCards.Count;
        int count = 0;
        List<int> indexList = new List<int>() { };


        if (max == 0)
        {
            return indexList;
        }
        else
        { 
            if(max > 3)
            {
                max = 3;
            }
            while (count < max)
            {
                int index;
                while (indexList.Contains(index = Random.Range(0, enemy.collectedCards.Count)))
                {
                    index = Random.Range(0, enemy.collectedCards.Count);
                }
                indexList.Add(index);
                count++;
            }
            return indexList;
        }
    }
}
