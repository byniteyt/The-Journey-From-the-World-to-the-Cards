using System.Collections.Generic;
using UnityEngine;

public class CardCollection : MonoBehaviour
{
    Dictionary<Card, int> cardDictionary = new Dictionary<Card, int>();
    
    void AddCard(Card cardToAdd)
    {
        if (cardDictionary.ContainsKey(cardToAdd))
        {
            cardDictionary[cardToAdd]++;
        }
        else
        {
            cardDictionary.Add(cardToAdd, 1);
        }
    }
}
