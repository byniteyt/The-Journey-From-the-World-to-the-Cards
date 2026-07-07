using System;
using UnityEngine;

[Serializable]
public class Card 
{
    // Event for when the card is clicked
    public static event Action<Card> OnCardClicked;
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
        OnCardClicked?.Invoke(this);
    }
    
    
}
