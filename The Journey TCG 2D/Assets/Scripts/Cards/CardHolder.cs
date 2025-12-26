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
        foreach (Card newCard in CardCollection.GetCollection().Keys)
        {
            GameObject cb = Instantiate(cardButton, cardHolder.transform);
            //cb.transform.GetChild(0).GetComponent<Image>().sprite = card.artwork;
            cb.GetComponentInChildren<TextMeshProUGUI>().text = CardCollection.GetCollection()[newCard].ToString();
            cb.gameObject.name = newCard.cardName;
            cb.transform.GetChild(0).GetComponent<Image>().sprite = newCard.artwork;
            cb.AddComponent<BoxCollider2D>();
            switch (newCard.GetType().ToString())
            {
                case "RoomCard":
                    Debug.Log($"Adding {newCard.cardName} as a RoomCard");
                    cb.AddComponent<BattleRoomCard>();
                    cb.GetComponent<BattleRoomCard>().SetCard((RoomCard)newCard.Clone());
                    break;
                case string s when s.Contains("Spell"):
                    Debug.Log($"Adding {newCard.cardName} as a {newCard.GetType()}");
                    cb.AddComponent<BattleSpellCard>();
                    cb.GetComponent<BattleSpellCard>().SetCard((SpellCard)newCard.Clone());
                    break;
                case "CharacterCard":
                    Debug.Log($"Adding {newCard.cardName} as a CharacterCard");
                    cb.AddComponent<BattleCharCard>();
                    cb.GetComponent<BattleCharCard>().SetCard((CharacterCard) newCard.Clone());
                    break;
                default:
                    Debug.Log($"Adding {newCard.cardName} as a Unknown card type");
                    break;
            }
            Button selectButton = cb.GetComponent<Button>();
            BattleCard newBattleCard = new BattleCard();
            newBattleCard.SetCard(newCard);
            selectButton.onClick.AddListener(() => AddToDeck(newBattleCard));
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
