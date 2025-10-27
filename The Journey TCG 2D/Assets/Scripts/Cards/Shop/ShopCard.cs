using System.Runtime.CompilerServices;
using UnityEngine;

public class ShopCard : MonoBehaviour
{
    public Card card;
    public int price;
    public void PurchaseCard()
    {
        if (!PlayerSources.HasCoins(price))
        {
            Debug.Log("No tienes suficiente dinero");
            return;
        }
        EventManager.ChangeCoins.Invoke(this, -price);
        CardCollection.AddCard(card);
        Debug.Log($"Compraste {card.cardName} por {price} monedas.");
    }
}
