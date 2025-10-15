using UnityEngine;

public class CharacterCard : Card
{
    // Character specific attributes
    public int health;
    public int attack;

    protected override void OnMouseDown()
    {
        if (ManaTextManager.Instance.ChangeMana(-cost))
        {
            Debug.Log("Character Card played: " + cardName + " with Attack: " + attack + " and Health: " + health);
            UseCard();
            //Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Not enough mana to play: " + cardName);
        }
        
    }

    protected override void UseCard()
    {
        // Implement character-specific behavior when the card is used
        Debug.Log("Using Character Card: " + cardName);
        // For example, summon the character to the battlefield
        Hand.Instance.RemoveCard(this);
    }
}
