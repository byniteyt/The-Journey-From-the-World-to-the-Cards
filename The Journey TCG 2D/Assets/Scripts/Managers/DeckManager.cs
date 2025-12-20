using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public static BattleDeck selectedDeck;
    public static BattleDeck IADeck;

    public void RemoveDeck(BattleDeck deckToDelete)
    { 
        int index = DeckCollection.GetIndexOfDeck(deckToDelete);
        Destroy(gameObject.transform.GetChild(index).gameObject);
        DeckCollection.RemoveDeck(deckToDelete);
    }
}
