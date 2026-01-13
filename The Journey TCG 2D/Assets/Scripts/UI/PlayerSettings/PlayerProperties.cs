using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]

public class PlayerProperties
{
    [Serializable] public
    class CardsReceived
    {
        public BattleCard card;
        public int amount;
    }
    
    public static PlayerProperties properties = new PlayerProperties();

    private Dictionary<string, int> cards = new Dictionary<string, int>();

    public List<Deck> decks = new List<Deck>();

    public List<CardsReceived> cardsReceived = new List<CardsReceived>();

    public List<Deck> decksReceived = new List<Deck>();

    #region Getters
    public Dictionary<string, int> GetCards()
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
            properties.cards = new Dictionary<string, int>
            {
            };
            properties.cardsReceived = new List<CardsReceived>
            {
            };
            properties.decks = new List<Deck>
            {
            };
            SaveData<PlayerProperties>.SerializeJSON(properties, "PlayerProperties.json");
            Debug.LogWarning("PlayerProperties file not found. Created new PlayerProperties.json");
        }
        else
        {
            PlayerProperties props = SaveData<PlayerProperties>.DeserializeJSON("PlayerProperties.json");
            properties.cards = props.cards;
            properties.cardsReceived = props.cardsReceived;
            properties.decks = props.decks;
            properties.decksReceived = props.decksReceived;
            Debug.LogWarning("PlayerProperties loaded.");
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
        SaveData<PlayerProperties>.SerializeJSON(Player.GetPlayer().Properties(), "PlayerProperties.json");
    }

    public void AddDeck(Deck deckToAdd)
    {
        Player.GetPlayer().Properties().decks.Add(deckToAdd);
        /*properties.decksReceived.Add(new DeckInfo
        {
            deckName = deckToAdd.GetDeckName(),
            format = deckToAdd.GetFormat(),
            deckcards = new List<CardsReceived>()
        });*/
        UpdateProperties();
    }

    public void RemoveDeck(Deck deckToRemove)
    {
        properties.decks.Remove(deckToRemove);
        properties.decksReceived.RemoveAll(d => d.deckName == deckToRemove.GetDeckName());
        UpdateProperties();
    }

    /*public void UpdateDeckInfo(Deck deckToUpdate)
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
    }*/

    public void AddCard(BattleCard cardToAdd, int amount)
    {
        if (Player.GetPlayer().Properties().cards.ContainsKey(cardToAdd.GetCard().cardName))
        {
            Player.GetPlayer().Properties().cards[cardToAdd.GetCard().cardName] += amount;
            Debug.Log("Increased amount of card: " + cardToAdd.GetCard().cardName + " by " + amount);
        }
        else
        {
            Player.GetPlayer().Properties().cards[cardToAdd.GetCard().cardName] = amount;
            Debug.Log("Added new card: " + cardToAdd.GetCard().cardName + " with amount " + amount);
        }
        Debug.Log("Added " + amount + " of card: " + cardToAdd.GetCard().cardName);
        var existingCard = Player.GetPlayer().Properties().cardsReceived.Find(c => c.card.GetCard().cardName == cardToAdd.GetCard().cardName);
        if (existingCard != null)
        {
            Debug.Log("Card already exists in cardsReceived. Increasing amount by " + amount);
            existingCard.amount += amount;
        }
        else
        {
            Debug.Log("Card does not exist in cardsReceived. Adding new entry.");
            Player.GetPlayer().Properties().cardsReceived.Add(new CardsReceived { card = cardToAdd, amount = amount });
        }
        UpdateProperties();
    }
}
