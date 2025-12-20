using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[Serializable]
public abstract class Deck
{
    public string deckName;

    public DeckFormat deckFormat;

    public List<Card> deck;
    
    protected int limitCardAmount;

    protected Dictionary<string, int> cardLimits;

    protected void OnCreate()
    {
        deckName = "New Deck";
        deck = new List<Card>();
        cardLimits = new Dictionary<string, int>();
    }
    public Deck()
    {
        deckName = "New Deck";
        deck = new List<Card>();
        cardLimits = new Dictionary<string, int>();
    }
    public Deck(Deck deckToClone)
    {
        deckName = deckToClone.deckName;
        deckFormat = deckToClone.deckFormat;
        limitCardAmount = deckToClone.limitCardAmount;
        deck = new List<Card>();
        cardLimits = new Dictionary<string, int>(deckToClone.cardLimits);
        foreach (Card card in deckToClone.deck)
        {
            AddCard(card.Clone());
        }
    }
    public DeckFormat GetFormat() => this.deckFormat;
    public string GetDeckName() => this.deckName;
    protected virtual void Awake()
    {
    }

    public virtual bool IsValidForPlay()
    {
        return true;
    }
    public List<Card> GetDeck()
    {
        return deck;
    }

    public void SetDeck(List<Card> deck)
    {
        this.deck = deck;
    }

    public Dictionary<string, int> GetDictionary()
    {
        return cardLimits;
    }

    public Card GetCardByName(string cardName)
    {
        return deck.Find(d => d.cardName == cardName);
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

    public Card GetCard(int index) { return deck[index]; }

    public Card GetLastCard()
    {
        if (deck.Count == 0)
        {
            Debug.Log("IA Deck is empty");
            return null;
        }
        return GetCard(deck.Count - 1);
    }

    protected virtual bool IsFull()
    {
        if (deck==null)
        {
            Debug.Log("Deck is null");
            return false;
        }
        return !(deck.Count<limitCardAmount);
    }
    
    public virtual void RemoveCard(Card cardToRemove) {
        deck.Remove(cardToRemove);
    }

    public virtual void RemoveLastCard()
    {
        deck.RemoveAt(deck.Count-1);
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
