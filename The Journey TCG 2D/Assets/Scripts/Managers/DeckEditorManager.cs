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
        Debug.Log(Player.GetPlayer().Properties().GetDecks().Count);
        index = Player.GetPlayer().Properties().GetIndexOfDeck(DeckManager.Instance.SelectedDeck);
        Debug.Log($"La baraja es {Player.GetPlayer().Properties().GetDecks()[^1].deckName}");
        deckToEdit = DeckManager.Instance.SelectedDeck;
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
        deckNameText.GetComponent<TMP_Text>().text = deckToEdit.deckName;
    }
    public void SaveDeck()
    {
        Player.GetPlayer().Properties().RemoveDeck(deckToEdit);
        Player.GetPlayer().Properties().AddDeck(deckToEdit);
        SceneManager.LoadScene("DeckBuilder");
    }
}
