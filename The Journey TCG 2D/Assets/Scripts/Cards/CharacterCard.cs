using UnityEngine;

public class CharacterCard : Card
{
    // Character specific attributes
    public int health;
    public int attack;

    protected override void Update()
    {
        base.Update();
        // Additional update logic for CharacterCard if needed
    }

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
    protected override bool IsShowingDetails()
    {
        return GameObject.Find("CharacterCardInfo(Clone)");
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
}
