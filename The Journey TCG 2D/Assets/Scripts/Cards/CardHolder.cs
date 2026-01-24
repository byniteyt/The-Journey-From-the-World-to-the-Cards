using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PlayerProperties;

public class CardHolder : MonoBehaviour
{
    GameObject cardButton;
    GameObject cardHolder;


    private void Start()
    {
        cardHolder = GameObject.Find("CardHolder");
        cardButton = Resources.Load<GameObject>("Prefabs/Cards/CollectedCard");
        if (Player.GetPlayer().Properties().cardsReceived.Count == 0) 
            Debug.Log("No hay cartas guardadas para mostrar");
        foreach (CardsReceived newCard in Player.GetPlayer().Properties().cardsReceived)
        {
            GameObject cb = Instantiate(cardButton, cardHolder.transform);
            //cb.transform.GetChild(0).GetComponent<Image>().sprite = card.artwork;
            cb.GetComponentInChildren<TextMeshProUGUI>().text = newCard.amount.ToString();
            cb.name = newCard.card.GetCard().cardName;
            cb.transform.GetChild(0).GetComponent<Image>().sprite = newCard.card.GetCard().artwork;
            cb.AddComponent<BoxCollider2D>();
            string nameOfCard = cb.name;
            switch (newCard.card.GetType().ToString())
            {
                case "BattleRoomCard":
                    cb.AddComponent<BattleRoomCard>();
                    cb.GetComponent<BattleRoomCard>().SetCard(newCard.card.GetRoom());
                    Debug.Log($"Adding {cb.name}  with the card {newCard.card.GetRoom().cardName} as a Room");
                    nameOfCard = newCard.card.GetRoom().cardName;
                    break;
                case string s when s.Contains("Spell"):
                    cb.AddComponent<BattleSpellCard>();
                    cb.GetComponent<BattleSpellCard>().SetCard(newCard.card.GetSpell());
                    Debug.Log($"Adding {cb.name}  with the card {newCard.card.GetSpell().cardName} as a Spell");
                    nameOfCard = newCard.card.GetSpell().cardName;
                    break;
                case "BattleCharCard":
                    cb.AddComponent<BattleCharCard>();
                    cb.GetComponent<BattleCharCard>().SetCard(newCard.card.GetCharacter());
                    Debug.Log($"Adding {cb.name}  with the card {newCard.card.GetCharacter().cardName} as a Character");
                    nameOfCard = newCard.card.GetCharacter().cardName;
                    break;
                case "Card":
                    Debug.Log($"Adding {newCard.card.GetCard().cardName} as a Card");
                    break;
                default:
                    Debug.Log($"Adding {newCard.card.GetCard().cardName} as a {newCard.card.GetType()} card type");
                    break;
            }
            cb.GetComponent<Button>().onClick.AddListener(() => {
                Debug.Log($"Adding {nameOfCard} to deck.");
                if (Player.GetPlayer().Properties().GetCard(nameOfCard) == null)
                {
                    Debug.LogError($"Card {nameOfCard} not found in player properties.");
                    return;
                }
                AddToDeck(Player.GetPlayer().Properties().GetCard(nameOfCard).GetComponent<BattleCard>().GetCard());
            });
        }
    }
    void AddToDeck(Card cardToAdd)
    {
        if (DeckManager.Instance.SelectedDeck == null)
        {
            Debug.Log("No deck selected.");
            return;
        }
        DeckManager.Instance.SelectedDeck.AddCard(cardToAdd);
    }
}
