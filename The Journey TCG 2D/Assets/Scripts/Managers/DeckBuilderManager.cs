using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.GPUSort;

public class DeckBuilderManager : MonoBehaviour
{
    public static Deck selectedDeck;
    GameObject deckButton;
    string deckName;
    DeckFormat format;
    GameObject deckHolder;

    private void Start()
    {
        deckHolder = GameObject.Find("DeckHolder");
        deckButton = Resources.Load<GameObject>("Prefabs/Decks/DeckButton");
        foreach (Deck deck in DeckCollection.SavedDecks())
        {
            GameObject db = Instantiate(deckButton,deckHolder.transform);
            db.transform.SetAsFirstSibling();
            db.gameObject.name = deck.name;
            if (db.GetComponentInChildren<TextMeshProUGUI>() == null)
            {
                Debug.Log("TextMeshPro component not found in Deck Button prefab.");
                return;
            }
            db.GetComponentInChildren<TextMeshProUGUI>().text = $"{deck.name}\n{deck.GetFormat()}";
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
        format = (format==DeckFormat.Standard)? DeckFormat.Standard: DeckFormat.Wild;
    }
    public void RemoveDeck(Deck deckToDelete)
    { 
        int index = DeckCollection.GetIndexOfDeck(deckToDelete);
        Destroy(gameObject.transform.GetChild(index).gameObject);
        DeckCollection.RemoveDeck(deckToDelete);

    }
}
