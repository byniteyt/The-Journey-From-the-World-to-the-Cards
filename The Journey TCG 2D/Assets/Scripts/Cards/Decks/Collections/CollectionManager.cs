using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    CollectionData collectionData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(Collection collection in collectionData.collections)
        {
            foreach(Card card in collection.GetSingleCards())
            {
                if(!collection.GetCardList().ContainsKey(card))
                {
                    collection.GetCardList().Add(card, card);
                }
                else
                {
                    collection.GetSingleCards().Remove(card);
                }
            }
            foreach (Card card in collection.GetSpecialCards())
            {
                if (!collection.GetCardList().ContainsKey(card)) 
                {
                    collection.GetCardList().Add(card, card);
                }
                else
                {
                    collection.GetSpecialCards().Remove(card);
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
