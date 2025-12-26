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
    [Serializable]
    public struct DeckInfo 
    { 
        public string deckName;
        public DeckFormat format;
        public List<CardsReceived> deckcards;
    }
    public static PlayerProperties properties = new PlayerProperties();

    private Dictionary<Card, int> cards;

    public List<Deck> decks;

    public List<CardsReceived> cardsReceived;

    public List<DeckInfo> decksReceived;

    #region Getters
    public Dictionary<Card, int> GetCards()
    {
        if (properties.cards == null)
        {
            properties.cards = CardCollection.GetCollection();
        }
        return properties.cards;
    }

    public List<Deck> GetDecks()
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
            properties.decks = new List<Deck>
            {
                new StandardDeck()
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

    public void AddDeck(Deck deckToAdd)
    {
        properties.decks.Add(deckToAdd);
        properties.decksReceived.Add(new DeckInfo
        {
            deckName = deckToAdd.GetDeckName(),
            format = deckToAdd.GetFormat(),
            deckcards = new List<CardsReceived>()
        });
        UpdateProperties();
    }

    public void RemoveDeck(Deck deckToRemove)
    {
        properties.decks.Remove(deckToRemove);
        properties.decksReceived.RemoveAll(d => d.deckName == deckToRemove.GetDeckName());
        UpdateProperties();
    }

    public void UpdateDeckInfo(Deck deckToUpdate)
    {
        var deckInfo = properties.decksReceived.Find(d => d.deckName == deckToUpdate.GetDeckName());
        if (deckInfo.deckName != null)
        {
            deckInfo.format = deckToUpdate.GetFormat();
            deckInfo.deckcards.Clear();
            foreach (var cardEntry in deckToUpdate.GetDictionary())
            {
                Card newCard = deckToUpdate.GetCard(cardEntry.Value).GetCard();
                deckInfo.deckcards.Add(new CardsReceived { card = newCard, amount = cardEntry.Value });
            }
            UpdateProperties();
        }
    }

    public void AddCard(Card cardToAdd, int amount)
    {
        if (properties.cards.ContainsKey(cardToAdd))
        {
            properties.cards[cardToAdd] += amount;
        }
        else
        {
            properties.cards[cardToAdd] = amount;
        }
        var existingCard = properties.cardsReceived.Find(c => c.card.cardName == cardToAdd.cardName);
        if (existingCard.card != null)
        {
            existingCard.amount += amount;
        }
        else
        {
            properties.cardsReceived.Add(new CardsReceived { card = cardToAdd, amount = amount });
        }
        UpdateProperties();
    }
}
