using System.Collections.Generic;
using UnityEngine;

public class WildDeck : Deck
{
    private void Awake()
    {
        deckFormat = DeckFormat.Wild;
        limitCardAmount = 40;
        cardLimits = new Dictionary<string, int>();
    }

    public override void AddCard(Card cardToAdd)
    {
        if (IsFull())
        {
            Debug.Log("Wild Deck is full. Cannot add more cards.");
            return;
        }
        if (cardLimits.ContainsKey(cardToAdd.name))
        {
            if (!HasEnoughCards(cardToAdd))
            {
                deck.Add(cardToAdd);
                cardLimits[cardToAdd.name]++;
            }
            else
            {
                Debug.Log("Cannot add more copies of " + cardToAdd.name + " to the Wild Deck.");
                //deck.Remove(cardToAdd);
            }
            return;
        }
        deck.Add(cardToAdd);
        cardLimits.Add(cardToAdd.name, 1);
    }
    bool HasEnoughCards(Card cardToAdd)
    {
        int count = 0;
        foreach (var card in deck)
        {
            if (card.name == cardToAdd.name)
            {
                count++;
            }
        }
        return count == 2;
    }

    protected override bool IsFull()
    {
        return deck.Count >= limitCardAmount;
    }
}
