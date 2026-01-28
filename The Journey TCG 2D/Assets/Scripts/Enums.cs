#region Deck Data
using System;

public enum CollectionName
{
    The_Beginning,
    Singing_Shadows,
    Flames_of_Fury,
    Frozen_Throne,
    Dark_Covenant
}
public enum DeckFormat
{
    Standard,
    Wild
}   
#endregion

#region CardInfo
public enum CardType
{
    Character,
    Spell,
    Room
}

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}



#endregion

#region CharacterStats
public enum Element
{
    Fire,
    Water,
    Earth,
    Air,
    Light,
    Dark
}

public enum CharacterType
{
    Goblin,
    Orc,
    Troll,
    Dragon,
    Undead
}

public enum StatusEffect
{
    Stunned,
    Poisoned,
    Burned,
    Frozen,
    Shielded,
    Weakened
}

[Serializable]
public enum CreatureRank
{
    Minion,
    Elite,
    Boss
}
#endregion

#region SpellAndAbility
public enum AbilityType
{
    Passive,
    Active,
    Triggered
}

[Serializable]
public enum TargetType
{
    SingleEnemy,
    RandomEnemy,
    AllEnemies,
    SingleAlly,
    RandomAlly,
    AllAllies,
    Self,
    Random,
    All,
    None
}

[Serializable]
public enum SpellEffectType
{
    Damage,
    Heal,
    Buff,
    Debuff,
    DrawCards,
    DiscardCards,
    Summon,
    Destroy
}
#endregion

#region RoomInfo
public enum RoomType
{
    Tavern,
    Blacksmith,
    Alchemist,
    Enchanter,
    GuildHall,
    None
}
#endregion

#region GameData
public enum GameState
{
    MainMenu,
    InGame,
    Paused,
    GameOver
}

public enum PlayerAction
{
    DrawCard,
    PlayCard,
    EndTurn,
    UseAbility
}


public enum InGamePhase 
{
    DrawPhase,
    FirstMainPhase, 
    BattlePhase, 
    SecondMainPhase,
    EndPhase
}

public enum ZoneType
{
    Deck,
    Hand,
    Field,
    Graveyard,
    Exile
}

#endregion
