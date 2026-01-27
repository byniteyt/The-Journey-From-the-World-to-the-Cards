using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardAmount
{
    public string cardName;
    public int amount;
}

public class CardCollection
{
    public List<CardAmount> GetCardCollection()
    {
        return Player.GetPlayer().Properties().GetCards();
    }
    public void AddCard(Card cardToAdd)
    {
        if (cardToAdd == null)
        {
            Debug.Log("La carta a añadir es nula.");
            return;
        }
        var existingCard = Player.GetPlayer().Properties().GetCards().Find(c => c.cardName == cardToAdd.cardName);
        if (existingCard != null)
        {
            existingCard.amount++;
        }
        else
        {
            Player.GetPlayer().Properties().GetCards().Add(new CardAmount { cardName = cardToAdd.cardName, amount = 1 });
        }
    }

    public Card GetCard(string id)
    {
        Card cardData = CardDataBase.Instance
            .GetObjectCard(id)
            .GetComponent<BattleCard>()
            .GetCard();
        return cardData;
    }
}
