using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class StandardDeck : Deck
{
    protected override void OnInit()
    {
        deckFormat = DeckFormat.Standard;
        limitCardAmount = 10;
        InitLimits();   
    }
    public StandardDeck() : base()
    {
        deckFormat = DeckFormat.Standard;
        limitCardAmount = 100;
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
        if (cardToAdd == null)
        {
            Debug.LogError("Cannot add a null card to the Deck.");
            return;
        }
        Card cardOfBattleCard = cardToAdd.GetCard();
        /*
        switch (cardToAdd.GetType().Name)
        {
            case "BattleCharCard":
                BattleCharCard battleCharCard = new((CharacterCard)cardToAdd.GetCard());
                cardToAdd = battleCharCard;
                cardOfBattleCard = cardToAdd.GetCharacter();
                break;
            case "BattleSpellCard":
                BattleSpellCard battleSpellCard = new((SpellCard)cardToAdd.GetCard());
                cardToAdd = battleSpellCard;
                cardOfBattleCard = cardToAdd.GetSpell();
                break;
            case "BattleRoomCard":
                BattleRoomCard battleRoomCard = new((RoomCard)cardToAdd.GetCard());
                cardToAdd = battleRoomCard;
                cardOfBattleCard = cardToAdd.GetRoom();
                break;
            // Add other Card types here as needed
            default:
                Debug.LogError("Unsupported Card type.");
                return;
        }
        */
        if (cardOfBattleCard == null)
        {
            Debug.LogError($"BattleCard.GetCard() es NULL para {cardToAdd.name}");
            return;
        }
        AddCardToDictionary(cardToAdd);
        deck.Add(cardToAdd);
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

    private void InitLimits()
    {
        cardLimits = new Dictionary<string, int>();
        foreach (BattleCard card in deck)
        {
            if (cardLimits.ContainsKey(card.GetCard().cardName))
            {
                cardLimits[card.GetCard().cardName]++;
            }
            else
            {
                cardLimits.Add(card.GetCard().cardName, 1);
            }
        }
    }
}
