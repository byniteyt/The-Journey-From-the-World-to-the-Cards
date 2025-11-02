using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardHolder : MonoBehaviour
{
    GameObject cardButton;
    GameObject cardHolder;


    private void Start()
    {
        cardHolder = GameObject.Find("CardHolder");
        cardButton = Resources.Load<GameObject>("Prefabs/Cards/CollectedCard");
        foreach (Card card in CardCollection.GetCollection().Keys)
        {
            GameObject cb = Instantiate(cardButton, cardHolder.transform);
            //cb.transform.GetChild(0).GetComponent<Image>().sprite = card.artwork;
            cb.GetComponentInChildren<TextMeshProUGUI>().text = CardCollection.GetCollection()[card].ToString();
            cb.gameObject.name = card.name;
            cb.transform.GetChild(0).GetComponent<Image>().sprite = card.artwork;
            cb.AddComponent<BoxCollider2D>();
            switch (card.GetType().ToString())
            {
                case "RoomCard":
                    Debug.Log($"Adding {card.name} as a RoomCard");
                    cb.AddComponent<RoomCard>();
                    cb.GetComponent<RoomCard>().CopyValues((RoomCard) card);
                    break;
                case string s when s.Contains("Spell"):
                    Debug.Log($"Adding {card.name} as a {card.GetType()}");
                    cb.AddComponent<SpellCard>();
                    cb.GetComponent<SpellCard>().CopyValues((SpellCard) card);
                    break;
                case "CharacterCard":
                    Debug.Log($"Adding {card.name} as a CharacterCard");
                    cb.AddComponent<CharacterCard>();
                    cb.GetComponent<CharacterCard>().CopyValues((CharacterCard) card);
                    break;
                default:
                    Debug.Log($"Adding {card.name} as a Unknown card type");
                    break;
            }
            Button selectButton = cb.GetComponent<Button>();
            selectButton.onClick.AddListener(() => AddToDeck(card));
        }
    }
    void AddToDeck(Card cardToAdd)
    {
        if (DeckManager.selectedDeck == null)
        {
            Debug.Log("No deck selected.");
            return;
        }
        DeckManager.selectedDeck.AddCard(cardToAdd);
    }
}
