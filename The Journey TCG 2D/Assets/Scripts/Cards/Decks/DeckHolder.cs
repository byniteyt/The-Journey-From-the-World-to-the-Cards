using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeckHolder : MonoBehaviour
{
    GameObject deckButton;
    GameObject deckHolder;
    static Button selectedButton;

    
    private void Start()
    {
        deckHolder = GameObject.Find("DeckHolder");
        deckButton = Resources.Load<GameObject>("Prefabs/Decks/DeckButton");
        foreach (Deck deck in PlayerProperties.properties.GetDecks())
        {
            GameObject db = Instantiate(deckButton, deckHolder.transform);
            db.transform.SetAsFirstSibling();
            db.gameObject.name = deck.deckName;
            db.GetComponentInChildren<TextMeshProUGUI>().text = $"{deck.deckName}\n\n{deck.GetFormat()}";
            if (deck.GetType() == typeof(WildDeck)) db.AddComponent<BattleDeck>().deck = deck;
            else if (deck.GetType() == typeof(StandardDeck)) db.AddComponent<BattleDeck>().deck = deck;

            Button selectButton = db.GetComponent<Button>();
            selectButton.onClick.AddListener(() => { SelectDeck(deck); selectedButton = selectButton; });
            db.GetComponent<BattleDeck>().SetDeckName(deck.ToString());
        }
    }
    void SelectDeck(Deck selectedDeck)
    {
        DeckManager.selectedDeck = selectedDeck;
        //DontDestroyOnLoad(DeckManager.selectedDeck);
    }

    public void EditDeck()
    {
        SceneManager.LoadScene("DeckCreator");
    }
    public void PredeleteDeck()
    {
        StartCoroutine(ConfirmDeleteDeck());
    }
    void DestroyDeck()
    {
        if (selectedButton == null)
        {
            Debug.Log("No hay deck seleccionada");
            return;
        }
        DeckCollection.RemoveDeck(DeckManager.selectedDeck);
        //Destroy(DeckManager.selectedDeck);
        Destroy(selectedButton.gameObject);
    }
    IEnumerator ConfirmDeleteDeck()
    {
        Action deleteAction = new Action(DestroyDeck);
        yield return null;
        GameObject Warning = Instantiate(Resources.Load<GameObject>("Prefabs/Warning/Warning"));
        yield return new WaitForEndOfFrame();
        EventManager.GetOrder?.Invoke(this, deleteAction);
        DeleteDeck delete = new DeleteDeck(DeckManager.selectedDeck);
        delete.Execute();
    }

}
