using System;
using UnityEngine;

[Serializable]
public class Card 
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
        Card card = new()
        {
            artwork = this.artwork,
            cardName = this.cardName,
            description = this.description,
            cost = this.cost
        };
        return card;
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
