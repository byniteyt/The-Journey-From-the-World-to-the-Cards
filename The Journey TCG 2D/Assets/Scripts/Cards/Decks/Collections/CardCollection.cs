using System.Collections.Generic;
using UnityEngine;

public class CardCollection  
{
    public List<CardAmount> GetCardCollection()
    {
        return Player.GetPlayer().Properties().GetCards();
    }
    public static void AddCard(Card cardToAdd)
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
    public static List<CardAmount> GetCollection()
    {
        return Player.GetPlayer().Properties().GetCards();
    }
    public static Card GetCard(string id)
    {
        Card cardData = CardDataBase.GetDataBase()
            .GetObjectCard(id)
            .GetComponent<BattleCard>()
            .GetCard();
        return cardData;
    }
}
