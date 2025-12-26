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
            BattleCard card = DeckManager.selectedDeck.GetCardByName(key);
            GameObject cb = Instantiate(deckCardButton, deckCardHolder.transform);
            //cb.transform.GetChild(0).GetComponent<Image>().sprite = card.artwork;
            cb.GetComponentInChildren<TextMeshProUGUI>().text = DeckManager.selectedDeck.GetCardLimit(card).ToString();
            cb.gameObject.name = card.name;
            cb.AddComponent<BoxCollider2D>();
            cb.transform.GetChild(0).GetComponent<Image>().sprite = card.GetCard().artwork;
            switch (card.GetType().ToString())
            {
                case "RoomCard":
                    cb.AddComponent<BattleRoomCard>();
                    cb.GetComponent<BattleRoomCard>().SetCard((RoomCard)card.GetCard().Clone());
                    break;
                case "SpellCard":
                    cb.AddComponent<BattleSpellCard>();
                    cb.GetComponent<BattleSpellCard>().SetCard((SpellCard)card.GetCard().Clone());
                    break;
                case "CharacterCard":
                    cb.AddComponent<BattleCharCard>();
                    cb.GetComponent<BattleCharCard>().SetCard((CharacterCard)card.GetCard().Clone());
                    break;
                default:
                    Debug.Log("Unknown card type");
                    break;
            }
        }
    }
}
