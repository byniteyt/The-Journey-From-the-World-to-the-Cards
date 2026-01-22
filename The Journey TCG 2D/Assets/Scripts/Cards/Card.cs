using System;
using UnityEngine;

[Serializable]
public abstract class Card 
{
    // Basic Info
    public Sprite artwork;
    public string cardName;
    public string description;
    public int cost;

    // Advanced Info
    /*public CardType cardType;
    public Rarity rarity;
    public Element element;*/

    // Advanced UI Elements
    //[SerializeField] private TextMeshProUGUI cardTypeText;

    public virtual Card Clone()
    {
        return (Card)this.MemberwiseClone();
    }

    protected virtual void Update()
    {
        
    }

    public void OnMouseOver()
    {

    }
    public void OnMouseExit()
    {
    }
    public virtual void OnMouseDown()
    {
        UseCard();
    }
    public virtual void UseCard()
    {

    }
    public virtual void ShowCardDetails()
    {
        
    }
    
    
}
