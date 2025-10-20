using System.Linq;
using UnityEngine;

public class Hand : MonoBehaviour
{
    Card[] cards;
    public static Hand Instance { get; private set; }
    public int handLimit = 5;
    int currentCardCount = 0;
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
                TimeWaiter.WaitFor(0.2f);
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
        if (!HasCard(card))
        {
            Debug.Log("Card: " + card.cardName + " hasn't found.");
            return;
        }

        // Check if enough mana
        if (!ManaTextManager.Instance.IsEnoughMana(card.cost))
        {
            Debug.Log("Not enough mana to play: " + card.cardName);
            return;
        }

        // Summon to battlefield
        BattlegroundArea battleground = GameObject.Find("PlayerBattleGround").GetComponent<BattlegroundArea>();
        if (battleground == null|| !battleground.GenerateCharacter(card))
        {
            Debug.Log("Failed to summon " + card.cardName + " to the battlefield.");
            return;
        }


        ManaTextManager.Instance.ChangeMana(-card.cost);
        Debug.Log("Summoning " + card.cardName + " to the battlefield.");
        int index = System.Array.IndexOf(cards, card);
        cards[index] = null;
        Destroy(card.gameObject);
        for(int i=index;i<currentCardCount-1;i++)
        {
            cards[i] = cards[i + 1];
            if(cards[i]!=null)
                cards[i].transform.localPosition = new Vector3(-4 + i * 2, 0, 0);
        }
        cards[currentCardCount - 1] = null;
        currentCardCount--;
    }
    public void UseRoomCard(object sender, RoomCard card)
    {
        // Check if the card is in hand
        if (!HasCard(card))
        {
            Debug.Log("Card: " + card.cardName + " hasn't found.");
            return;
        }
        // Check if enough mana
        if (!ManaTextManager.Instance.IsEnoughMana(card.cost))
        {
            Debug.Log("Not enough mana to play: " + card.cardName);
            return;
        }
        // Set active room
        ManaTextManager.Instance.ChangeMana(-card.cost);
        Debug.Log("Setting active room to " + card.cardName);
        int index = System.Array.IndexOf(cards, card);
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
    void LoadEvents()
    {
        EventManager.SetActiveRoom += UseRoomCard;
    }
}
