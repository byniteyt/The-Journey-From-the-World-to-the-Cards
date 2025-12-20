using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeckCreator : MonoBehaviour
{
    string deckName;
    TMP_Dropdown formatDrop;
    DeckFormat format;

    private void Start()
    {
        formatDrop = GameObject.Find("SelectFormat").GetComponent<TMP_Dropdown>();
        formatDrop.ClearOptions();
        var options = new List<TMP_Dropdown.OptionData>();
        for (int i = 0; i < Enum.GetNames(typeof(DeckFormat)).Length; i++)
        {
            options.Add(new TMP_Dropdown.OptionData(((DeckFormat)i).ToString()));
        }
        formatDrop.AddOptions(options);
        formatDrop.onValueChanged.AddListener(SelectMatchFormat);
    }

    void SelectMatchFormat(int index)
    {
        format = (DeckFormat)index;
    }

    public void AddDeck()
    {
        GameObject newDeckObj = new GameObject("Deck new"); // crea un objeto vacío
        Deck deckToAdd;
        deckToAdd = (format == DeckFormat.Standard)? new StandardDeck() : new WildDeck();
        deckToAdd.deckName = deckName;
        //DontDestroyOnLoad(deckToAdd);
        DeckCollection.AddDeck(deckToAdd);
        PlayerProperties.properties.AddDeck(deckToAdd);
        if (DeckCollection.DecksAmount() == 0)
        {
            Debug.Log("No deck was added.");
            return;
        }
        //DontDestroyOnLoad(DeckCollection.GetDeck(DeckCollection.DecksAmount() - 1));
        DeckManager.selectedDeck = deckToAdd;
        DeckManager.IADeck = DeckCollection.GetDeck(0);
        SceneManager.LoadScene("DeckCreator");
    }

    public void SetDeckName(string newName)
    {
        deckName = (newName == null || newName == "") ? "New Deck" : newName;
    }
}
