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
        foreach (Deck deck in Player.GetPlayer().Properties().GetDecks())
        {
            Debug.Log($"Cargando deck: {deck.deckName}");
            GameObject db = Instantiate(deckButton, deckHolder.transform);
            db.transform.SetAsFirstSibling();
            db.name = deck.deckName;
            db.GetComponentInChildren<TextMeshProUGUI>().text = $"{deck.deckName}\n\n{deck.GetFormat()}";
            if (deck.deckFormat == DeckFormat.Wild) { db.AddComponent<BattleDeck>().deck = (WildDeck)deck; db.GetComponentInChildren<Image>().color = Color.darkOrange; }
            else if (deck.deckFormat == DeckFormat.Standard) { db.AddComponent<BattleDeck>().deck = (StandardDeck)deck; db.GetComponentInChildren<Image>().color = Color.darkOliveGreen;}
            else Debug.LogError($"Tipo de deck desconocido: {deck.deckFormat}");
            Button selectButton = db.GetComponent<Button>();
            selectButton.onClick.AddListener(() => { SelectDeck(deck); selectedButton = selectButton; });
            db.GetComponent<BattleDeck>().SetDeckName(deck.deckName);
        }
    }
    void SelectDeck(Deck selectedDeck)
    {
        DeckManager.Instance.SelectedDeck = selectedDeck;
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
        Player.GetPlayer().Properties().RemoveDeck(DeckManager.Instance.SelectedDeck);
        //Destroy(DeckManager.selectedDeck);
        Destroy(selectedButton.gameObject);
    }
    IEnumerator ConfirmDeleteDeck()
    {
        Action deleteAction = new(DestroyDeck);
        yield return null;
        GameObject Warning = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Warning/Warning"));
        yield return new WaitForEndOfFrame();
        EventManager.GetOrder?.Invoke(this, deleteAction);
        DeleteDeck delete = new(DeckManager.Instance.SelectedDeck);
        delete.Execute();
    }

}
