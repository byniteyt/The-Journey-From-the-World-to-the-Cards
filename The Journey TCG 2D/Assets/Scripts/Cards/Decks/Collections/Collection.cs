using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AlbumCollection", menuName = "Collections/AlbumCollection")]
public class Collection : ScriptableObject
{
    public CollectionName collectionName;
    public Sprite collectionIcon;
    public List<BattleCard> singleCardsOfCollection;
    public List<BattleCard> specialCardsOfCollection;
    private readonly Dictionary<Card, Card> cardDictionary;

    public Dictionary<Card, Card> GetCardList()
    {
        return cardDictionary;
    }
    public BattleCard GetRandomCard()
    {
        int randomIndex = Random.Range(0, singleCardsOfCollection.Count);
        BattleCard newCard = new();
        newCard.SetCard(singleCardsOfCollection[randomIndex].GetCard());
        return newCard;
    }
}
