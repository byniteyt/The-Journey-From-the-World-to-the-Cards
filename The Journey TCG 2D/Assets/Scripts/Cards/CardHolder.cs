using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardHolder : MonoBehaviour
{
    GameObject cardButton;
    GameObject cardHolder;
    [SerializeField] int[] maxCardAmount;


    private void Start()
    {
        cardHolder = GameObject.Find("CardHolder");
        cardButton = Resources.Load<GameObject>("Prefabs/Decks/DeckButton");
        foreach ( Card card in CardCollection.GetCollection().Keys)
        {
            GameObject cb = Instantiate(cardButton, cardHolder.transform);
            cb.transform.SetAsFirstSibling();
            cb.gameObject.name = card.name;
            cb.GetComponentInChildren<TextMeshProUGUI>().text = $"{card.name}";
            switch (card.GetType().ToString())
            {
                case "RoomCard":
                    cb.AddComponent<RoomCard>();
                    cb.GetComponent<RoomCard>().CopyValues((RoomCard) card);
                    break;
                case "SpellCard":
                    cb.AddComponent<SpellCard>();
                    cb.GetComponent<SpellCard>().CopyValues((SpellCard) card);
                    break;
                case "CharacterCard":
                    cb.AddComponent<CharacterCard>();
                    cb.GetComponent<CharacterCard>().CopyValues((CharacterCard) card);
                    break;
                default:
                    Debug.Log("Unknown card type");
                    break;
            }
            Button selectButton = cb.GetComponent<Button>();
            selectButton.onClick.AddListener(() => AddToDeck(card));
        }
    }
    void AddToDeck(Card cardToAdd)
    {
        if (DeckBuilderManager.selectedDeck == null)
        {
            Debug.Log("No deck selected.");
            return;
        }
        DeckBuilderManager.selectedDeck.AddCard(cardToAdd);
    }
}
