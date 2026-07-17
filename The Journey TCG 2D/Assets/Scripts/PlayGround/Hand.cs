using System;
using UnityEngine;

public class Hand : MonoBehaviour
{
    protected BattleCard[] hand;
    protected int actualHandSize = 0;
    [SerializeField] protected int handLimit;
    protected BattlegroundArea battleground;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hand = new BattleCard[handLimit];
        LoadEvents();
    }
    public BattleCard[] GetHand()
    {
        return hand;
    }
    public int GetHandAmount()
    {
        return actualHandSize;
    }

    public int GetHandSize()
    {
        return handLimit;
    }
    protected void UpdateHand(int index)
    {
        if (index <0) {
            for (int i = 0; i < actualHandSize; i++)
            {
                if (hand[i] != null)
                    hand[i].transform.localPosition = new Vector3(-actualHandSize + 0.5f + i * 2, 0, 0);
            }
            return;
        }
        for (int i = 0; i < index; i++)
        {
            if (hand[i] != null)
                hand[i].transform.localPosition = new Vector3(-(actualHandSize-1) + 0.5f + i * 2, 0, 0);
        }
        hand[index] = null;
        for (int i = index; i < actualHandSize - 1; i++)
        {
            hand[i] = hand[i + 1];
            if (hand[i] != null)
                hand[i].transform.localPosition = new Vector3(-(actualHandSize-1) + 0.5f + i * 2, 0, 0);
        }
        hand[actualHandSize - 1] = null;
        actualHandSize--;
    }
    protected virtual void LoadEvents()
    {
        
    }
    protected void ReorganizeHand(object sender, Card e)
    {
        int index = Array.IndexOf(hand, e);
        UpdateHand(index);
    }
    public virtual bool AddCard(BattleCard card)
    {
        GameObject cardObject = Instantiate(card.gameObject);
        hand[actualHandSize] = cardObject.GetComponent<BattleCard>();
        cardObject.transform.parent = this.transform;
        cardObject.transform.localPosition = new Vector3(-4 + actualHandSize * 2, 0, 0);
        
        actualHandSize++;
        UpdateHand(-1);
        return true;
    }

    public virtual void RemoveCard(BattleCard card)
    {
        for (int i = 0; i < hand.Length; i++)
        {
            if (hand[i] == card)
            {
                hand[i] = null;
                actualHandSize--;
                break;
            }
        }
    }


    public bool HasMoreCards(Hand otherHand)
    {
        return this.actualHandSize > otherHand.actualHandSize;
    }
    public bool HasCard(BattleCard card)
    {
        for (int i = 0; i < actualHandSize; i++)
        {
            if (hand[i] == card)
                return true;
        }
        return false;
    }

    public virtual void UseCharacterCard(BattleCharCard card)
    {
        if (battleground == null)
        {
            Debug.Log("Battleground is not set. Cannot summon character.");
            return;
        }
        // Summon to battlefield
        if (!battleground.GenerateCharacter(card))
        {
            Debug.Log("Failed to summon " + card.GetCharacter().cardName + " to the battlefield.");
            return;
        }
        Debug.Log("Summoning " + card.GetCharacter().cardName + " to the battlefield.");
        int index = Array.IndexOf(hand, card);
        UpdateHand(index);
    }

    public virtual void UseRoomCard(object sender, BattleRoomCard card)
    {
        Debug.Log("Setting active room to " + card.GetRoom().cardName);
        int index = Array.IndexOf(hand, card);
        //Destroy(card.gameObject);
        UpdateHand(index);
        EventManager.SetActiveRoom(this, card);
    }

    public virtual void UseSpellCard(object sender, BattleSpellCard card)
    {
        int index = Array.IndexOf(hand, card);
        UpdateHand(index);
    }
    
    protected bool Corrections(BattleCard card)
    {
        if (!HasCard(card))
        {
            Debug.Log("Card: " + card.GetCard().cardName + " hasn't found.");
            return false;
        }
        // Check if enough mana
        if (!ManaTextManager.Instance.IsEnoughMana(card.GetCard().cost))
        {
            Debug.Log("Not enough mana to play: " + card.GetCard().cardName);
            return false;
        }
        return true;
    }
}
