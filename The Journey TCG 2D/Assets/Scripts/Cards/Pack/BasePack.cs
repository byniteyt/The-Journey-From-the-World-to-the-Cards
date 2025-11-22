using System.Collections.Generic;
using UnityEngine;

public class BasePack : MonoBehaviour
{
    [SerializeField] protected string packName = "Base Pack";
    [SerializeField] protected int cardsAmount = 0;
    [SerializeField] protected float packPrice = 0.0f;
    [SerializeField] protected Collection collection;
    protected List<Card> cardsInPack = new List<Card>();
    //GameObject ui;
    
    void Start()
    {
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
    public List<Card> GetCardsInPack()
    {
        return cardsInPack;
    }
    #endregion

    public void OpenPack()
    {
        cardsInPack = new List<Card>(); // Reiniciamos el sobre para que no se acumulen cartas si se abre varias veces
        Debug.Log($"Has abierto un {packName} de la colección {collection} que contiene {cardsAmount} cartas!!");
        /*foreach (Card card in cardsInPack)
        {
            Debug.Log($"Recibiste: {card.cardName}");
        }*/
        for (int i = 0; i < cardsAmount; i++)
        {
            Card newCard = collection.GetRandomCard();
            cardsInPack.Add(newCard);
            Debug.Log($"Recibiste: {newCard.cardName}");
        }
        GameObject ui = Resources.Load<GameObject>("Prefabs/UI/Shop/PackOpenedUI");
        Instantiate(ui,GameObject.Find("Canvas").transform);
        //ui.SetActive(true);
        EventManager.OpenPack?.Invoke(this, this);
    }
}
