using UnityEngine;

public class BattleCharCard : BattleCard
{
    [SerializeField] protected CharacterCard card;
    public override Card GetCard()
    {
        return (CharacterCard)card;
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
        card = (CharacterCard) card;
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
