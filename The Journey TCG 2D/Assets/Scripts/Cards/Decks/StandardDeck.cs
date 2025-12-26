using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class StandardDeck : Deck
{
    public StandardDeck() : base()
    {
        deckFormat = DeckFormat.Standard;
        limitCardAmount = 100;
        base.Awake();
    }
    public StandardDeck(Deck deckToClone) : base(deckToClone)
    {

    }
    public override void AddCard(BattleCard cardToAdd)
    {
        if (IsFull())
        {
            Debug.Log("This Deck is full. Cannot add more cards.");
            return;
        }
        BattleCard cardToAddAsBattleCard = null;
        switch (cardToAdd.GetType().Name)
        {
            case "BattleCharCard":
                cardToAddAsBattleCard = new BattleCharCard();
                break;
            case "BattleSpellCard":
                cardToAddAsBattleCard = new BattleSpellCard();
                break;
            case "BattleRoomCard":
                cardToAddAsBattleCard = new BattleRoomCard();
                break;
            // Add other Card types here as needed
            default:
                Debug.LogError("Unsupported Card type.");
                return;
        }
        if (cardLimits.ContainsKey(cardToAdd.GetCard().cardName))
        {
            if (cardLimits[cardToAdd.GetCard().cardName] < 8)
            {

                Debug.Log($"Added {cardToAdd.GetCard().cardName} to the Deck.");
                deck.Add(cardToAddAsBattleCard);
                cardLimits[cardToAdd.GetCard().cardName]++;
            }
            else
            {
                Debug.Log($"Cannot add more copies of {cardToAdd.GetCard().cardName} to this Deck.");
            }
            return;
        }
        deck.Add(cardToAddAsBattleCard);
        cardLimits.Add(cardToAdd.GetCard().cardName, 1);
    }

    public override void RemoveCard(BattleCard cardToRemove)
    {
        base.RemoveCard(cardToRemove);
        cardLimits[cardToRemove.name]--;
        if (cardLimits[cardToRemove.name] == 0)
        {
            cardLimits.Remove(cardToRemove.name);
        }
    }

    public override bool IsValidForPlay()
    {
        //return (deck.Count >= 40 && deck.Count <= 100);
        return true;
    }
}
