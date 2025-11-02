using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeckEditorManager : MonoBehaviour
{
    int index;
    Deck deckToEdit;
    GameObject deckNameText;
    private void Start()
    {
        //Debug.Log($"La baraja se llama {DeckCollection.decks[DeckCollection.decks.Count-1].name}");
        //Debug.Log($"La baraja es de tipo {DeckCollection.decks[DeckCollection.decks.Count-1].GetFormat()}");
        Debug.Log(DeckCollection.DecksAmount());
        index = DeckCollection.GetIndexOfDeck(DeckManager.selectedDeck);
        Debug.Log($"La baraja es {DeckCollection.GetDeck(DeckCollection.DecksAmount() - 1).GetComponent<Deck>().name}");
        deckToEdit = DeckManager.selectedDeck;
        deckNameText = GameObject.Find("DeckNameText");
        if (deckNameText== null)
        {
            Debug.Log("Text not found");
            return;
        }
        if (deckNameText.GetComponent<TMP_Text>() == null) 
        {
            Debug.Log("TMP_Text component not found");
            return;
        }
        if (deckToEdit == null)
        {
            Debug.Log("No deck to edit");
            return;
        }
        deckNameText.GetComponent<TMP_Text>().text = deckToEdit.name;
    }
    public void SaveDeck()
    {
        DeckCollection.RemoveDeck(deckToEdit);
        DeckCollection.AddDeck(deckToEdit);
        SceneManager.LoadScene("DeckBuilder");
    }
}
