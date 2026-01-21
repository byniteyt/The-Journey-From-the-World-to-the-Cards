using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerDeck : BattleDeck
{
    public static PlayerDeck Instance { get; private set; }
   
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
        deck = DeckManager.Instance.SelectedDeck;
        deckCount = deck.GetDeck().Count;
        Debug.Log($"Deck Count: {deckCount}");
        
        ShuffleDeck(deck.GetDeck());

    }

    void StartTurn(object sender, System.EventArgs e)
    {
        DrawCard(PlayerHand.GetPlayerHand().GetHandSize() - PlayerHand.GetPlayerHand().GetHandAmount());
    }

    public void DrawCard(int amount)
    {
        if(amount>DeckCount) amount = deckCount;
        for (int i = 0; i<amount;i++)
        {
            if (PlayerHand.GetPlayerHand().AddCard(deck.GetLastCard()))
            {
                //deck.RemoveCard(deck.GetLastCard());

                Debug.Log($"Se ha eliminado {deck.GetLastCard().name}");
                deck.RemoveLastCard();
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

    void ShuffleDeck(List<BattleCard> array)
    {
        for (int i = array.Count - 1; i > 0; i--)
        {
            Debug.Log($"La carta nº {i} es {array[i].GetCard().cardName}---------------------------");
        }
        Debug.Log( array.ToString());
        
        for (int i = array.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            (array[i], array[randomIndex]) = (array[randomIndex], array[i]);
        }

        for (int i = array.Count - 1; i > 0; i--)
        {
            Debug.Log($"------------------------La carta nº {i} es {array[i].name}");
        }
    }
}
