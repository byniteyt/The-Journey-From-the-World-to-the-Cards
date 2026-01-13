using System.Collections.Generic;
using UnityEngine;

public class BasePack : MonoBehaviour
{
    [SerializeField] protected string packName = "Base Pack";
    [SerializeField] protected int cardsAmount = 0;
    [SerializeField] protected int packPrice = 0;
    [SerializeField] protected CollectionName collectionName;
    Collection collection;
    protected List<BattleCard> cardsInPack = new();
    //GameObject ui;
    
    void Start()
    {
        collection = CollectionDataBase.GetDataBase().GetCollection(collectionName.ToString());
        /*ui =GameObject.Find("OpenningPack");
        ui.SetActive(false);
        for (int i = 0; i < cardsAmount; i++)
        {
            Card newCard = collection.GetRandomCard();
            cardsInPack.Add(newCard);
        }*/
    }

    #region Getters
    public string GetPackName()
    {
        return packName;
    }
    public int GetCardsAmount()
    {
        return cardsAmount;
    }
    public float GetPackPrice()
    {
        return packPrice;
    }
    public Collection GetCollection()
    {
        return collection;
    }
    public List<BattleCard> GetCardsInPack()
    {
        return cardsInPack;
    }
    #endregion

    public void OpenPack()
    {
        if (!PlayerSources.HasCoins(packPrice))
        {
            Debug.Log("No tienes suficiente dinero");
            return;
        }
        EventManager.ChangeCoins.Invoke(this, -packPrice);
        cardsInPack = new List<BattleCard>(); // Reiniciamos el sobre para que no se acumulen cartas si se abre varias veces
        Debug.Log($"Has abierto un {packName} de la colección {collectionName} que contiene {cardsAmount} cartas!!");
        /*foreach (Card card in cardsInPack)
        {
            Debug.Log($"Recibiste: {card.cardName}");
        }*/
        for (int i = 0; i < cardsAmount; i++)
        {
            BattleCard newCard = collection.GetRandomCard();
            cardsInPack.Add(newCard);
            Debug.Log($"Recibiste: {newCard.GetCard().cardName}");
        }
        GameObject ui = Resources.Load<GameObject>("Prefabs/UI/Shop/PackOpenedUI");
        Instantiate(ui,GameObject.Find("Canvas").transform);
        
        EventManager.OpenPack?.Invoke(this, this);
    }
}
