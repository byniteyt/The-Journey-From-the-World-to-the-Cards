using UnityEngine;

public class RoomCard : Card
{
    // Room specific attributes
    public Element[] buffElements;
    public Element[] nerfElements;

    public CharacterType[] buffCharacters;
    public CharacterType[] nerfCharacters;

    public void OnDestroyRoom()
    {
        Debug.Log($"Room {cardName} is being destroyed. Removing its effects.");
    }
    public override void UseCard()
    {
        if (GameManager.CurrentGameState != GameState.InGame||
            transform.parent.name== "RoomsArea")
            return;
        EventManager.SetActiveRoom?.Invoke(this, this);
    }
    public void CopyValues(RoomCard card)
    {
        this.cardName = card.cardName;
        this.description = card.description;
        this.cost = card.cost;
        this.artwork = card.artwork;
        this.buffElements = card.buffElements;
        this.nerfElements = card.nerfElements;
        this.buffCharacters = card.buffCharacters;
        this.nerfCharacters = card.nerfCharacters;
    }
}
