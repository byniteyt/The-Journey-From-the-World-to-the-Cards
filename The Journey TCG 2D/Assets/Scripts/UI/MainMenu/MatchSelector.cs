using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    }
    public void StartMatch()
    {
        if (DeckManager.Instance.SelectedDeck == null)
        {
            switch (format)
            {
                case DeckFormat.Standard:
                    if(Player.GetPlayer().Properties().GetValidStandardDecks().Count == 0)
                    {
                        Debug.Log("No valid standard deck available.");
                        return;
                    }
                    DeckManager.Instance.SelectedDeck =Player.GetPlayer().Properties().GetValidStandardDecks()[0];
                    break;
                case DeckFormat.Wild:
                    if(Player.GetPlayer().Properties().GetValidWildDecks().Count == 0)
                    {
                        Debug.Log("No valid wild deck available.");
                        return;
                    }
                    DeckManager.Instance.SelectedDeck = Player.GetPlayer().Properties().GetValidWildDecks()[0];
                    break;
                default:
                    break;
            }
            SceneManager.LoadScene(format.ToString() + "Match");
            return;
        }

        if (DeckManager.Instance.SelectedDeck.GetFormat() != format)
        {
            Debug.Log("La baraja no es correcta.");
            return;
        }

        if (!DeckManager.Instance.SelectedDeck.IsValidForPlay())
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

    public void StartTutorial()
    {
        SceneManager.LoadScene("TutorialCombat");
    }
}
