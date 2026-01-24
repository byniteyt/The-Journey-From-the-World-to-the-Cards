using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class StandardDeck : Deck
{
    protected override void OnInit()
    {
        deckFormat = DeckFormat.Standard;
        limitCardAmount = 10;
        InitLimits();   
    }
    public StandardDeck() : base()
    {
        deckFormat = DeckFormat.Standard;
        limitCardAmount = 100;
        limitPerCard = 8;
    }
    public StandardDeck(Deck deckToClone) : base(deckToClone)
    {

    }
    public override void AddCard(Card cardToAdd)
    {
        if (IsFull())
        {
            Debug.Log("This Deck is full. Cannot add more cards.");
            return;
        }
        if (cardToAdd == null)
        {
            Debug.LogWarning("Cannot add null card to the Deck.");
            return;
        }

        AddCardToDictionary(cardToAdd);
    }

    public override void RemoveCard(Card cardToRemove)
    {
        string key = cardToRemove.cardName;
        if (cardLimits.ContainsKey(key))
        {
            cardLimits[key]--;
            if (cardLimits[key] == 0)
            {
                cardLimits.Remove(key);
                deck.Remove(cardToRemove);
            }
        }

    }

    protected override bool IsFull()
    {
        return base.IsFull();
    }

    public override bool IsValidForPlay()
    {
        //return (deck.Count >= 40 && deck.Count <= 100);
        return true;
    }

    private void InitLimits()
    {
        cardLimits = new Dictionary<string, int>();
        foreach (Card card in deck)
        {
            if (cardLimits.ContainsKey(card.cardName))
            {
                cardLimits[card.cardName]++;
            }
            else
            {
                cardLimits.Add(card.cardName, 1);
            }
        }
    }
}
