using UnityEngine;

public class BattleDeck : MonoBehaviour
{
    [HideInInspector] public Deck deck;
    protected int deckCount;
    public int DeckCount { get { return deckCount; } private set { deckCount = value; } }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetDeckName(string name)
    {
        deck.deckName = name;
    }
}
