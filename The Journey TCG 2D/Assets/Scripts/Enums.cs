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

public enum Element
{
    Fire,
    Water,
    Earth,
    Air,
    Light,
    Dark
}

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

public enum CharacterType
{
    Goblin,
    Orc,
    Troll,
    Dragon,
    Undead
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

public enum TurnPlayer
{
    Player,
    Opponent
}

public enum AbilityType
{
    Passive,
    Active,
    Triggered
}

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
