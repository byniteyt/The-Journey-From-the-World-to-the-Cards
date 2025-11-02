using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateDeck : MonoBehaviour
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
}
