using System.Collections.Generic;
using UnityEngine;

public class CardCollection  
{
    [SerializeField] static Dictionary<string, int> cardDictionary;
    public Dictionary<string, int> GetCardCollection()
    {
        cardDictionary ??= new Dictionary<string, int>();
        return cardDictionary;
    }
    public static void AddCard(BattleCard cardToAdd)
    {
        cardDictionary ??= Player.GetPlayer().Properties().GetCards();

        if (cardToAdd == null)
        {
            Debug.Log("La carta a añadir es nula.");
            return;
        }
        if (cardDictionary.ContainsKey(cardToAdd.GetCard().cardName))
        {
            cardDictionary[cardToAdd.GetCard().cardName]++;
        }
        else
        {
            cardDictionary.Add(cardToAdd.GetCard().cardName, 1);
        }
        Player.GetPlayer().Properties().AddCard(cardToAdd, 1);
        Debug.Log($"Added card: {cardToAdd.GetCard().cardName}. Total count: {cardDictionary[cardToAdd.GetCard().cardName]}");
    }
    public static Dictionary<string, int> GetCollection()
    {
        cardDictionary ??= Player.GetPlayer().Properties().GetCards();
        return cardDictionary;
    }
    public static Card GetCard(string id)
    {
        Card cardData = CardDataBase.GetDataBase()
            .GetCard(id)
            .GetComponent<BattleCard>()
            .GetCard();
        return cardData;
    }
}
