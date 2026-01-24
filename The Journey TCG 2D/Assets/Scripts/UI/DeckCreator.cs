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
        Deck deckToAdd;
        deckToAdd = (format == DeckFormat.Standard)? new StandardDeck() : new WildDeck();
        deckToAdd.deckName = deckName;
        Player.GetPlayer().Properties().AddDeck(deckToAdd);
        if (Player.GetPlayer().Properties().GetDecks().Count == 0)
        {
            Debug.Log("No deck was added.");
            return;
        }
        DeckManager.Instance.SelectedDeck = deckToAdd;
        SceneManager.LoadScene("DeckCreator");
    }

    public void SetDeckName(string newName)
    {
        deckName = (newName == null || newName == "") ? "New Deck" : newName;
    }
}
