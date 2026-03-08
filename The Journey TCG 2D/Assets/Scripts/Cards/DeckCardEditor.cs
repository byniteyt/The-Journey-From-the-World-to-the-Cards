using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckCardEditor : MonoBehaviour
{
    GameObject cardButton;
    GameObject cardHolder;


    private void Start()
    {
        cardHolder = GameObject.Find("CardHolder");
        cardButton = Resources.Load<GameObject>("Prefabs/Cards/CollectedCard");
        if (Player.GetPlayer().Properties().GetCards().Count == 0) 
            Debug.Log("No hay cartas guardadas para mostrar");
        if(Player.GetPlayer().Properties().GetCards().Count == 0) Debug.Log("No hay cartas guardadas para mostrar");
        foreach (CardAmount newCardId in Player.GetPlayer().Properties().GetCards())
        {
            Card newCard = CardDataBase.Instance.GetCard(newCardId.cardName);
            if (newCard == null)
            {
                Debug.LogError($"Card {newCardId.cardName} not found in CardDataBase.");
                continue;
            }

            GameObject cb = Instantiate(cardButton, cardHolder.transform);
            Debug.Log($"Instantiated card button for {newCard.cardName} with amount {newCardId.amount}");

            //cb.transform.GetChild(0).GetComponent<Image>().sprite = card.artwork;
            cb.GetComponentInChildren<TextMeshProUGUI>().text = newCardId.amount.ToString();
            cb.name = newCard.cardName;
            if (newCard ==null)
            {
                Debug.LogError("Card is null in CardsReceived.");
                continue;
            }   
            cb.transform.GetChild(0).GetComponent<Image>().sprite = newCard.artwork;
            cb.AddComponent<BoxCollider2D>();
            string nameOfCard = cb.name;
            switch (newCard.GetType().ToString())
            {
                case "BattleRoomCard":
                    cb.AddComponent<BattleRoomCard>();
                    cb.GetComponent<BattleRoomCard>().SetCard(newCard);
                    Debug.Log($"Adding {cb.name}  with the card {newCard.cardName} as a Room");
                    nameOfCard = newCard.cardName;
                    break;
                case string s when s.Contains("Spell"):
                    cb.AddComponent<BattleSpellCard>();
                    cb.GetComponent<BattleSpellCard>().SetCard(newCard);
                    Debug.Log($"Adding {cb.name}  with the card {newCard.cardName} as a Spell");
                    nameOfCard = newCard.cardName;
                    break;
                case "BattleCharCard":
                    cb.AddComponent<BattleCharCard>();
                    cb.GetComponent<BattleCharCard>().SetCard(newCard);
                    Debug.Log($"Adding {cb.name}  with the card {newCard.cardName} as a Character");
                    nameOfCard = newCard.cardName;
                    break;
                case "Card":
                    Debug.Log($"Adding {newCard.cardName} as a Card");
                    break;
                default:
                    Debug.Log($"Adding {newCard.cardName} as a {newCard.GetType()} card type");
                    break;
            }
            cb.GetComponent<Button>().onClick.AddListener(() => {
                Debug.Log($"Adding {nameOfCard} to deck.");
                if (Player.GetPlayer().Properties().GetCard(nameOfCard) == null)
                {
                    Debug.LogError($"Card {nameOfCard} not found in player properties.");
                    return;
                }
                AddToDeck(Player.GetPlayer().Properties().GetCard(nameOfCard));
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
