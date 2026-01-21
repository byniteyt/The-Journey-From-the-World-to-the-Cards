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
            switch (newCard.card.GetType().ToString())
            {
                case "BattleRoomCard":
                    Debug.Log($"Adding {newCard.card.GetCard().cardName} as a RoomCard\n" +
                        $"La carta vale {newCard.card.GetCard().cost} y su descripcion es: \n" +
                        $"\\ {newCard.card.GetCard().description}");
                    cb.AddComponent<BattleRoomCard>();
                    cb.GetComponent<BattleRoomCard>().SetCard(newCard.card.GetRoom());
                    //cb.GetComponent<BattleRoomCard>().SetCard((RoomCard)newCard.Clone());
                    break;
                case string s when s.Contains("Spell"):
                    Debug.Log($"Adding {newCard.card.GetCard().cardName} as a {newCard.card.GetType()}");
                    cb.AddComponent<BattleSpellCard>();
                    cb.GetComponent<BattleSpellCard>().SetCard(newCard.card.GetSpell());
                    //cb.GetComponent<BattleSpellCard>().SetCard((SpellCard)newCard.Clone());
                    break;
                case "BattleCharCard":
                    Debug.Log($"Adding {newCard.card.GetCard().cardName} as a CharacterCard.\n" +
                        $"La carta vale {newCard.card.GetCard().cost} y su descripcion es: \n" +
                        $"\\ {newCard.card.GetCard().description}");
                    cb.AddComponent<BattleCharCard>();
                    cb.GetComponent<BattleCharCard>().SetCard(newCard.card.GetCharacter());
                    break;
                case "Card":
                    Debug.Log($"Adding {newCard.card.GetCard().cardName} as a Card");
                    break; 
                default:
                    Debug.Log($"Adding {newCard.card.GetCard().cardName} as a {newCard.card.GetType()} card type");
                    break;
            }
            Debug.Log("Se accede al botón");
            cb.GetComponent<Button>().onClick.AddListener(() => AddToDeck(cb.GetComponent<BattleCard>()));
        }
    }
    void AddToDeck(BattleCard cardToAdd)
    {
        if (DeckManager.Instance.SelectedDeck == null)
        {
            Debug.Log("No deck selected.");
            return;
        }
        DeckManager.Instance.SelectedDeck.AddCard(cardToAdd);
    }
}
