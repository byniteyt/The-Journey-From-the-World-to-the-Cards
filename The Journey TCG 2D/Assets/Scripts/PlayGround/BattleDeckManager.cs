using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BattleDeckManager : MonoBehaviour
{
    public static BattleDeckManager Instance { get; private set; }
    //public Card[] deck;
    public Deck deck;
    int deckCount;
    public int DeckCount { get { return deckCount; } private set { deckCount = value; } }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        deck = DeckManager.selectedDeck;
        deckCount = deck.GetDeck().Count;
        Debug.Log($"Deck Count: {deckCount}");
        
        ShuffleDeck(deck.GetDeck());
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
            if (Hand.Instance.AddCard(deck.GetLastCard()))
            {
                deck.RemoveCard(deck.GetLastCard());
                deckCount--;
            }  
        }
        if (deckCount == 0)
        {
            Debug.Log("Deck is empty!");
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
    void ShuffleDeck(List<Card> array)
    {
        for (int i = array.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Card temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}
