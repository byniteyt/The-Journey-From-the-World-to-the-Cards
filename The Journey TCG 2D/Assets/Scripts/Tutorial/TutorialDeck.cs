using UnityEngine;

public class TutorialDeck : BattleDeck
{
    string[] cards = { "Sappy", "Shield", "Seta", "Swords", "Town Hall" };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadTutorialCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LoadTutorialCards()
    {
        foreach (var card in cards)
        {
            deck.AddCard(CardDataBase.Instance.GetCard(card));
            deck.AddCard(CardDataBase.Instance.GetCard(card));
            deck.AddCard(CardDataBase.Instance.GetCard(card));
        }
    }
}
