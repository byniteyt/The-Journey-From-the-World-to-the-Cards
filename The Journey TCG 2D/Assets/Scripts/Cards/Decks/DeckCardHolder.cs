using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckCardHolder : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject deckCardButton;
    GameObject deckCardHolder;


    private void Start()
    {
        deckCardHolder = GameObject.Find("DeckCardHolder");
        deckCardButton = Resources.Load<GameObject>("Prefabs/Cards/CollectedCard");
        if (DeckManager.selectedDeck == null)
        {
            Debug.Log("No deck selected.");
            return;
        }
        if (DeckManager.selectedDeck.GetDeck().Count == 0)
        {
            Debug.Log("Selected deck has no cards.");
            return;
        }
        foreach (string key in DeckManager.selectedDeck.GetDictionary().Keys)
        {
            Card card = DeckManager.selectedDeck.GetCardByName(key);
            GameObject cb = Instantiate(deckCardButton, deckCardHolder.transform);
            //cb.transform.GetChild(0).GetComponent<Image>().sprite = card.artwork;
            cb.GetComponentInChildren<TextMeshProUGUI>().text = DeckManager.selectedDeck.GetCardLimit(card).ToString();
            cb.gameObject.name = card.name;
            cb.AddComponent<BoxCollider2D>();
            cb.transform.GetChild(0).GetComponent<Image>().sprite = card.artwork;
            switch (card.GetType().ToString())
            {
                case "RoomCard":
                    cb.AddComponent<RoomCard>();
                    cb.GetComponent<RoomCard>().CopyValues((RoomCard)card);
                    break;
                case "SpellCard":
                    cb.AddComponent<SpellCard>();
                    cb.GetComponent<SpellCard>().CopyValues((SpellCard)card);
                    break;
                case "CharacterCard":
                    cb.AddComponent<CharacterCard>();
                    cb.GetComponent<CharacterCard>().CopyValues((CharacterCard)card);
                    break;
                default:
                    Debug.Log("Unknown card type");
                    break;
            }
        }
    }
}
