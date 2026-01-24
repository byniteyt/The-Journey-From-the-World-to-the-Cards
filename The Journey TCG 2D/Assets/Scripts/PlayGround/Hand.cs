using System;
using UnityEngine;

public class Hand : MonoBehaviour
{
    protected BattleCard[] hand;
    protected int actualHandSize = 0;
    [SerializeField] protected int handLimit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hand = new BattleCard[handLimit];
        LoadEvents();
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

    public void UseCharacterCard(BattleCharCard card)
    {
        // Check if the card is in hand
        if (!Corrections(card)) return;

        // Summon to battlefield
        BattlegroundArea battleground = GameObject.Find("PlayerBattleGround").GetComponent<BattlegroundArea>();
        if (battleground == null || !battleground.GenerateCharacter(card))
        {
            Debug.Log("Failed to summon " + card.GetCharacter().cardName + " to the battlefield.");
            return;
        }

        ManaTextManager.Instance.AddMana(-card.GetCharacter().cost);
        Debug.Log("Summoning " + card.GetCharacter().cardName + " to the battlefield.");
        int index = Array.IndexOf(hand, card);
        Destroy(card.gameObject);
        UpdateHand(index);
    }

    public void UseRoomCard(object sender, BattleRoomCard card)
    {
        // Check if the card is in hand
        if (!Corrections(card)) return;
        ManaTextManager.Instance.AddMana(-card.GetRoom().cost);
        Debug.Log("Setting active room to " + card.GetRoom().cardName);
        int index = Array.IndexOf(hand, card);
        //Destroy(card.gameObject);
        UpdateHand(index);
    }

    public void UseSpellCard(object sender, BattleSpellCard card)
    {
        // Check if the card is in hand
        if (!Corrections(card)) return;
        ManaTextManager.Instance.AddMana(-card.GetSpell().cost);
        Debug.Log("Casting spell: " + card.GetSpell().cardName);
        int index = Array.IndexOf(hand, card);
        UpdateHand(index);
    }
    
    bool Corrections(BattleCard card)
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
