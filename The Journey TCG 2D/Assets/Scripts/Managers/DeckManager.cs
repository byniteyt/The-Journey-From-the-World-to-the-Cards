using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static Deck selectedDeck;
    public static Deck IADeck;

    public void RemoveDeck(Deck deckToDelete)
    { 
        int index = DeckCollection.GetIndexOfDeck(deckToDelete);
        Destroy(gameObject.transform.GetChild(index).gameObject);
        DeckCollection.RemoveDeck(deckToDelete);
    }
}
