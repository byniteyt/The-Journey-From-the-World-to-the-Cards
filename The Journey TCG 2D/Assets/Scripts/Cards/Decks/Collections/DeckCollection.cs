using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class DeckCollection 
{
    static List<Deck> decks = new List<Deck>();

    public static int DecksAmount()
    {
        return decks.Count;
    }

    ////////// Deck Getters and Setters /////////
   
    public static Deck GetDeck(int index)
    {
        //return new Deck(decks[index]);
        return decks[index];
    }

    public static Deck GetDeck(Deck deckToGet)
    {
        //return new Deck(deckToGet);
        //return decks[GetIndexOfDeck(deckToGet)];
        return deckToGet;
    }

    public static int GetIndexOfDeck(Deck deck)
    {
        return decks.IndexOf(deck);
    }

    public static Deck GetLastDeck()
    {
        if (decks.Count == 0)
        {
            return null;
        }
        return decks[decks.Count - 1];
    }

    public static void SetDeck(int index, Deck deckToChange)
    {
        decks[index] = deckToChange;
    }

    public static void SetDeck(Deck deckToGet, Deck deckToChange)
    {
        decks[GetIndexOfDeck(deckToGet)] = deckToChange;
    }

    public static void SetIndexOfDeck(Deck oldDeck, int deckPos)
    {
        if (deckPos<0||deckPos>=decks.Count)
        {
            return;
        }
        Deck temp = decks[deckPos];
        decks[deckPos] = oldDeck;
        decks[decks.IndexOf(oldDeck)] = temp;
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
