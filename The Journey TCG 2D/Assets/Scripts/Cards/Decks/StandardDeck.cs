using UnityEngine;
using System.Collections.Generic;

public class StandardDeck : Deck
{
    private void Awake()
    {
        deckFormat = DeckFormat.Standard;
        limitCardAmount = 100;
        
    }
    public override void AddCard(Card cardToAdd)
    {
        if (IsFull())
        {
            Debug.Log("This Deck is full. Cannot add more cards.");
            return;
        }
        Debug.Log($"Added {cardToAdd.name} to the Deck.");
        
        if (cardLimits.ContainsKey(cardToAdd.name))
        {
            if (cardLimits[cardToAdd.name]<8)
            {
                deck.Add(cardToAdd);
                cardLimits[cardToAdd.name]++;
            }
            else
            {
                Debug.Log($"Cannot add more copies of {cardToAdd.name} to this Deck.");
            }
            return;
        }
        deck.Add(cardToAdd);
        cardLimits.Add(cardToAdd.name, 1);
    }

    public override void RemoveCard(Card cardToRemove)
    {
        base.RemoveCard(cardToRemove);
        cardLimits[cardToRemove.name]--;
        if (cardLimits[cardToRemove.name] == 0)
        {
            cardLimits.Remove(cardToRemove.name);
        }
    }
}
