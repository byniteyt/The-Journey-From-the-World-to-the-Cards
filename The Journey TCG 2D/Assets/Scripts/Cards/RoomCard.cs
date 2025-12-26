using System;
using UnityEngine;

[Serializable]
public class RoomCard : Card
{
    // Room specific attributes
    public Element[] buffElements;
    public Element[] nerfElements;

    public CharacterType[] buffCharacters;
    public CharacterType[] nerfCharacters;

    public override Card Clone()
    {
        return (RoomCard)this.MemberwiseClone();
    }

    public void OnDestroyRoom()
    {
        Debug.Log($"Room {cardName} is being destroyed. Removing its effects.");
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
