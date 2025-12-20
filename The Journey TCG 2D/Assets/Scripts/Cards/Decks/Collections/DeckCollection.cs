using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class DeckCollection 
{
    static List<BattleDeck> decks = new List<BattleDeck>();

    public static int DecksAmount()
    {
        return decks.Count;
    }

    ////////// Deck Getters and Setters /////////
   
    public static BattleDeck GetDeck(int index)
    {
        //return new Deck(decks[index]);
        return decks[index];
    }

    public static BattleDeck GetDeck(BattleDeck deckToGet)
    {
        //return new Deck(deckToGet);
        //return decks[GetIndexOfDeck(deckToGet)];
        return deckToGet;
    }

    public static int GetIndexOfDeck(BattleDeck deck)
    {
        return decks.IndexOf(deck);
    }

    public static BattleDeck GetLastDeck()
    {
        if (decks.Count == 0)
        {
            return null;
        }
        return decks[decks.Count - 1];
    }

    public static void SetDeck(int index, BattleDeck deckToChange)
    {
        decks[index] = deckToChange;
    }

    public static void SetDeck(BattleDeck deckToGet, BattleDeck deckToChange)
    {
        decks[GetIndexOfDeck(deckToGet)] = deckToChange;
    }

    public static void SetIndexOfDeck(BattleDeck oldDeck, int deckPos)
    {
        if (deckPos<0||deckPos>=decks.Count)
        {
            return;
        }
        BattleDeck temp = decks[deckPos];
        decks[deckPos] = oldDeck;
        decks[decks.IndexOf(oldDeck)] = temp;
    }


    public static void AddDeck(BattleDeck newDeck)
    {
        decks.Add(newDeck);
        
    }
    public static void RemoveDeck(BattleDeck deckToRemove)
    {
        decks.Remove(deckToRemove);
    }
    public static List<BattleDeck> SavedDecks() { return decks; }
}
