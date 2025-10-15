using System;

public class RoomCard : Card
{
    // Room specific attributes
    public Element[] buffElements;
    public Element[] nerfElements;

    public CharacterType[] buffCharacters;
    public CharacterType[] nerfCharacters;

    override public void UpdateAsset()
    {
        base.UpdateAsset();
        // Add any RoomCard specific UI updates here if needed
    }
}
