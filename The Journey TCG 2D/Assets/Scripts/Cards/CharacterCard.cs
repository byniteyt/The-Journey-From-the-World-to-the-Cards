using System;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class CharacterCard : Card
{
    // Character specific attributes
    public int health;
    int maxHealth ;
    public int attack;

    public CreatureRank rank;

    public override Card Clone()
    {
        CharacterCard card = (CharacterCard) base.Clone();
        card.health = this.health;
        card.attack = this.attack;
        card.rank = this.rank;
        return card;
    }
    public void Start()
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

    public override void OnMouseDown()
    {

    }

    public override void UseCard()
    {
        // Implement character-specific behavior when the card is used
        Debug.Log("Using Character Card: " + cardName);
        // For example, summon the character to the battlefield
    }

    public override void ShowCardDetails()
    {
        base.ShowCardDetails();
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas.transform.Find("CharacterCardInfo(Clone)") == null&&
            canvas.transform.Find("CharacterCardInfo") == null)
        {
            GameObject panel = Object.Instantiate(Resources.Load<GameObject>("Prefabs/UI/Interfaces/CharacterCardInfo"), canvas.transform); ;
            panel.transform.SetAsLastSibling(); // Ensure the panel is on top
            panel.transform.localPosition = Vector3.zero; // Center the panel
        }
        GameObject cardDetailPanel = canvas.transform.Find("CharacterCardInfo(Clone)")? 
            canvas.transform.Find("CharacterCardInfo(Clone)").gameObject:
            canvas.transform.Find("CharacterCardInfo").gameObject;
        cardDetailPanel.SetActive(true);
        CharCardText info = cardDetailPanel.GetComponent<CharCardText>();
        info.cardToRead = this;
    }
    public void ChangeHealth(int amount)
    {
        //if (health==maxHealth && amount>0) return;
        health += amount;
        if (amount < 0)
        {
            Debug.Log(cardName + " took " + amount + " damage. Remaining health: " + Mathf.Max(0, health));
            if (health <= 0)
            {
                Debug.Log(cardName + " has been defeated!");
            }
            return;
        }
        Debug.Log(cardName + " heal " + amount + " points. Remaining health: " + health);
    }

    public void ChangeAttack(int amount)
    {
        attack = Math.Max(1, attack + amount);
        if (amount < 0)
        {
            Debug.Log(cardName + " lost " + amount + " attack. Current attack: " +  attack);
            return;
        }
        Debug.Log(cardName + " gained " + amount + " attack. Current attack: " + attack);
    }


}
