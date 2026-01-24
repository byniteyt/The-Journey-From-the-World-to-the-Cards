using System;
using UnityEngine;

[Serializable]
public class BattleCharCard : BattleCard
{
    [SerializeField] protected CharacterCard card = new();

    public BattleCharCard(CharacterCard newCard)
    {
        card = newCard;
    }
    public override Card GetCard()
    {
        if (card == null)
        {
            Debug.LogWarning("Tropa no tiene carta");
            return null;
        }
        return (CharacterCard)card;
    }
    public override CharacterCard GetCharacter()
    {
        return card;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update() 
    {
        base.Update();

    }

    public override void SetCard(Card card)
    {
        this.card = (CharacterCard) card;
    }

    public override void ShowCardDetails()
    {

    }

    public override void UseCard()
    {
        throw new System.NotImplementedException();
    }
    private void OnMouseDown()
    {
        PlayerHand.GetPlayerHand().UseCharacterCard(this);
    }
}
