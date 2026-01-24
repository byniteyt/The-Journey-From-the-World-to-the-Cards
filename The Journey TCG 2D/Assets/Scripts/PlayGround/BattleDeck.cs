using UnityEngine;

public class BattleDeck : MonoBehaviour
{
    [HideInInspector] public Deck deck;
    protected int deckCount;
    public int DeckCount { get { return deckCount; } private set { deckCount = value; } }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDeckName(string name)
    {
        deck.deckName = name;
    }
}
