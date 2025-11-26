using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "AlbumCollection", menuName = "Collections/AlbumCollection")]
public class Collection : ScriptableObject
{
    [SerializeField] private CollectionName collectionName;
    [SerializeField] private Sprite collectionIcon;
    [SerializeField] List<Card> singleCardsOfCollection;
    [SerializeField] List<Card> specialCardsOfCollection;
    Dictionary<Card, Card> cardDictionary;

    
    public List<Card> GetSingleCards()
    {
        return singleCardsOfCollection;
    }
    public List<Card> GetSpecialCards()
    {
        return specialCardsOfCollection;
    }

    public Dictionary<Card, Card> GetCardList()
    {
        return cardDictionary;
    }
    public Card GetRandomCard()
    {
        int randomIndex = Random.Range(0, singleCardsOfCollection.Count);
        return singleCardsOfCollection[randomIndex];
    }
    public CollectionName GetCollectionName()
    {
        return collectionName;
    }
}
