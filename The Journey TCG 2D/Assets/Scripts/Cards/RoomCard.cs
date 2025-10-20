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
    protected override void UseCard()
    {
        if (GameManager.CurrentGameState != GameState.InGame||
            transform.parent.name== "RoomsArea")
            return;
        EventManager.SetActiveRoom?.Invoke(this, this);
    }
}
