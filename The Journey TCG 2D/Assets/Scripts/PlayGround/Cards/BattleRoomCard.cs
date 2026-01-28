using System;
using UnityEngine;

[Serializable]
public class BattleRoomCard : BattleCard
{
    [SerializeField] protected RoomCard card = new();

    public BattleRoomCard(RoomCard newCard)
    {
        card = newCard;
    }

    public override RoomCard GetRoom()
    {
        return card;
    }
    public override Card GetCard()
    {
        if (card == null)
        {
            Debug.LogWarning("Casa no tiene carta");
            return null;
        }
        return (RoomCard) card;
    }

    public override void SetCard(Card card)
    {
        this.card = (RoomCard) card;    
    }

    public override void UseCard()
    {
        if (GameManager.CurrentGameState != GameState.InGame ||
            transform.parent.name == "RoomsArea")
            return;
        PlayerHand.GetPlayerHand().UseRoomCard(this, this);
    }

    public override void ShowCardDetails()
    {
        throw new System.NotImplementedException();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    private void OnMouseDown()
    {
        UseCard();
    }
}
