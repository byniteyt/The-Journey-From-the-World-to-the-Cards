using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Callbacks;
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

    private List<Deck> decks = new();

    public List<StandardDeck> StandardDecks = new();

    public List<WildDeck> WildDecks = new();

    public List<DeckData> deckDataList = new();

    public List<CardsReceived> cardsReceived = new List<CardsReceived>();

    public List<Deck> decksReceived = new List<Deck>();

    #region Getters
    public Dictionary<string, int> GetCards()
    {
        if (properties.cards == null)
        {
            properties.cards = new();
        }
        return properties.cards;
    }

    public BattleCard GetCard(string name)
    {
        if (string.IsNullOrEmpty(name))
            return null;

        var cardEntry = properties.cardsReceived.Find(c =>
            c.card != null &&
            c.card.GetCard() != null &&
            c.card.GetCard().cardName == name
        );

        return cardEntry != null ? cardEntry.card : null;
    }


    public List<Deck> GetDecks()
    {
        if (properties.decks == null)
        {
            Debug.Log("Loading saved decks for the player.");
            properties.decks = DeckCollection.SavedDecks();
        }
        return properties.decks;
    }
    public List<StandardDeck> GetStandardDecks()
    {
        return StandardDecks;
    }
    public List<WildDeck> GetWildDecks()
    {
        return WildDecks;
    }
    public int GetIndexOfDeck(Deck deck)
    {
        return properties.decks.IndexOf(deck);
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
            properties.cards = new();
            properties.cardsReceived = new();
            properties.decks = new();
            CardDataBase.GetDataBase().LoadCardsFromFolder("Card", () =>
            {
                foreach (var card in Player.GetPlayer().Properties().cardsReceived)
                {
                    if (card.card == null)
                    {
                        Debug.LogWarning("La carta recibida es nula"); continue;
                    }
                    if (card.card.GetCard() == null)
                    {
                        Debug.LogWarning("No hay carta"); continue;
                    }
                    if (card.card.GetCard().cardName == null)
                    {
                        Debug.LogWarning("No hay nombre de carta"); continue;
                    }
                    Debug.Log($"Card received: {card.card.GetCard().cardName}, Amount: {card.amount}");
                }
                Debug.Log("Todas las cartas de Durnei cargadas");
            });
            SaveData<PlayerProperties>.SerializeJSON(properties, "PlayerProperties.json");
        }
        else
        {
            PlayerProperties props = SaveData<PlayerProperties>.DeserializeJSON("PlayerProperties.json");
            props.decks = new List<Deck>();

            foreach (var data in props.deckDataList)
            {
                Deck newDeck = data.deckFormat switch
                {
                    DeckFormat.Standard => new StandardDeck(),
                    DeckFormat.Wild => new WildDeck(),
                    _ => throw new Exception("Unknown deck type: " + data.deckFormat)
                };

                newDeck.SetDeckName(data.deckName);
                newDeck.deckFormat = data.deckFormat;

                foreach (var cardName in data.cardNames)
                {
                    var card = CardDataBase.GetDataBase().GetCard(cardName);
                    newDeck.AddCard(card.GetComponent<BattleCard>());
                }

                props.decks.Add(newDeck);
            }

            properties.cards = props.cards;
            Debug.Log("Cards loaded: " + properties.cards.Count);
            properties.cardsReceived = props.cardsReceived;
            Debug.Log("CardsReceived loaded: " + properties.cardsReceived.Count);
            properties.decks = props.decks;
            Debug.Log("Decks loaded: " + properties.decks.Count);
            properties.decksReceived = props.decksReceived;
            Debug.Log("DecksReceived loaded: " + properties.decksReceived.Count);
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
        deckDataList = decks.Select(d => new DeckData
        {
            deckName = d.GetDeckName(),
            deckFormat = d.GetFormat(),
            cardNames = d.GetDeck().Select(c => c.GetCard().cardName).ToList()
        }).ToList();

        SaveData<PlayerProperties>.SerializeJSON(Player.GetPlayer().Properties(), "PlayerProperties.json");
    }

    public void AddDeck(Deck deckToAdd)
    {
        Player.GetPlayer().Properties().decks.Add(deckToAdd);
        if (deckToAdd.deckFormat == DeckFormat.Standard)
        {
            StandardDecks.Add((StandardDeck)deckToAdd);
        }
        else if (deckToAdd.deckFormat == DeckFormat.Wild)
        {
            WildDecks.Add((WildDeck)deckToAdd);
        }
        UpdateProperties();
    }

    public void RemoveDeck(Deck deckToRemove)
    {
        Player.GetPlayer().Properties().decks.Remove(deckToRemove);
        Player.GetPlayer().Properties().decksReceived.RemoveAll(d => d.deckName == deckToRemove.GetDeckName());
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
