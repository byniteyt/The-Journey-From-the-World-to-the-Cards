using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    CollectionData collectionData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        foreach(Collection collection in collectionData.collections)
        {
            foreach(Card card in collection.cardsOfCollection)
            {
                if(!collection.cardDictionary.ContainsKey(card))
                {
                    collection.cardDictionary.Add(card, card);
                }
                else
                {
                    collection.cardsOfCollection.Remove(card);
                }
            }
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
