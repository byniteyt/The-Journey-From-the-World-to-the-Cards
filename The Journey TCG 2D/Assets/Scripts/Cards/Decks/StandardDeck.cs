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

    protected override void RemoveCard(Card cardToRemove)
    {
        base.RemoveCard(cardToRemove);
        cardLimits[cardToRemove.name]--;
        if (cardLimits[cardToRemove.name] == 0)
        {
            cardLimits.Remove(cardToRemove.name);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
