using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeckManager : MonoBehaviour
{
    public static Deck selectedDeck;

    public void RemoveDeck(Deck deckToDelete)
    { 
        int index = DeckCollection.GetIndexOfDeck(deckToDelete);
        Destroy(gameObject.transform.GetChild(index).gameObject);
        DeckCollection.RemoveDeck(deckToDelete);
    }
}
