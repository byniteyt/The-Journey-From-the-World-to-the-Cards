using System.Linq;
using UnityEngine;

public class Hand : MonoBehaviour
{
    Card[] cards;
    public static Hand Instance { get; private set; }
    public int handLimit;
    int currentCardCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        cards = new Card[handLimit];
        LoadEvents();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool AddCard(Card card)
    {
         if(currentCardCount<handLimit)
            {
                GameObject cardObject = Instantiate(card.gameObject);
                cards[currentCardCount] = cardObject.GetComponent<Card>();
                cardObject.transform.parent = this.transform;
                cardObject.transform.localPosition = new Vector3(-4 + currentCardCount*2, 0, 0);
                currentCardCount++;
                return true;
            }
         Debug.Log("Hand is full");
          return false;
    }
    public bool HasCard(Card card)
    {
        for(int i=0;i<currentCardCount;i++)
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
        if (battleground == null|| !battleground.GenerateCharacter(card))
        {
            Debug.Log("Failed to summon " + card.cardName + " to the battlefield.");
            return;
        }

        ManaTextManager.Instance.AddMana(-card.cost);
        Debug.Log("Summoning " + card.cardName + " to the battlefield.");
        int index = System.Array.IndexOf(cards, card);
        cards[index] = null;
        Destroy(card.gameObject);
        ReorganizeHand(index);
    }
    public void UseRoomCard(object sender, RoomCard card)
    {
        // Check if the card is in hand
        if (!Corrections(card)) return;
        ManaTextManager.Instance.AddMana(-card.cost);
        Debug.Log("Setting active room to " + card.cardName);
        int index = System.Array.IndexOf(cards, card);
        ReorganizeHand(index);
    }
    public void UseSpellCard(object sender, SpellCard card)
    {
        // Check if the card is in hand
        if (!Corrections(card)) return;
        ManaTextManager.Instance.AddMana(-card.cost);
        Debug.Log("Casting spell: " + card.cardName);
        int index = System.Array.IndexOf(cards, card);
        ReorganizeHand(index);
    }
    void LoadEvents()
    {
        EventManager.SetActiveRoom += UseRoomCard;
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
    void ReorganizeHand(int index)
    {
        cards[index] = null;
        for (int i = index; i < currentCardCount - 1; i++)
        {
            cards[i] = cards[i + 1];
            if (cards[i] != null)
                cards[i].transform.localPosition = new Vector3(-4 + i * 2, 0, 0);
        }
        cards[currentCardCount - 1] = null;
        currentCardCount--;
    }
}
