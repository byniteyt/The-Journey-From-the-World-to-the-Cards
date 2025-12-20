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
        foreach (BattleDeck deck in PlayerProperties.properties.GetDecks())
        {
            GameObject db = Instantiate(deckButton, deckHolder.transform);
            db.transform.SetAsFirstSibling();
            db.gameObject.name = deck.name;
            db.GetComponentInChildren<TextMeshProUGUI>().text = $"{deck.name}\n\n{deck.GetFormat()}";
            if (deck.GetComponent<WildBattleDeck>()) db.AddComponent<WildBattleDeck>();
            else if (deck.GetComponent<StandardBattleDeck>()) db.AddComponent<StandardBattleDeck>();
            Button selectButton = db.GetComponent<Button>();
            selectButton.onClick.AddListener(() => { SelectDeck(deck); selectedButton = selectButton; });
            db.GetComponent<BattleDeck>().SetDeckName(deck.ToString());
        }
    }
    void SelectDeck(BattleDeck selectedDeck)
    {
        DeckManager.selectedDeck = selectedDeck;
        DontDestroyOnLoad(DeckManager.selectedDeck);
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
        Destroy(DeckManager.selectedDeck);
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
