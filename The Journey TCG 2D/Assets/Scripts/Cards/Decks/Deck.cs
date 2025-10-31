using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Deck : MonoBehaviour
{
    protected string deckName;

    protected DeckFormat deckFormat;

    protected List<Card> deck = new List<Card>();

    protected int limitCardAmount;

    protected Dictionary<string, int> cardLimits = new Dictionary<string, int>();

    protected void OnCreate()
    {
        this.deckName = "New Deck";
    }
    public DeckFormat GetFormat() => this.deckFormat;

    public List<Card> GetCards()
    {
        return deck;
    }

    public Dictionary<string, int> GetDictionary()
    {
        return cardLimits;
    }

    public Card GetCardByName(string cardName)
    {
        foreach (Card card in deck)
        {
            if (card.name == cardName)
            {
                return card;
            }
        }
        return null;
    }

    public int GetCardLimit(Card card)
    {
        if (cardLimits.ContainsKey(card.name))
        {
            return cardLimits[card.name];
        }
        return 0;
    }

    public abstract void AddCard(Card cardToAdd);
     protected virtual bool IsFull()
    {
        if (deck==null)
        {
            Debug.Log("Deck is null");
            return false;
        }
        return !(deck.Count<limitCardAmount);
    }
    protected virtual void RemoveCard(Card cardToRemove) {
        deck.Remove(cardToRemove);
    }
    protected virtual bool CorrectSize()
    {
        return deck.Count == limitCardAmount;
    }
    public void SetDeckName(string newName)
    {
        deckName = newName;
    }
}
