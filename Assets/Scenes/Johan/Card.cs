using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Card : MonoBehaviour
{
    [SerializeField] public Suit suit;
    [SerializeField] public int cardValue;
    [SerializeField] public bool selectable = false;
}

public enum Suit
{
    SUNS,
    JARS,
    SABERS,
    CLUBS
}
