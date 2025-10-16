using UnityEngine;

public class CharacterCard : Card
{
    // Character specific attributes
    public int health;
    public int attack;

    protected override void OnMouseDown()
    {
        Hand.Instance.UseCharacterCard(this);
    }

    protected override void UseCard()
    {
        // Implement character-specific behavior when the card is used
        Debug.Log("Using Character Card: " + cardName);
        // For example, summon the character to the battlefield
        Hand.Instance.UseCharacterCard(this);
    }
}
