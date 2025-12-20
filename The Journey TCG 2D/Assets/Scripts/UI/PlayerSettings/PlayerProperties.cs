using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]

public class PlayerProperties
{
    [Serializable] public
    struct CardsReceived
    {
        public Card card;
        public int amount;
    }
    public static PlayerProperties properties = new PlayerProperties();

    private Dictionary<Card, int> cards;

    public List<BattleDeck> decks;

    public List<CardsReceived> cardsReceived;

    #region Getters
    public Dictionary<Card, int> GetCards()
    {
        if (properties.cards == null)
        {
            properties.cards = CardCollection.GetCollection();
        }
        return properties.cards;
    }

    public List<BattleDeck> GetDecks()
    {
        if (properties.decks == null)
        {
            properties.decks = DeckCollection.SavedDecks();
        }
        return properties.decks;
    }
    /*
    public static List<Deck> GetOwnDecks()
    {
        if (properties.loadedDecks.decks == null)
        {
            InitializeOwnDecks();
        }
        return properties.loadedDecks.decks;
    }

    public static List<Deck> GetPrefabDecks()
    {
        if (properties.prefabDecks.prefabDecks == null)
        {
            InitializePrefabDecks();
        }
        return properties.prefabDecks.prefabDecks;
    }*/
    #endregion

    public void Initialize()
    {
        InitializePlayerProperties();
        /*InitializeOwnDecks();
        InitializePrefabDecks();*/

    }
    public static void InitializePlayerProperties()
    {
        if (!SaveData<PlayerProperties>.SaveDataExists("PlayerProperties.json"))
        {
            properties.cards = new Dictionary<Card, int>
            {
                { new Card(), 77 } // Placeholder card
            };
            properties.cardsReceived = new List<CardsReceived>
            {
                new CardsReceived { card = new Card(), amount = 77 } // Placeholder card
            };
            properties.decks = new List<BattleDeck>
            {
                new StandardBattleDeck()
            };
            SaveData<PlayerProperties>.SerializeJSON(properties, "PlayerProperties.json");
            Debug.Log("PlayerProperties file not found. Created new PlayerProperties.json");
        }
        else
        {
            properties = SaveData<PlayerProperties>.DeserializeJSON("PlayerProperties.json");
            Debug.Log("PlayerProperties loaded.");
        }
    }
    public static void InitializePrefabDecks()
    {

    }
    public static void InitializeOwnDecks()
    {
        /*properties.GetCards();
        if (!SaveData<OwnDecks>.SaveDataExists("OwnDecks.json"))
        {
            SaveData<OwnDecks>.SerializeJSON(properties.loadedDecks, "OwnDecks.json");
            Debug.Log("OwnDecks file not found. Created new OwnDecks.json");
        }
        else
        {
            SaveData<OwnDecks>.DeserializeJSON(properties.loadedDecks,"OwnDecks.json");
            Debug.Log("OwnDecks loaded. Decks count: " + properties.loadedDecks.decks.Count);
            // Here you can assign loadedDecks.decks to a static variable if needed
        }*/
    }

    public void UpdateProperties()
    {
        SaveData<PlayerProperties>.SerializeJSON(properties, "PlayerProperties.json");
    }

    public void AddDeck(BattleDeck deckToAdd)
    {
        properties.decks.Add(deckToAdd);
    }

    public void RemoveDeck(BattleDeck deckToRemove)
    {
        properties.decks.Remove(deckToRemove);
        UpdateProperties();
    }
}
