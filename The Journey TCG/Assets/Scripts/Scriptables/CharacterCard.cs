using UnityEngine;

[CreateAssetMenu(fileName = "CharacterCard", menuName = "Cards/CharacterCard")]
public class CharacterCard : ScriptableObject
{
    public string characterName;
    public int health;
    public int attack;
}
