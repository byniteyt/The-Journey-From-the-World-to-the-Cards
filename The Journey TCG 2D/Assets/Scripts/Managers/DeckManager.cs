using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeckManager : MonoBehaviour
{
    public static Deck selectedDeck;

    public void CreateDeck()
    {
        if (DeckCollection.DecksAmount() == 0)
        {
            Debug.Log("No deck was added.");
            return;
        }
        selectedDeck = DeckCollection.GetDeck(DeckCollection.DecksAmount() - 1);
        DontDestroyOnLoad(selectedDeck);
        SceneManager.LoadScene("DeckCreator");
    }

    public void RemoveDeck(Deck deckToDelete)
    { 
        int index = DeckCollection.GetIndexOfDeck(deckToDelete);
        Destroy(gameObject.transform.GetChild(index).gameObject);
        DeckCollection.RemoveDeck(deckToDelete);
    }
}
