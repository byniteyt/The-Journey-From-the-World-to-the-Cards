using System.Collections.Generic;
using UnityEngine;

public static class CardCollection  
{
    [SerializeField] static Dictionary<Card, int> cardDictionary = new Dictionary<Card, int>();
    
    public static void AddCard(Card cardToAdd)
    {
        if (cardDictionary.ContainsKey(cardToAdd))
        {
            cardDictionary[cardToAdd]++;
        }
        else
        {
            cardDictionary.Add(cardToAdd, 1);
        }
        Debug.Log($"Added card: {cardToAdd.cardName}. Total count: {cardDictionary[cardToAdd]}");
    }
    public static Dictionary<Card, int> GetCollection()
    {
        return cardDictionary;
    }
}
