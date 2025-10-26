using UnityEngine;

public class ShopCard : MonoBehaviour
{
    public Card card;
    public int price;
    public void PurchaseCard()
    {
        CardCollection.AddCard(card);
        Debug.Log($"Purchased card: {card.cardName} for {price} coins.");
    }
}
