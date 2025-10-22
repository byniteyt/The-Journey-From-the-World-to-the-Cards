using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.GPUSort;

public class DeckBuilderManager : MonoBehaviour
{
    public static Deck selectedDeck;
    GameObject deckButton;
    string deckName;
    DeckFormat format;

    private void Start()
    {
        deckButton = Resources.Load<GameObject>("Prefabs/Decks/AddDeckButton");
        foreach (Deck deck in DeckCollection.SavedDecks())
        {
            GameObject db = Instantiate(deckButton,this.transform);
            db.transform.SetAsFirstSibling();
            db.gameObject.name = deck.name;
            if (deck.GetComponent<WildDeck>()) db.AddComponent<WildDeck>();
            else if (deck.GetComponent<StandardDeck>()) db.AddComponent<StandardDeck>();
            db.GetComponent<Deck>().SetDeckName(deck.ToString());
        }
    }
    public void CreateDeck()
    {
        AddDeck();
        if (DeckCollection.DecksAmount() == 0)
        {
            Debug.Log("No deck was added.");
            return;
        }
        selectedDeck = DeckCollection.GetDeck(DeckCollection.DecksAmount() - 1);
        SceneManager.LoadScene("DeckCreator");
    }

    void AddDeck()
    {
        GameObject newDeckObj = new GameObject(deckName); // crea un objeto vacío
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
        format = (format==DeckFormat.Standard)? DeckFormat.Standard: DeckFormat.Wild;
    }
    public void RemoveDeck(Deck deckToDelete)
    { 
        int index = DeckCollection.GetIndexOfDeck(deckToDelete);
        Destroy(gameObject.transform.GetChild(index).gameObject);
        DeckCollection.RemoveDeck(deckToDelete);

    }
}
