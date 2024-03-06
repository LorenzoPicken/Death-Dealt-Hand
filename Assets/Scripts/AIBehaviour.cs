using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIBehaviour: MonoBehaviour
{
    Card currentCard;
    [SerializeField] Transform AI_CollectedCards;
    

    public List<Card> handList = new List<Card>() { };
    public List<Card> collectedCards = new List<Card>() { };
    List<List<Card>> listOfPlays = new List<List<Card>>();
    Dictionary<Card, List<Card>> playDict = new Dictionary<Card, List<Card>> { };
    List<List<Card>> playList = new List<List<Card>> { };

    [Header("--- Table ---")]
    [SerializeField] Table table;
    [SerializeField] CardSlot slot1;
    [SerializeField] CardSlot slot2;
    [SerializeField] CardSlot slot3;
    [SerializeField] CardSlot slot4;
    [SerializeField] CardSlot slot5;
    [SerializeField] CardSlot slot6;
    [SerializeField] CardSlot slot7;
    [SerializeField] CardSlot slot8;
    [SerializeField] CardSlot slot9;
    public List<CardSlot> slotsList = new List<CardSlot> { };




    [Header("Timing")]
    [SerializeField] int minimumTime;
    [SerializeField] int maximumTime;

    private void Start()
    {
        slotsList.Add(slot1);
        slotsList.Add(slot2);
        slotsList.Add(slot3);
        slotsList.Add(slot4);
        slotsList.Add(slot5);
        slotsList.Add(slot6);
        slotsList.Add(slot7);
        slotsList.Add(slot8);
        slotsList.Add(slot9);
    }


    public void CountDown()
    {
        int waitTime = Random.Range(minimumTime, maximumTime);
        Invoke("AIPlay", waitTime);
    }


    private void AIPlay()
    {
        
            
            int currentIndex = 0;
            int playIndex = 0;


            //Iterates through all cards in the AI's hand
            while (currentIndex < handList.Count)
            {
                currentCard = handList[currentIndex];
                bool cardOfSameValueOnTable = false;
                List<Card> soloCardList = new List<Card> { };
                foreach (Card card in table.cards)
                {
                    //Checks if there is a card of the same value as current card on table and adds them to a list
                    if (currentCard.CardValue == card.CardValue)
                    {
                        cardOfSameValueOnTable = true;
                        soloCardList.Add(card);
                    }
                }

                
                if (cardOfSameValueOnTable == true)
                {
                    bool sunfound = false;
                    foreach (Card card in soloCardList)
                    {
                        //Checks to see if any of those same value cards were of Suit suns and adds to dictionary of possible plays
                        if (card.Suit == Suit.SUNS)
                        {
                            playDict.Add(currentCard, new List<Card> { card });
                            sunfound = true;
                        }

                    }

                    //If not, it will
                    if (sunfound == false)
                    {
                       
                        playDict.Add(currentCard, new List<Card> { soloCardList[0] });

                        

                    }
                    sunfound = false;
                }
                else
                {

                    FindCombinations(table.cards, currentCard);
                    if (playList.Count > 0)
                    {

                        playDict.Add(currentCard, playList[playIndex]);
                    }

                }
                cardOfSameValueOnTable = false;
                soloCardList.Clear();






                currentIndex++;
            }
            playIndex = 0;


            if (playDict.Count == 0)
            {
                int tableTotal = 0;
                foreach (Card card in table.cards)
                {
                    tableTotal += card.CardValue;
                }

                if (tableTotal > 10)
                {
                    Card smallestCard = new Card();
                    smallestCard.CardValue = handList[0].CardValue; smallestCard.Suit = handList[0].Suit;

                    foreach (Card card in handList)
                    {
                        if (card.CardValue <= smallestCard.CardValue)
                        {
                            smallestCard.CardValue = card.CardValue; smallestCard.Suit = card.Suit;
                        }
                    }

                    foreach(Card card in handList)
                    {
                        if (card.CardValue == smallestCard.CardValue && card.Suit == smallestCard.Suit)
                        {
                            PlaceCards(card);

                        }
                    }
                }
                else
                {
                    int count = 0;
                    Card smallestCard = new Card();
                    smallestCard.CardValue = 0; smallestCard.Suit = Suit.CLUBS;
                    foreach (Card card in handList)
                    {
                        if (card.CardValue + tableTotal > 10)
                        {
                            if (smallestCard.CardValue == 0)
                            {
                                smallestCard.Suit = card.Suit;
                                smallestCard.CardValue = card.CardValue;
                            }
                            else if (smallestCard.CardValue > card.CardValue)
                            {
                                smallestCard.CardValue = card.CardValue; smallestCard.Suit = card.Suit;

                            }
                        }
                        else
                        {
                            count++;
                        }
                    }

                    if (count == handList.Count)
                    {
                        foreach (Card card in handList)
                        {
                            if (smallestCard.CardValue == 0)
                            {
                                smallestCard.Suit = card.Suit;
                                smallestCard.CardValue = card.CardValue;
                            }
                            else if (smallestCard.CardValue > card.CardValue)
                            {
                                smallestCard.CardValue = card.CardValue; smallestCard.Suit = card.Suit;

                            }
                        }
                    }
                    foreach (Card card in handList)
                    {
                        if (card.CardValue == smallestCard.CardValue && card.Suit == smallestCard.Suit)
                        {
                            PlaceCards(card);

                        }
                    }
                    
                }

            }

            else
            {
                Debug.Log("Making Final Play");
                FinalChoice(table.cards, handList, playDict);
            }
        
    }

    public int CalculateTableTotal(List<Card> tableList)
    {
        int tableTotal = 0;
        foreach (Card card in tableList)
        {
            tableTotal += card.CardValue;
        }
        return tableTotal;
    }




    #region narrows down option to most beneficial play
    public void FinalChoice(List<Card> tableList, List<Card> handList, Dictionary<Card, List<Card>> dict)
    {
        Dictionary<int, Dictionary<Card, List<Card>>> bigDict = new Dictionary<int, Dictionary<Card, List<Card>>> { };
        bool isScopa = false;

        foreach (KeyValuePair<Card, List<Card>> combinations in dict)
        {
            Card currentKey = new Card();
            currentKey.CardValue = 0; currentKey.Suit = Suit.SUNS;
            currentKey = combinations.Key;
            if (isScopa == false)
            {

                isScopa = ScopaChecker(handList, tableList, bigDict, dict, currentKey);
                SevenSunChecker(handList, tableList, bigDict, dict, currentKey);
                SunsChecker(handList, tableList, bigDict, dict, currentKey);
                NormalCardsChecker(handList, tableList, bigDict, dict, currentKey);
            }
            isScopa = false;
        }
        Dictionary<int, Dictionary<Card, List<Card>>> finalDict = new Dictionary<int, Dictionary<Card, List<Card>>> { };


        int max = bigDict.Keys.First();
        foreach (KeyValuePair<int, Dictionary<Card, List<Card>>> combination in bigDict)
        {
            if (combination.Key >= max)
            {

                finalDict.Clear();
                finalDict.Add(combination.Key, combination.Value);
                max = combination.Key;
            }
        }
        if (max > 0)
        {
            Dictionary <Card, List<Card>> dictionary = finalDict.First().Value;
            PickUpCards(dictionary);
        }
        else if (max == 0)
        {
            
            var rand = Random.Range(1, 3);

            if (rand == 1)
            {
                Dictionary<Card, List<Card>> dictionary = finalDict.First().Value;
                PickUpCards(dictionary);
            }
            else
            {
                int tableTotal;
                tableTotal = CalculateTableTotal(tableList);

                Card cardToPlay = new Card();
                cardToPlay.CardValue = 0; cardToPlay.Suit = Suit.CLUBS;
                bool match = false;


                if (tableTotal >= 10)
                {
                    foreach (Card handCard in handList)
                    {
                        foreach (Card tableCard in tableList)
                        {
                            match = false;
                            if (handCard.CardValue == tableCard.CardValue)
                            {
                                match = true;
                            }
                            if (match != true)
                            {
                                if (cardToPlay.CardValue == 0)
                                {
                                    cardToPlay.CardValue = handCard.CardValue;
                                    cardToPlay.Suit = handCard.Suit;
                                }
                                else
                                {
                                    if (cardToPlay.CardValue > handCard.CardValue)
                                    {
                                        cardToPlay.Suit = handCard.Suit;
                                        cardToPlay.CardValue = handCard.CardValue;
                                    }
                                }
                            }
                        }
                        if (cardToPlay.CardValue != 0)
                        {
                            foreach (Card card in handList)
                            {
                                if (card.CardValue == cardToPlay.CardValue && card.Suit == cardToPlay.Suit)
                                {
                                    PlaceCards(card);

                                }
                            }
                            break;
                        }
                        else
                        {
                            Dictionary<Card, List<Card>> dictionary = finalDict.First().Value;
                            PickUpCards(dictionary);
                        }
                    }

                }
                else
                {
                    int count = 0;
                    foreach (Card card in handList)
                    {
                        if (card.CardValue + tableTotal > 10)
                        {
                            if (cardToPlay.CardValue == 0)
                            {
                                cardToPlay.Suit = card.Suit;
                                cardToPlay.CardValue = card.CardValue;
                            }
                            else if (cardToPlay.CardValue > card.CardValue)
                            {
                                cardToPlay.Suit = card.Suit;
                                cardToPlay.CardValue = card.CardValue;
                            }
                        }
                        else
                        {
                            count++;
                        }

                    }

                    if (count == handList.Count)
                    {
                        foreach (Card card in handList)
                        {
                            if (cardToPlay.CardValue == 0)
                            {
                                cardToPlay.CardValue = card.CardValue; cardToPlay.Suit = card.Suit;
                            }
                            else
                            {
                                if (cardToPlay.CardValue > card.CardValue)
                                {
                                    cardToPlay.Suit = card.Suit;
                                    cardToPlay.CardValue = card.CardValue;
                                }
                            }
                        }
                    }
                    foreach (Card card in handList)
                    {
                        if (card.CardValue == cardToPlay.CardValue && card.Suit == cardToPlay.Suit)
                        {
                            PlaceCards(card);

                        }
                    }
                }
            }
        }
        else
        {
            Suit suitToPlay = Suit.CLUBS;
            int valueToPlay = 0;
            //Card cardToPlay = new Card();
            //cardToPlay.CardValue = 0; cardToPlay.Suit = Suit.CLUBS;
            int count = 0;

            int tableTotal;
            tableTotal = CalculateTableTotal(tableList);
            bool match = false;

            foreach (Card card in handList)
            {

                foreach (Card tableCard in tableList)
                {
                    if (card.CardValue == tableCard.CardValue)
                    {

                        match = true;
                    }
                }

                if (card.CardValue + tableTotal > 10)
                {

                    if (valueToPlay == 0)
                    {

                        suitToPlay = card.Suit;
                        valueToPlay = card.CardValue;
                    }
                    else if (valueToPlay > card.CardValue)
                    {
                        suitToPlay = card.Suit;
                        valueToPlay = card.CardValue;
                    }
                }
                else
                {
                    count++;
                }








            }

            if (count == handList.Count)
            {
                foreach (Card card in handList)
                {
                    if (valueToPlay == 0)
                    {
                        valueToPlay = card.CardValue; suitToPlay = card.Suit;
                    }
                    else
                    {
                        if (valueToPlay > card.CardValue)
                        {
                            suitToPlay = card.Suit;
                            valueToPlay = card.CardValue;
                        }
                    }
                }
                foreach (Card card in handList)
                {
                    if (card.CardValue == valueToPlay && card.Suit == suitToPlay)
                    {
                        PlaceCards(card);

                    }
                }
            }
            else if (match == true)
            {
                Dictionary<Card, List<Card>> dictionary = finalDict.First().Value;
                PickUpCards(dictionary);
            }

        }



    }
    #endregion

    #region Checks risks of a play
    void RiskChecker(Dictionary<Card, List<Card>> dict, Dictionary<int, Dictionary<Card, List<Card>>> bigDict, int reward, List<Card> tableList, List<Card> handList, Card currentKey)
    {
        int tableTotal = 0;
        int riskRewardValue = reward;

        foreach (Card card in tableList)
        {
            tableTotal += card.CardValue;
        }
        if (tableTotal - currentKey.CardValue > 0 && tableTotal - currentKey.CardValue <= 10)
        {
            riskRewardValue -= 3;
        }
        if (!bigDict.ContainsKey(riskRewardValue))
        {
            Dictionary<Card, List<Card>> tenpDict = new Dictionary<Card, List<Card>>();
            tenpDict.Add(currentKey, dict[currentKey]);
            bigDict.Add(riskRewardValue, tenpDict);
        }
        else
        {
            int count = 0;
            Dictionary<Card, List<Card>> CountDict = bigDict[riskRewardValue];
            List<Card> CountList = new List<Card> { };
            foreach (KeyValuePair<Card, List<Card>> combination in CountDict)
            {
                foreach (Card card in combination.Value)
                {
                    count++;
                }
            }
            if (dict[currentKey].Count > count)
            {
                Dictionary<Card, List<Card>> tenpDict = new Dictionary<Card, List<Card>>();
                tenpDict.Add(currentKey, dict[currentKey]);
                bigDict.Remove(riskRewardValue);
                bigDict.Add(riskRewardValue, tenpDict);
            }
            else if (dict[currentKey].Count == count)
            {
                int sunCount = 0;
                int newSunCount = 0;
                Dictionary<Card, List<Card>> newDict = bigDict[riskRewardValue];


                foreach (KeyValuePair<Card, List<Card>> combination in newDict)
                {
                    if (combination.Key.Suit == Suit.SUNS)
                    {
                        sunCount++;
                    }
                    foreach (Card card in combination.Value)
                    {
                        if (card.Suit == Suit.SUNS)
                        {
                            sunCount++;
                        }

                    }
                }

                foreach (KeyValuePair<Card, List<Card>> newCombination in dict)
                {
                    if (newCombination.Key.Suit == Suit.SUNS)
                    {
                        newSunCount++;
                    }
                    foreach (Card card in newCombination.Value)
                    {
                        if (card.Suit == Suit.SUNS)
                        {
                            newSunCount++;
                        }

                    }
                }

                if (newSunCount > sunCount)
                {
                    Dictionary<Card, List<Card>> tenpDict = new Dictionary<Card, List<Card>>();
                    tenpDict.Add(currentKey, dict[currentKey]);
                    bigDict.Remove(riskRewardValue);
                    bigDict.Add(riskRewardValue, tenpDict);
                }
            }

        }

    }
    #endregion

    #region Checks for all different types of Plays
    public bool ScopaChecker(List<Card> handList, List<Card> tableList, Dictionary<int, Dictionary<Card, List<Card>>> bigDict, Dictionary<Card, List<Card>> dict, Card currentKey)
    {
        int reward = 10;
        int currentCardValue = currentKey.CardValue;
        int tableTotal = 0;
        foreach (Card card in tableList)
        {
            tableTotal += card.CardValue;
        }

        if (currentCardValue == tableTotal)
        {
            Dictionary<Card, List<Card>> tenpDict = new Dictionary<Card, List<Card>>();
            tenpDict.Add(currentKey, dict[currentKey]);
            bigDict.Add(reward, tenpDict);
            return true;

        }
        return false;
    }
    public void SevenSunChecker(List<Card> handList, List<Card> tableList, Dictionary<int, Dictionary<Card, List<Card>>> bigDict, Dictionary<Card, List<Card>> dict, Card currentKey)
    {
        int reward = 0;
        bool foundSevenSun = false;
        Card searchedKey = new Card();
        searchedKey.CardValue = 7; searchedKey.Suit = Suit.SUNS;
        if (dict[currentKey].Count > 1)
        {
            if (currentKey.Suit == Suit.SUNS && currentKey.CardValue == 7)
            {

                reward = 9;

                RiskChecker(dict, bigDict, reward, tableList, handList, currentKey);
                return;
            }
            else
            {
                foreach (Card card in dict[currentKey])
                {
                    if (card.CardValue == 7 && card.Suit == Suit.SUNS)
                    {
                        foundSevenSun = true;
                    }
                }
            }
            if (foundSevenSun == true)
            {
                reward = 9;

                RiskChecker(dict, bigDict, reward, tableList, handList, currentKey);
                return;
            }

        }
        else if (dict[currentKey].Count == 1)
        {
            if (currentKey.CardValue == 7 && currentKey.Suit == Suit.SUNS)
            {

                reward = 8;

                RiskChecker(dict, bigDict, reward, tableList, handList, currentKey);
                return;
            }
        }
        if (dict[currentKey].Count == 1)
        {
            bool foundSevenSuns = false;
            foreach (Card card in dict[currentKey])
            {
                if (card.CardValue == searchedKey.CardValue && card.Suit == searchedKey.Suit)
                {
                    foundSevenSuns = true;
                }
            }
            if (foundSevenSuns == true)
            {

                reward = 8;

                RiskChecker(dict, bigDict, reward, tableList, handList, currentKey);

            }
            foundSevenSuns = false;
        }




    }

    public void SunsChecker(List<Card> handList, List<Card> tableList, Dictionary<int, Dictionary<Card, List<Card>>> bigDict, Dictionary<Card, List<Card>> dict, Card currentKey)
    {
        int sunCount = 0;
        int reward = 0;

        if (currentKey.Suit == Suit.SUNS)
        {
            sunCount++;
        }
        foreach (Card card in dict[currentKey])
        {
            if (card.Suit == Suit.SUNS)
            {
                sunCount++;
            }
        }
        if (sunCount >= 2)
        {
            reward = 5;
            RiskChecker(dict, bigDict, reward, tableList, handList, currentKey);
            return;


        }
        else if (sunCount == 1)
        {
            reward = 2;
            RiskChecker(dict, bigDict, reward, tableList, handList, currentKey);
            return;

        }



    }



    public void NormalCardsChecker(List<Card> handList, List<Card> tableList, Dictionary<int, Dictionary<Card, List<Card>>> bigDict, Dictionary<Card, List<Card>> dict, Card currentKey)
    {
        int reward = 0;
        if (dict[currentKey].Count >= 3)
        {
            reward = 3;
            RiskChecker(dict, bigDict, reward, tableList, handList, currentKey);
            return;

        }
        else if (dict[currentKey].Count == 2)
        {
            reward = 2;
            RiskChecker(dict, bigDict, reward, tableList, handList, currentKey);
            return;
        }
        else if (dict[currentKey].Count == 1)
        {
            reward = 1;
            RiskChecker(dict, bigDict, reward, tableList, handList, currentKey);
            return;
        }

    }
    #endregion


    public void FindCombinations(List<Card> tableList, Card currentCard)
    {

        List<List<Card>> result = new List<List<Card>>();
        List<Card> currentCombination = new List<Card>();

        FindCombinationsHelper(tableList, currentCard, 0, currentCombination, result);


        CountSuns(result);


    }

    void FindCombinationsHelper(List<Card> tableList, Card remainingCard, int startIndex, List<Card> currentCombination, List<List<Card>> result)
    {
        if (remainingCard.CardValue == 0)
        {
            // Combination found, add a copy to the result
            result.Add(new List<Card>(currentCombination));
            return;
        }

        for (int i = startIndex; i < tableList.Count; i++)
        {
            if (tableList[i].CardValue <= remainingCard.CardValue)
            {
                // Add the card to the current combination
                currentCombination.Add(tableList[i]);

                // Recursively search for combinations with the remaining value
                Card newCard = new Card();
                newCard.CardValue = remainingCard.CardValue - table.cards[i].CardValue; newCard.Suit = remainingCard.Suit;
                FindCombinationsHelper(tableList, newCard, i + 1, currentCombination, result);

                // Backtrack - remove the last added card for the next iteration
                currentCombination.RemoveAt(currentCombination.Count - 1);
            }
        }
    }

    void CountSuns(List<List<Card>> bigList)
    {
        Dictionary<int, List<Card>> sundict = new Dictionary<int, List<Card>>();
        foreach (List<Card> list in bigList)
        {
            int suncount = 0;
            foreach (Card card in list)
            {
                if (card.Suit == Suit.SUNS)
                {
                    suncount++;
                }
            }
            if (sundict.ContainsKey(suncount))
            {
                if (sundict[suncount].Count >= list.Count)
                {
                    bool foundSevensun = false;
                    foreach (Card card in list)
                    {
                        if (card.CardValue == 7 && card.Suit == Suit.SUNS)
                        {
                            foundSevensun = true;
                        }
                    }
                    if (foundSevensun == true)
                    {
                        sundict[suncount] = new List<Card>(list);
                        break;
                    }
                    else
                    {

                        break;
                    }
                }
                else
                {
                    sundict[suncount] = new List<Card>(list);
                }
            }
            else
            {
                sundict.Add(suncount, list);

            }

        }
        FinalList(sundict);
    }

    void FinalList(Dictionary<int, List<Card>> dict)
    {
        int max = 0;
        List<Card> finalList = new List<Card>();

        foreach (KeyValuePair<int, List<Card>> combination in dict)
        {
            if (combination.Key >= max)
            {
                finalList.Clear();
                foreach (Card card in combination.Value)
                {
                    finalList.Add(card);
                }
                max = combination.Key;
            }

        }
        playList.Clear();

        if (finalList.Count >= 1)
        {
            playList.Add(finalList);

        }



    }


    public void PickUpCards(Dictionary<Card, List<Card>> dict)
    {
        Debug.Log("Making a Play");
        List<Card> cardsToRemoveFromHand = new List<Card>();
        List<Card> cardsToRemoveFromTable = new List<Card>();

        foreach (KeyValuePair<Card, List<Card>> combination in dict)
        {
            foreach (Card card in handList.ToList()) // Use ToList() to create a copy of the list for iteration
            {
                if (card.CardValue == combination.Key.CardValue && card.Suit == combination.Key.Suit)
                {
                    card.transform.position = AI_CollectedCards.position;
                    cardsToRemoveFromHand.Add(card);
                    Debug.Log(card.CardValue + card.Suit);
                }
            }

            foreach (Card card in combination.Value.ToList()) // Use ToList() to create a copy of the list for iteration
            {
                foreach (Card tableCard in table.cards.ToList()) // Use ToList() to create a copy of the list for iteration
                {
                    if (card.CardValue == tableCard.CardValue && card.Suit == tableCard.Suit)
                    {
                        card.transform.position = AI_CollectedCards.position;
                        cardsToRemoveFromTable.Add(card);
                    }
                }
            }
        }

        // Remove cards from handList
        foreach (Card cardToRemove in cardsToRemoveFromHand)
        {
            collectedCards.Add(cardToRemove);
            handList.Remove(cardToRemove);
        }

        // Remove cards from table.cards
        foreach (Card cardToRemove in cardsToRemoveFromTable)
        {
            collectedCards.Add(cardToRemove);
            table.cards.Remove(cardToRemove);
        }

        EndTurn();
    }

    private void EndTurn()
    {
        listOfPlays.Clear();
        playDict.Clear();
        playList.Clear();
        GAMEMANAGER.Instance.currentRoundState = RoundState.CHECKPLAYSTATE;

    }

    

    private void PlaceCards(Card card)
    {
        Debug.Log("Cant Play");
        foreach(CardSlot slot in slotsList)
        {
            if(slot.available == true)
            {
                handList.Remove(card);
                card.transform.position = slot.transform.position;
                break;
            }
        }
        EndTurn();
        
    }

}
