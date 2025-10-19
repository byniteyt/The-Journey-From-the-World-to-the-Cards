using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventHandler PlayCard;

    public static EventHandler<int> DrawCard;

    public static EventHandler StartTurn;

    public static EventHandler FirstMainTurn;

    public static EventHandler BattleTurn;

    public static EventHandler SecondMainTurn;

    public static EventHandler EndTurn;

    public static EventHandler<int> TakeDamage;

    public static EventHandler<int> DealDamage;

    public static EventHandler<int> HealDamage;

    public static EventHandler<int> ChangeLife;

    public static EventHandler<int> UpdateLife;

    public static EventHandler CombatTurn;
}
