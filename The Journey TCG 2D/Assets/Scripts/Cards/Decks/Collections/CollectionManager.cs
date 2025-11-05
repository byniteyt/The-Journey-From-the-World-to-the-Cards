using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    CollectionData collectionData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(Collection collection in collectionData.collections)
        {
            foreach(Card card in collection.GetCollectionCards())
            {
                if(!collection.GetCardList().ContainsKey(card))
                {
                    collection.GetCardList().Add(card, card);
                }
                else
                {
                    collection.GetCollectionCards().Remove(card);
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
