using UnityEngine;

public class BattleRoomCard : BattleCard
{
    [SerializeField] protected RoomCard card = new RoomCard();

    public BattleRoomCard(RoomCard newCard)
    {
        card = newCard;
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
        EventManager.SetActiveRoom?.Invoke(this, this);
    }

    public override void ShowCardDetails()
    {
        throw new System.NotImplementedException();
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
}
