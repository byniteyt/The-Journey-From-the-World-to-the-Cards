using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;

public class MatchSelector : MonoBehaviour
{
    TMP_Dropdown formatDrop;
    DeckFormat format;

    private void Start()
    {
        GameObject.Find("Play").GetComponentInChildren<TextMeshProUGUI>().text =
            $"Start {format} match";
        formatDrop = GameObject.Find("SelectFormat").GetComponent<TMP_Dropdown>();
        formatDrop.ClearOptions();
        var options = new List<TMP_Dropdown.OptionData>();
        for (int i = 0; i < Enum.GetNames(typeof(DeckFormat)).Length; i++)
        {
            options.Add(new TMP_Dropdown.OptionData(((DeckFormat)i).ToString()));
        }
        formatDrop.AddOptions(options);
        formatDrop.onValueChanged.AddListener(SelectMatchFormat);
        Debug.Log((DeckFormat)3);
    }
    public void StartMatch()
    {
        if (DeckManager.selectedDeck == null)
        {
            Debug.Log("No has elegido la baraja.");
            return;
        }

        if (DeckManager.selectedDeck.GetFormat() != format)
        {
            Debug.Log("La baraja no es correcta.");
            return;
        }

        if (!DeckManager.selectedDeck.IsValidForPlay())
        {
            Debug.Log("La baraja no es válida. Asegúrate de que cumple los requisitos."); 
            return;
        }

        SceneManager.LoadScene(format.ToString() + "Match");
    }

    void SelectMatchFormat(int index)
    {
        format = (DeckFormat)index;
        GameObject.Find("Play").GetComponentInChildren<TextMeshProUGUI>().text = 
            $"Start {format} match";
    }
}
