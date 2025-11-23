using UnityEngine;

public class CharacterCard : Card
{
    // Character specific attributes
    public int health;
    int maxHealth ;
    public int attack;

    public CreatureRank rank;
    void Start()
    {
        maxHealth = health;
    }
    protected override void Update()
    {
        base.Update();
        // Additional update logic for CharacterCard if needed
    }
    public void CopyValues(CharacterCard card)
    {
        this.cardName = card.cardName;
        this.description = card.description;
        this.cost = card.cost;
        this.artwork = card.artwork;
        this.health = card.health;
        this.maxHealth = card.health;
        this.attack = card.attack;
    }

    protected override void OnMouseDown()
    {
        PlayerHand.Instance.UseCharacterCard(this);
    }

    protected override void UseCard()
    {
        // Implement character-specific behavior when the card is used
        Debug.Log("Using Character Card: " + cardName);
        // For example, summon the character to the battlefield
        PlayerHand.Instance.UseCharacterCard(this);
    }
        
    protected override void ShowCardDetails()
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject cardDetailPanel = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Interfaces/CharacterCardInfo"), canvas.transform);
        cardDetailPanel.transform.SetAsLastSibling(); // Ensure the panel is on top
        cardDetailPanel.transform.localPosition = Vector3.zero; // Center the panel
        CharCardText info = cardDetailPanel.GetComponent<CharCardText>();
        info.cardToRead = this;
    }
    public void ChangeHeal(int amount)
    {
        //if (health==maxHealth && amount>0) return;
        health += amount;
        if (amount < 0)
        {
            Debug.Log(cardName + " took " + amount + " damage. Remaining health: " + Mathf.Max(0, health));
            if (health <= 0)
            {
                Debug.Log(cardName + " has been defeated!");
                Destroy(this.gameObject);
            }
            return;
        }
        Debug.Log(cardName + " heal " + amount + " points. Remaining health: " + Mathf.Min(maxHealth,health));
    }
}
