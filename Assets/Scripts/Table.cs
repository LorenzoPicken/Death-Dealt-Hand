using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public List<Card> tableList = new List<Card>();
    int tableTotal;

    // Start is called before the first frame update
    void Start()
    {
        tableTotal = 0;
    }

    // Update is called once per frame
    
    public void UpdateTable()
    {
        int newTotal = 0;
        foreach (Card card in tableList) 
        {
            newTotal += card.CardValue;
        }
        tableTotal = newTotal;
    }

    public void AddCards(Card card)
    {
        tableList.Add(card);
    }

    public void RemoveCards(List<Card> removeList)
    {
        foreach (Card card in removeList)
        {
            tableList.Remove(card);
        }
        removeList.Clear();
    }
}
