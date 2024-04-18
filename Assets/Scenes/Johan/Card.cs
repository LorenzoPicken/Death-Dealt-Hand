using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
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
    public Outline outline;
    public Material dissolveMaterialFront;
    public Material dissolveMaterialBack;
    

   
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
        dissolveMaterialFront.SetFloat("_Dissolve_Value", 1f );
        dissolveMaterialBack.SetFloat("_Dissolve_Value", 1f );
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
