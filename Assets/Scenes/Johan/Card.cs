using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Card : MonoBehaviour
{
    
    [SerializeField] private Suit suit;
    [SerializeField] private int cardValue;
    private bool selectable;
    public bool inHand;
    public bool inTable;
    private bool selected = false;
    public Sprite sprite;
    private Outline outline;
    

   
    public Suit Suit
    {
        get { return suit; }
        set { suit = value; }
    }

    public int CardValue { get => cardValue; set => cardValue = value; }
    public bool Selectable { get => selectable; set => selectable = value; }
    public bool InHand { get => inHand; set => inHand = value; }
    public bool Selected { get => selected; set => selected = value; }

    private void Awake()
    {
        outline = gameObject.AddComponent<Outline>();
        outline.enabled = false;
        outline.OutlineWidth = 5f;
        outline.OutlineMode = Outline.Mode.OutlineVisible;
    }
    private void OnMouseOver()
    {
        outline.enabled = true;
    }
    private void OnMouseExit()
    {
        outline.enabled = false;
    }
}

public enum Suit
{
    SUNS,
    JARS,
    SABERS,
    CLUBS
}
