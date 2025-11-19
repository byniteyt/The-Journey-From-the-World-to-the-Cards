using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR;

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
        EventManager.FirstMainTurn += StartTurn;
        deck = DeckManager.selectedDeck;
        deckCount = deck.GetDeck().Count;
        Debug.Log($"Deck Count: {deckCount}");
        
        ShuffleDeck(deck.GetDeck());
        DrawCard(5);

    }

    void StartTurn(object sender, System.EventArgs e)
    {
        DrawCard(Hand.Instance.GetHandSize() - Hand.Instance.GetHandAmount());
    }

    public void DrawCard(int amount)
    {
        if(amount>DeckCount) amount = deckCount;
        for (int i = 0; i<amount;i++)
        {
            if (Hand.Instance.AddCard(deck.GetLastCard()))
            {
                //deck.RemoveCard(deck.GetLastCard());
                deck.RemoveLastCard();
                Debug.Log($"Se ha eliminado {deck.GetLastCard().name}");
                deckCount--;
            }  
        }
        if (deckCount == 0)
        {
            Debug.Log("Deck is empty!");
            this.GetComponent<SpriteRenderer>().enabled = false;
            return;
        }
        Debug.Log($"Proxima carta a robar: {deck.GetLastCard().name}");
    }

    void ShuffleDeck(List<Card> array)
    {
        for (int i = array.Count - 1; i > 0; i--)
        {
            Debug.Log($"La carta nº {i} es {array[i].name}---------------------------");
        }
        Debug.Log( array.ToString());
        
        for (int i = array.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Card temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }

        for (int i = array.Count - 1; i > 0; i--)
        {
            Debug.Log($"------------------------La carta nº {i} es {array[i].name}");
        }
    }
}
