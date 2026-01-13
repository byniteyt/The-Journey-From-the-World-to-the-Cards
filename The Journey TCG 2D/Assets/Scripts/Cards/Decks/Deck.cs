using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;


public abstract class Deck
{


    public string deckName;

    [HideInInspector] public DeckFormat deckFormat;

    public List<BattleCard> deck;
    
    protected int limitCardAmount;

    protected Dictionary<string, int> cardLimits;
    #region Serialization
    public void OnAfterDeserialize()
    {
        deck ??= new List<BattleCard>();

        cardLimits ??= new Dictionary<string, int>();

        OnInit();
    }

    public void OnBeforeSerialize() { }

    protected virtual void OnInit() { }

    #endregion

    #region Constructors
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
    #endregion

    #region Getters
    public DeckFormat GetFormat() => this.deckFormat;
    public string GetDeckName() => this.deckName;
    public List<BattleCard> GetDeck()
    {
        return deck;
    }


    public Dictionary<string, int> GetDictionary()
    {
        return cardLimits;
    }

    public BattleCard GetCardByName(string cardName)
    {
        BattleCard card = deck.Find(d => d.GetCard().cardName == cardName);
        if (card == null)
        {
            Debug.Log("Card not found: " + cardName);
            return null;
        }
        return card;
    }

    public int GetCardLimit(BattleCard card)
    {
        if (card == null)
        {
            Debug.Log("Card is null");
            return 0;
        }
        if (cardLimits.ContainsKey(card.name))
        {
            return cardLimits[card.name];
        }
        Debug.Log("Card limit not found for card: " + card.name);
        return 1;
    }

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

    public int GetMaxLimit()
    {
        return limitCardAmount;
    }
    #endregion
    protected void OnCreate()
    {
        deckName = "New Deck";
        deck = new List<BattleCard>();
        cardLimits = new Dictionary<string, int>();
    }
    protected virtual void Awake()
    {
    }

    public virtual bool IsValidForPlay()
    {
        return true;
    }

    public void SetDeck(List<BattleCard> deck)
    {
        this.deck = deck;
    }

    public abstract void AddCard(BattleCard cardToAdd);

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

    public void AddCardToDictionary(BattleCard cardToAdd)
    {
        if (cardToAdd == null) return;

        string key = cardToAdd.name;

        if (!cardLimits.ContainsKey(key))
        {
            cardLimits[key] = 1;
            Debug.Log($"Added {cardToAdd.GetCard().cardName} to the Deck.");
        }
        else if (cardLimits[key] < GetMaxLimit())
        {
            cardLimits[key]++;
            Debug.Log($"Added {cardToAdd.GetCard().cardName} to the Deck. Total: {cardLimits[key]}");
        }
        else
        {
            Debug.LogWarning($"Cannot add more cards of this type: {cardToAdd.GetCard().cardName}");
        }
    }
}
