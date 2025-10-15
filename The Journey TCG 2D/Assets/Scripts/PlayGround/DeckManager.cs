using System.Runtime.CompilerServices;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public Card[] deck = new Card[30];
    int deckCount;
    public int DeckCount { get { return deckCount; } private set { deckCount = value; } }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deckCount = deck.Length;
        Debug.Log("Deck Count: " + deckCount);
        //DrawCard(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DrawCard(int amount)
    {
        if(amount>DeckCount) amount = deckCount;
        for (int i = 0; i<amount;i++)
        {
            if (Hand.Instance.AddCard(deck[deckCount - 1]))
            {
                deck[deckCount-1] = null;
                deckCount--;
            }  
        }
    }
}
