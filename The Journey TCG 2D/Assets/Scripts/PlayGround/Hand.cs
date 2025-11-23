using System;
using UnityEngine;

public class Hand : MonoBehaviour
{
    protected Card[] cards;
    protected int currentCardCount = 0;
    [SerializeField] protected int handLimit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cards = new Card[handLimit];
        LoadEvents();
    }
    public int GetHandAmount()
    {
        return currentCardCount;
    }

    public int GetHandSize()
    {
        return handLimit;
    }
    protected void UpdateHand(int index)
    {
        if (index <0) {
            for (int i = 0; i < currentCardCount; i++)
            {
                if (cards[i] != null)
                    cards[i].transform.localPosition = new Vector3(-currentCardCount + 0.5f + i * 2, 0, 0);
            }
            return;
        }
        for (int i = 0; i < index; i++)
        {
            if (cards[i] != null)
                cards[i].transform.localPosition = new Vector3(-(currentCardCount-1) + 0.5f + i * 2, 0, 0);
        }
        cards[index] = null;
        for (int i = index; i < currentCardCount - 1; i++)
        {
            cards[i] = cards[i + 1];
            if (cards[i] != null)
                cards[i].transform.localPosition = new Vector3(-(currentCardCount-1) + 0.5f + i * 2, 0, 0);
        }
        cards[currentCardCount - 1] = null;
        currentCardCount--;
    }
    protected virtual void LoadEvents()
    {
        
    }
    protected void ReorganizeHand(object sender, Card e)
    {
        int index = Array.IndexOf(cards, e);
        UpdateHand(index);
    }
    public bool AddCard(Card card)
    {
        GameObject cardObject = Instantiate(card.gameObject);
        cards[currentCardCount] = cardObject.GetComponent<Card>();
        cardObject.transform.parent = this.transform;
        cardObject.transform.localPosition = new Vector3(-4 + currentCardCount * 2, 0, 0);
        
        currentCardCount++;
        UpdateHand(-1);
        return true;
    }

    public bool HasCard(Card card)
    {
        for (int i = 0; i < currentCardCount; i++)
        {
            if (cards[i] == card)
                return true;
        }
        return false;
    }

    public void UseCharacterCard(CharacterCard card)
    {
        // Check if the card is in hand
        if (!Corrections(card)) return;

        // Summon to battlefield
        BattlegroundArea battleground = GameObject.Find("PlayerBattleGround").GetComponent<BattlegroundArea>();
        if (battleground == null || !battleground.GenerateCharacter(card))
        {
            Debug.Log("Failed to summon " + card.cardName + " to the battlefield.");
            return;
        }

        ManaTextManager.Instance.AddMana(-card.cost);
        Debug.Log("Summoning " + card.cardName + " to the battlefield.");
        int index = Array.IndexOf(cards, card);
        Destroy(card.gameObject);
        UpdateHand(index);
    }

    public void UseRoomCard(object sender, RoomCard card)
    {
        // Check if the card is in hand
        if (!Corrections(card)) return;
        ManaTextManager.Instance.AddMana(-card.cost);
        Debug.Log("Setting active room to " + card.cardName);
        int index = Array.IndexOf(cards, card);
        //Destroy(card.gameObject);
        UpdateHand(index);
    }

    public void UseSpellCard(object sender, SpellCard card)
    {
        // Check if the card is in hand
        if (!Corrections(card)) return;
        ManaTextManager.Instance.AddMana(-card.cost);
        Debug.Log("Casting spell: " + card.cardName);
        int index = Array.IndexOf(cards, card);
        UpdateHand(index);
    }
    bool Corrections(Card card)
    {
        if (!HasCard(card))
        {
            Debug.Log("Card: " + card.cardName + " hasn't found.");
            return false;
        }
        // Check if enough mana
        if (!ManaTextManager.Instance.IsEnoughMana(card.cost))
        {
            Debug.Log("Not enough mana to play: " + card.cardName);
            return false;
        }
        return true;
    }
}
