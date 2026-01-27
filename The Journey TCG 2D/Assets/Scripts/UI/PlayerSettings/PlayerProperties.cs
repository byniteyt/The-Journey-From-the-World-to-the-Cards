using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class PlayerProperties
{
    public static PlayerProperties properties = new PlayerProperties();

    public List<CardAmount> cards = new();


    private List<Deck> decks = new();

    public List<DeckData> deckDataList = new();

    #region Getters
    public List<CardAmount> GetCards()
    {
        if (cards.Count == 0)
        {
            Debug.Log("No cards found in player properties. Initializing empty card list.");
            cards = new List<CardAmount>();
        }
        return cards;
    }


    public Card GetCard(string name)
    {
        if (string.IsNullOrEmpty(name))
            return null;

        var cardEntry = CardDataBase.Instance.GetCard(name);

        return cardEntry != null ? cardEntry : null;
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
        return (List<StandardDeck>)decks.Where(d => d.deckFormat == DeckFormat.Standard);
    }
    public List<WildDeck> GetWildDecks()
    {
        return (List<WildDeck>)decks.Where(d => d.deckFormat == DeckFormat.Wild);
    }
    public int GetIndexOfDeck(Deck deck)
    {
        return properties.decks.IndexOf(deck);
    }
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
            foreach (var card in CardDataBase.Instance.GetAllCards())
            {
                properties.cards.Add(new CardAmount { cardName = card.GetComponent<BattleCard>().GetCard().cardName, amount = 1 });
            }
            properties.decks = new();
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
                    var card = CardDataBase.Instance.GetObjectCard(cardName);
                    if (card == null)
                    {
                        Debug.LogWarning("Card not found in database: " + cardName);
                        continue;
                    }
                    newDeck.AddCard(card.GetComponent<BattleCard>().GetCard());
                }

                props.decks.Add(newDeck);
            }

            properties.cards = props.cards;
            Debug.Log("Cards loaded: " + properties.cards.Count);
            properties.decks = props.decks;
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
            cardNames = d.GetDeck().Select(c => c.cardName).ToList()
        }).ToList();

        SaveData<PlayerProperties>.SerializeJSON(Player.GetPlayer().Properties(), "PlayerProperties.json");
    }

    public void AddDeck(Deck deckToAdd)
    {
        Player.GetPlayer().Properties().decks.Add(deckToAdd);
        
        UpdateProperties();
    }

    public void RemoveDeck(Deck deckToRemove)
    {
        Player.GetPlayer().Properties().decks.Remove(deckToRemove);
        UpdateProperties();
    }

    public void AddCard(Card cardToAdd, int amount)
    {
        if (cardToAdd == null || amount <= 0)
            return;
        var existingCard = cards.Find(c => c.cardName == cardToAdd.cardName);
        if (existingCard != null)
        {
            existingCard.amount += amount;
        }
        else
        {
            cards.Add(new CardAmount { cardName = cardToAdd.cardName, amount = amount });
        }
        Debug.Log("Added " + amount + " of card: " + cardToAdd.cardName);
        UpdateProperties();
    }
}
