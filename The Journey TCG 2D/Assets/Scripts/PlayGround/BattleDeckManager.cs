using System.Runtime.CompilerServices;
using UnityEngine;

public class BattleDeckManager : MonoBehaviour
{
    public static BattleDeckManager Instance { get; private set; }
    public Card[] deck;
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
        deckCount = deck.Length;
        Debug.Log($"Deck Count: {deckCount}");
        ShuffleArray(deck);
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
    void ShuffleArray(Card[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Card temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }
}
