using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AlbumCollection", menuName = "Collections/AlbumCollection")]
public class Collection : ScriptableObject
{
    [SerializeField] private CollectionName collectionName;
    [SerializeField] private Sprite collectionIcon;
    public List<Card> cardsOfCollection;
    public Dictionary<Card, Card> cardDictionary = new Dictionary<Card, Card>();
}
