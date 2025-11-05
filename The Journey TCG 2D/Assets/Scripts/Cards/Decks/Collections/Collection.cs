using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "AlbumCollection", menuName = "Collections/AlbumCollection")]
public class Collection : ScriptableObject
{
    [SerializeField] private CollectionName collectionName;
    [SerializeField] private Sprite collectionIcon;
    [SerializeField] List<Card> cardsOfCollection;
    Dictionary<Card, Card> cardDictionary;

    
    public List<Card> GetCollectionCards()
    {
        return cardsOfCollection;
    }

    public Dictionary<Card, Card> GetCardList()
    {
        return cardDictionary;
    }
}
