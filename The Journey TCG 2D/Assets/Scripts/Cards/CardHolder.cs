using System.Linq;
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
        if (Player.player.Properties().cardsReceived.Count == 0) Debug.Log("No hay cartas guardadas para mostrar");
        foreach (CardsReceived newCard in Player.player.Properties().cardsReceived)
        {
            GameObject cb = Instantiate(cardButton, cardHolder.transform);
            //cb.transform.GetChild(0).GetComponent<Image>().sprite = card.artwork;
            cb.GetComponentInChildren<TextMeshProUGUI>().text = newCard.amount.ToString();
            cb.gameObject.name = newCard.card.cardName;
            cb.transform.GetChild(0).GetComponent<Image>().sprite = newCard.card.artwork;
            cb.AddComponent<BoxCollider2D>();
            switch (newCard.GetType().ToString())
            {
                case "RoomCard":
                    Debug.Log($"Adding {newCard.card.cardName} as a RoomCard");
                    cb.AddComponent<BattleRoomCard>();
                    cb.GetComponent<BattleRoomCard>().SetCard((RoomCard)newCard.card);
                    //cb.GetComponent<BattleRoomCard>().SetCard((RoomCard)newCard.Clone());
                    break;
                case string s when s.Contains("Spell"):
                    Debug.Log($"Adding {newCard.card.cardName} as a {newCard.GetType()}");
                    cb.AddComponent<BattleSpellCard>();
                    cb.GetComponent<BattleSpellCard>().SetCard((SpellCard)newCard.card);
                    //cb.GetComponent<BattleSpellCard>().SetCard((SpellCard)newCard.Clone());
                    break;
                case "CharacterCard":
                    Debug.Log($"Adding {newCard.card.cardName} as a CharacterCard");
                    cb.AddComponent<BattleCharCard>();
                    cb.GetComponent<BattleCharCard>().SetCard((CharacterCard)newCard.card);
                    //cb.GetComponent<BattleCharCard>().SetCard((CharacterCard) newCard.Clone());
                    break;
                default:
                    Debug.Log($"Adding {newCard.card.cardName} as a Unknown card type");
                    break;
            }
            Button selectButton = cb.GetComponent<Button>();
            selectButton.onClick.AddListener(() => AddToDeck(cb.GetComponent<BattleCard>()));
        }
    }
    void AddToDeck(BattleCard cardToAdd)
    {
        if (DeckManager.selectedDeck == null)
        {
            Debug.Log("No deck selected.");
            return;
        }
        DeckManager.selectedDeck.AddCard(cardToAdd);
    }
}
