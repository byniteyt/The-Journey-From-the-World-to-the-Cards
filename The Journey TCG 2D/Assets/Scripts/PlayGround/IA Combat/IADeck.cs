using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class IADeck : BattleDeck
{
    public static IADeck Instance { get; private set; }
   
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
        deck = DeckManager.IADeck;
        //deck = DeckCollection.GetDeck(Mathf.Min(1, DeckCollection.DecksAmount())); // Si solo hay un mazo, usa ese
        deckCount = deck.GetDeck().Count;
        Debug.Log($"IA Deck Count: {deckCount}");
        
        ShuffleDeck(deck.GetDeck());

        EventManager.FirstIAMainTurn+= StartIATurn;
    }
    void StartIATurn(object sender, System.EventArgs e)
    {
        int amount = IAPlayer.GetHand().GetHandSize() - IAPlayer.GetHand().GetHandAmount();
        Debug.Log($"-----------------IA Draw {amount} cards-----------------");
        DrawCard(amount);
    }
    public void DrawCard(int amount)
    {
        Debug.Log("IA robó:");
        if(amount>DeckCount) amount = deckCount;
        for (int i = 0; i<amount;i++)
        {
            if (deck.GetLastCard()==null)
            {
                Debug.Log("No more cards to draw for IA!");
                return;
            }
            if (IAHand.GetIAHand().AddCard(deck.GetLastCard()))
            {
                //deck.RemoveCard(deck.GetLastCard());
                Debug.Log($"Una carta de {deck.GetLastCard().name}");
                deck.RemoveLastCard();
                deckCount--;
            }  
        }
        if (deckCount == 0)
        {
            Debug.Log("IA Deck is empty!");
            GetComponent<SpriteRenderer>().enabled = false;
            return;
        }
        Debug.Log($"Proxima carta a robar de la IA: {deck.GetLastCard().name}");
    }
    void ShuffleDeck(List<BattleCard> array)
    {
        for (int i = array.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            BattleCard temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}