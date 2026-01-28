using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
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
        deck = (GameManager.Instance.GetCurrentFormat()== DeckFormat.Standard)? 
            Player.GetPlayer().Properties().GetStandardDecks()[Random.Range(0, Player.GetPlayer().Properties().GetStandardDecks().Count)]
            : Player.GetPlayer().Properties().GetWildDecks()[Random.Range(0, Player.GetPlayer().Properties().GetWildDecks().Count)];
        deckCount = deck.GetDeck().Where(c => c != null).ToList().Count;
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
        if(amount>deckCount) amount = deckCount;
        for (int i = 0; i<amount;i++)
        {
            if (deck.GetLastCard()==null)
            {
                Debug.Log("No more cards to draw for IA!");
                return;
            }
            if (IAHand.GetIAHand().AddCard(CardDataBase.Instance.GetObjectCard(deck.GetLastCard().cardName).GetComponent<BattleCard>()))
            {
                Debug.Log($"Una carta de {deck.GetLastCard().cardName}");
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
        Debug.Log($"Proxima carta a robar de la IA: {deck.GetLastCard().cardName}");
    }
    void ShuffleDeck(List<Card> array)
    {
        for (int i = array.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            Card temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }
}