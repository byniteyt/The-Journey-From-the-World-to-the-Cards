using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeckBuilderManager : MonoBehaviour
{
    public static Deck selectedDeck;

    string deckName;
    DeckFormat format;

    
    public void CreateDeck()
    {
        AddDeck();
        if (DeckCollection.DecksAmount() == 0)
        {
            Debug.Log("No deck was added.");
            return;
        }
        selectedDeck = DeckCollection.GetDeck(DeckCollection.DecksAmount() - 1);
        DontDestroyOnLoad(selectedDeck);
        SceneManager.LoadScene("DeckCreator");
    }

    void AddDeck()
    {
        GameObject newDeckObj = new GameObject("Deck new"); // crea un objeto vacío
        Deck deckToAdd;

        if (format == DeckFormat.Standard)
        {
            deckToAdd = newDeckObj.AddComponent<StandardDeck>();
        }
        else
        {
            deckToAdd = newDeckObj.AddComponent<WildDeck>();
        }
        deckToAdd.name = deckName;
        DontDestroyOnLoad(deckToAdd);
        DeckCollection.AddDeck(deckToAdd);
    }

    public void SetDeckName(string newName)
    {
        deckName = (newName == null || newName == "") ? "New Deck" : newName;
    }
    public void SelectFormat(int value)
    {
        format = (value==0)? DeckFormat.Standard: DeckFormat.Wild;
    }
    public void RemoveDeck(Deck deckToDelete)
    { 
        int index = DeckCollection.GetIndexOfDeck(deckToDelete);
        Destroy(gameObject.transform.GetChild(index).gameObject);
        DeckCollection.RemoveDeck(deckToDelete);

    }
}
