using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckCardHolder : MonoBehaviour
{
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

        // Recorremos todas las cartas en la baraja
        foreach (var kvp in DeckManager.Instance.SelectedDeck.GetDictionary())
        {
            string cardName = kvp.Key;
            int amount = kvp.Value;

            // Obtenemos el template de BattleCard
            Card templateBattleCard = DeckManager.Instance.SelectedDeck.GetDeck()
                .Find(c => c.cardName == cardName);

            if (templateBattleCard == null)
            {
                foreach (var c in DeckManager.Instance.SelectedDeck.GetDeck())
                {
                    Debug.Log($"Carta en el deck:{c.cardName} ");
                }
                Debug.LogWarning($"No se pudo encontrar BattleCard para {cardName}");
                continue;
            }

            Card cardData = templateBattleCard;
            if (cardData == null)
            {
                Debug.LogWarning($"Los datos de la carta son nulos para {cardName}");
                continue;
            }

            var deck = DeckManager.Instance.SelectedDeck;
            var deckDict = deck.GetDictionary();


            // Instanciar SOLO UNA VEZ
            GameObject cb = Instantiate(deckCardButton, deckCardHolder.transform);
            cb.name = cardName;

            // Mostrar cantidad de copias
            cb.GetComponentInChildren<TextMeshProUGUI>().text = amount.ToString();
            cb.transform.GetChild(0).GetComponent<Image>().sprite = cardData.artwork;
            cb.AddComponent<BoxCollider2D>();

            if (cardData is CharacterCard character)
            {
                cb.AddComponent<BattleCharCard>().SetCard(character);
            }
            else if (cardData is SpellCard spell)
            {
                cb.AddComponent<BattleSpellCard>().SetCard(spell);
            }
            else if (cardData is RoomCard room)
            {
                cb.AddComponent<BattleRoomCard>().SetCard(room);
            }
            else
            {
                Debug.LogWarning($"Tipo de carta desconocido: {cardData.GetType()}");
            }
        }
    }
}
