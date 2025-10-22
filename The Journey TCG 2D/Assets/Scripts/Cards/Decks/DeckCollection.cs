using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public static class DeckCollection 
{
    static List<Deck> decks = new List<Deck>();

    public static int DecksAmount()
    {
        return decks.Count;
    }
    public static Deck GetDeck(int index)
    {
        return decks[index];
    }
    public static int GetIndexOfDeck(Deck deck)
    {
        return decks.IndexOf(deck);
    }
    public static void AddDeck(Deck newDeck)
    {
        decks.Add(newDeck);
    }
    public static void RemoveDeck(Deck deckToRemove)
    {
        decks.Remove(deckToRemove);
    }
    public static List<Deck> SavedDecks() { return decks; }
}
