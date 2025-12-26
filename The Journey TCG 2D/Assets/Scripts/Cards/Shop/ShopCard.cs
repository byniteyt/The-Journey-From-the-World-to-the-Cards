using System.Runtime.CompilerServices;
using UnityEngine;

public class ShopCard : MonoBehaviour
{
    public BattleCard card;
    public int price;
    public void PurchaseCard()
    {
        if (!PlayerSources.HasCoins(price))
        {
            Debug.Log("No tienes suficiente dinero");
            return;
        }
        EventManager.ChangeCoins.Invoke(this, -price);
        CardCollection.AddCard(card.GetCard());
        Debug.Log($"Compraste {card.GetCard().cardName} por {price} monedas.");
    }
}
