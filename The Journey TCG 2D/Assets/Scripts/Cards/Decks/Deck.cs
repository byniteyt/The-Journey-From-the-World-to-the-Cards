using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[Serializable]
public abstract class Deck
{
    public string deckName;

    public DeckFormat deckFormat;

    public List<BattleCard> deck;
    
    protected int limitCardAmount;

    protected Dictionary<string, int> cardLimits;

    protected void OnCreate()
    {
        deckName = "New Deck";
        deck = new List<BattleCard>();
        cardLimits = new Dictionary<string, int>();
    }
    public Deck()
    {
        Debug.Log("Deck constructor called");
        deckName = "New Deck";
        deck = new List<BattleCard>();
        cardLimits = new Dictionary<string, int>();
    }
    public Deck(Deck deckToClone)
    {
        deckName = deckToClone.deckName;
        deckFormat = deckToClone.deckFormat;
        limitCardAmount = deckToClone.limitCardAmount;
        deck = new List<BattleCard>();
        cardLimits = new Dictionary<string, int>(deckToClone.cardLimits);
        foreach (BattleCard card in deckToClone.deck)
        {
            AddCard(card);
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
    public List<BattleCard> GetDeck()
    {
        return deck;
    }

    public void SetDeck(List<BattleCard> deck)
    {
        this.deck = deck;
    }

    public Dictionary<string, int> GetDictionary()
    {
        return cardLimits;
    }

    public BattleCard GetCardByName(string cardName)
    {
        return deck.Find(d => d.GetCard().cardName == cardName);
    }

    public int GetCardLimit(BattleCard card)
    {
        if (cardLimits.ContainsKey(card.name))
        {
            return cardLimits[card.name];
        }
        Debug.Log("Card limit not found for card: " + card.name);
        return 0;
    }

    public abstract void AddCard(BattleCard cardToAdd);

    public BattleCard GetCard(int index) { return deck[index]; }

    public BattleCard GetLastCard()
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

    public virtual void RemoveCard(BattleCard cardToRemove) {
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
