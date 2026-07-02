using System;
using UnityEngine;
[Serializable]
[CreateAssetMenu(fileName = "Ability", menuName = "Ability")]
public class Ability : ScriptableObject
{
    #region Casting
    public virtual void OnSummon() { } // When the card enters the battlefield

    public virtual void OnPlay() { } // When the card is played
    #endregion

    #region Combat
    public virtual void OnAttack() { } // When the card attacks

    public virtual void OnHit() { } // When the card hits another target

    public virtual void OnDamage() { } // When the card takes damage
    #endregion

    #region Turns
    public virtual void OnTurnStart() { } // When the turn starts
    
    public virtual void OnFirstMain() { } // When the first main phase starts

    public virtual void OnBattleStart() { } // When the battle starts

    public virtual void OnSecondMain() { } // When the second main phase starts
    
    public virtual void OnTurnEnd() { } // When the turn ends
    #endregion

    #region Triggers
    public virtual void OnDraw() { } // When the playe draws a card
    #endregion

    public virtual void OnDeath() { } // When the card dies
}
