using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    CollectionData collectionData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var collectionRef in collectionData.collections)
        {
            collectionRef.LoadAssetAsync<Collection>().Completed += handle =>
            {
                Collection collection = handle.Result;
                ProcesarCollection(collection);
            };
        }
    }

    void ProcesarCollection(Collection collection)
    {
        foreach (var card in collection.singleCardsOfCollection)
        {
            if (!collection.GetCardList().ContainsKey(card.GetCard()))
            {
                collection.GetCardList().Add(card.GetCard(), card.GetCard());
            }
        }

        foreach (var card in collection.specialCardsOfCollection)
        {
            if (!collection.GetCardList().ContainsKey(card.GetCard()))
            {
                collection.GetCardList().Add(card.GetCard(), card.GetCard());
            }
        }
    }

}
