using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Deck
{

    protected int limitPerCard;
    public string deckName;

    [HideInInspector] public DeckFormat deckFormat;

    [SerializeField] protected List<Card> deck = new();
    
    protected int limitCardAmount;

    protected Dictionary<string, int> cardLimits = new();
    #region Serialization
    public void OnAfterDeserialize()
    {
        deck ??= new List<Card>();

        cardLimits ??= new Dictionary<string, int>();

        OnInit();
    }

    public void OnBeforeSerialize() { }

    protected virtual void OnInit() { }

    #endregion

    #region Constructors
    public Deck()
    {
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
            AddCard(card);
        }
    }
    #endregion

    #region Getters
    public DeckFormat GetFormat() => this.deckFormat;
    public string GetDeckName() => this.deckName;
    public List<Card> GetDeck()
    {
        return deck;
    }


    public Dictionary<string, int> GetDictionary()
    {
        return cardLimits;
    }

    public Card GetCardByName(string cardName)
    {
        return deck.FirstOrDefault(c => c.cardName == cardName);
    }

    public int GetCardLimit(BattleCard card)
    {
        if (card == null || card.GetCard() == null) return 0;

        string key = card.GetCard().cardName;
        return cardLimits.ContainsKey(key) ? cardLimits[key] : 0;
    }


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

    public int GetMaxLimit()
    {
        return limitCardAmount;
    }
    #endregion

    public virtual bool IsValidForPlay()
    {
        return true;
    }

    public void SetDeck(List<Card> deck)
    {
        this.deck = deck;
    }

    public virtual void AddCard(Card cardToAdd) { }

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

    public void AddCardToDictionary(Card cardToAdd)
    {
        if (cardToAdd == null)
        {
            Debug.LogWarning("La carta a introducir es nula.");
            return;
        }

        string key = cardToAdd.cardName;

        if (!cardLimits.ContainsKey(key))
            cardLimits[key] = 0;

        if (cardLimits[key] >= limitPerCard)
        {
            Debug.LogWarning($"Cannot add more copies of {key}");
            return;
        }
        Debug.Log($"Adding card {key} to the deck."); 
        deck.Add(cardToAdd);
        cardLimits[key]++;
    }
}
