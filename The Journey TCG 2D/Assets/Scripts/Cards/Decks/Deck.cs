using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Deck : MonoBehaviour
{
    protected string deckName;

    protected DeckFormat deckFormat;

    protected List<Card> deck;

    protected int limitCardAmount;

    protected Dictionary<string, int> cardLimits;

    protected void OnCreate()
    {
        this.deckName = "New Deck";
    }
    public DeckFormat GetFormat() => this.deckFormat;

    protected virtual void AddCard(Card cardToAdd)
    {
        if (IsFull())
        {
            Debug.Log("Deck is full. Cannot add more cards.");
            return;
        }
        deck.Add(cardToAdd);
    }
     protected virtual bool IsFull()
    {
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
