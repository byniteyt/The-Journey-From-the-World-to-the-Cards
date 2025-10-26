using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeckHolder : MonoBehaviour
{
    GameObject deckButton;
    GameObject deckHolder;

    
    private void Start()
    {
        deckHolder = GameObject.Find("DeckHolder");
        deckButton = Resources.Load<GameObject>("Prefabs/Decks/DeckButton");
        foreach (Deck deck in DeckCollection.SavedDecks())
        {
            GameObject db = Instantiate(deckButton, deckHolder.transform);
            db.transform.SetAsFirstSibling();
            db.gameObject.name = deck.name;
            db.GetComponentInChildren<TextMeshProUGUI>().text = $"{deck.name}\n\n{deck.GetFormat()}";
            if (deck.GetComponent<WildDeck>()) db.AddComponent<WildDeck>();
            else if (deck.GetComponent<StandardDeck>()) db.AddComponent<StandardDeck>();
            Button selectButton = db.GetComponent<Button>();
            selectButton.onClick.AddListener(() => SelectDeck(deck));
            db.GetComponent<Deck>().SetDeckName(deck.ToString());
        }
    }
    void SelectDeck(Deck selectedDeck)
    {
        DeckBuilderManager.selectedDeck = selectedDeck;
        DontDestroyOnLoad(DeckBuilderManager.selectedDeck);
        SceneManager.LoadScene("DeckCreator");
    }
}
