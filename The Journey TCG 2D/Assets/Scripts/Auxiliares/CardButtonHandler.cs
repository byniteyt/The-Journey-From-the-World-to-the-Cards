using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class CardButtonHandler : MonoBehaviour, IPointerClickHandler
{
    public Card card; // Card asociada a este botón
    public TextMeshProUGUI amountText; // referencia al texto de cantidad

    // Se llama cuando el botón recibe un click
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            // Clic izquierdo
            AddToDeck(card);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Clic derecho
            ShowCardDetails();
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
        Debug.Log($"Added {cardToAdd.cardName} to deck.");
    }

    void ShowCardDetails()
    {
        Debug.Log($"Showing details for {card.cardName}");
        // Aquí pones la lógica para mostrar detalles
        card.ShowCardDetails();
    }

    public void SetCard(Card c, int amount)
    {
        card = c;
        if (amountText != null)
            amountText.text = amount.ToString();
    }
}