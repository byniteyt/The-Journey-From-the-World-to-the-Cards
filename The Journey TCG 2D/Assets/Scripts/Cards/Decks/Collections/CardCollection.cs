using System.Collections.Generic;
using UnityEngine;

public class CardCollection  
{
    [SerializeField] static Dictionary<Card, int> cardDictionary;
    public Dictionary<Card, int> GetCardCollection()
    {
        if (cardDictionary == null) cardDictionary = new Dictionary<Card, int>();
        return cardDictionary;
    }
    public static void AddCard(Card cardToAdd)
    {
        if (cardDictionary == null)
        {
            cardDictionary = Player.player.Properties().GetCards();
        }

        if (cardToAdd == null)
        {
            Debug.Log("La carta a añadir es nula.");
            return;
        }
        if (cardDictionary.ContainsKey(cardToAdd))
        {
            cardDictionary[cardToAdd]++;
        }
        else
        {
            cardDictionary.Add(cardToAdd, 1);
        }
        Player.player.Properties().AddCard(cardToAdd, 1);
        Debug.Log($"Added card: {cardToAdd.cardName}. Total count: {cardDictionary[cardToAdd]}");
    }
    public static Dictionary<Card, int> GetCollection()
    {
        if (cardDictionary == null)
        {
            cardDictionary = Player.player.Properties().GetCards();
        }
        return cardDictionary;
    }
}
