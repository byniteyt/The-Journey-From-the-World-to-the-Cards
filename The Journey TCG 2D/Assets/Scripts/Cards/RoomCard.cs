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
        EventManager.SetActiveRoom?.Invoke(this, this);
    }
}
