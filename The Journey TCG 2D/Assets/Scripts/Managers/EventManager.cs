using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region Combat Events
    public static EventHandler<int> TakeDamage;

    public static EventHandler<int> DealDamage;

    public static EventHandler<int> HealDamage;

    public static EventHandler<int> ChangeLife;

    public static EventHandler<int> UpdateLife;
    #endregion

    #region IA Events
    public static EventHandler EnemyTurn;

    public static EventHandler<Card> IAUseCardFromHand;

    public static EventHandler<CharacterCard> IAPlayCharacterCard;

    public static EventHandler<SpellCard> IAPlaySpellCard;

    public static EventHandler<RoomCard> IAPlayRoomCard;

    public static EventHandler<int> IADrawCard;

    public static EventHandler<int> IADiscardCard;

    public static EventHandler<RoomCard> IASetActiveRoom;
    #endregion

    #region Card Events
    public static EventHandler<Card> UseCardFromHand;

    public static EventHandler<CharacterCard> PlayCharacterCard;

    public static EventHandler<SpellCard> PlaySpellCard;

    public static EventHandler<RoomCard> PlayRoomCard;

    public static EventHandler<int> DrawCard;

    public static EventHandler<int> DiscardCard;

    public static EventHandler<RoomCard> SetActiveRoom;
    #endregion

    #region Deck Events
    public static EventHandler<Deck> CreateDeck;
    #endregion

    #region Turn Events
    public static EventHandler StartTurn;

    public static EventHandler FirstMainTurn;

    public static EventHandler BattleTurn;

    public static EventHandler SecondMainTurn;

    public static EventHandler EndTurn;

    public static EventHandler<bool> GameOver;

    public static EventHandler CombatTurn;
    #endregion

    #region IA Turn Events
    public static EventHandler StartIATurn;

    public static EventHandler FirstIAMainTurn;

    public static EventHandler IABattleTurn;

    public static EventHandler SecondIAMainTurn;

    public static EventHandler EndIATurn;

    public static EventHandler IACombatTurn;
    #endregion

    #region Profile Events
    public static EventHandler<int> SetPlayerLevel;
    public static EventHandler<int> AddPlayerExp;
    public static EventHandler<int> ChangeCoins;
    #endregion

    #region MainMenu Events
    public static EventHandler OpenMainMenu;
    public static EventHandler CloseMainMenu;
    #endregion

    #region Shop Events
    //public static EventHandler<PackShopItem> BuyPack;
    public static EventHandler<BasePack> OpenPack;
    #endregion

}
