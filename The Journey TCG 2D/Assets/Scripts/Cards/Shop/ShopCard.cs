using UnityEngine;

public class ShopCard : MonoBehaviour
{
    public Card card;
    public int price;
    public void PurchaseCard()
    {
        CardCollection.AddCard(card);
        Debug.Log($"Compraste {card.cardName} por {price} monedas.");
    }
}
