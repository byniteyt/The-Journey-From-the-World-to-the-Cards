using System.Linq;
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
        if (DeckManager.Instance.SelectedDeck == null)
        {
            Debug.Log("No deck selected.");
            return;
        }
        if (DeckManager.Instance.SelectedDeck.GetDeck().Count == 0)
        {
            Debug.Log("Selected deck has no cards.");
            return;
        }
        Debug.Log($"Deck count: {DeckManager.Instance.SelectedDeck.GetDeck().Count}");
        /*foreach (var c in DeckManager.Instance.SelectedDeck.GetDeck())
        {
            if (c.GetCard() == null)
                Debug.LogWarning($"Found BattleCard with null Card! Name: {c.name}");
            else
                Debug.Log($"BattleCard: {c.GetCard().cardName}");
        }*/
        foreach (var kvp in DeckManager.Instance.SelectedDeck.GetDictionary())
        {
            string cardName = kvp.Key;
            BattleCard card = DeckManager.Instance.SelectedDeck.GetDeck()
                                .FirstOrDefault(c => c.GetCard() != null && c.GetCard().cardName == cardName);
            if (card == null)
                Debug.LogError($"Cannot find card in deck with name {cardName}");
        }
        foreach (var kvp in DeckManager.Instance.SelectedDeck.GetDictionary())
        {
            string cardName = kvp.Key;
            int amount = kvp.Value;

            BattleCard card = DeckManager.Instance.SelectedDeck.GetCardByName(cardName);
            if (card == null) continue;

            GameObject cb = Instantiate(deckCardButton, deckCardHolder.transform);
            cb.GetComponentInChildren<TextMeshProUGUI>().text = amount.ToString();
            cb.name = cardName;
            cb.transform.GetChild(0).GetComponent<Image>().sprite = card.GetCard().artwork;
            cb.AddComponent<BoxCollider2D>();

            var cardData = card.GetCard();
            if (cardData is RoomCard room) { cb.AddComponent<BattleRoomCard>().SetCard((RoomCard)room.Clone()); }
            else if (cardData is SpellCard spell) { cb.AddComponent<BattleSpellCard>().SetCard((SpellCard)spell.Clone()); }
            else if (cardData is CharacterCard character) { cb.AddComponent<BattleCharCard>().SetCard((CharacterCard)character.Clone()); }
        }
    }

    void ShowCards(Deck deck)
    {
        foreach (string key in deck.GetDictionary().Keys)
        {
            Debug.Log("la baraja contiene de " + key + " " + deck.GetCardLimit(deck.GetCardByName(key)) + " copias ");
        }
    }
}
