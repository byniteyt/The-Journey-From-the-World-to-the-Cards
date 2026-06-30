using System;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class SpellCard : Card
{
    public int effectAmount;
    public TargetType targetType;
    [SerializeField]protected BattleCard targetCard;
    public SpellEffectType effect;

    // Regiones del campo de batalla
    protected GameObject playerBattleGround;
    protected GameObject enemyBattleGround;
    protected GameObject playerDeck;
    protected GameObject enemyDeck;
    protected GameObject playerHand;
    protected GameObject enemyHand;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public override Card Clone()
    {
        SpellCard spell = (SpellCard) base.Clone();
        spell.effectAmount = this.effectAmount;
        spell.targetType = this.targetType;
        spell.effect = this.effect;
        return spell;
    }

    override public void OnMouseDown()
    {
        if (this.targetType == TargetType.SingleAlly || this.targetType == TargetType.SingleEnemy)
        {
            Debug.Log("Seleccione un objetivo para: " + cardName);
            return;
        }
         UseCard();
    }
    public void SetTarget(BattleCard target)
    {
        targetCard = target;
    }
    override public void UseCard()
    {
        Debug.Log("Spell card played: " + cardName);
        // Implement spell effect here
        ApplyEffect();
    }
    override public void ShowCardDetails()
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject cardDetailPanel = Object.Instantiate(Resources.Load<GameObject>
            ("Prefabs/UI/Interfaces/SpellCardInfo"), canvas.transform);
        cardDetailPanel.transform.SetAsLastSibling(); // Ensure the panel is on top
        cardDetailPanel.transform.localPosition = Vector3.zero; // Center the panel
        SpellCardText info = cardDetailPanel.GetComponent<SpellCardText>();
        info.UpdatePanel(this);
    }
     public virtual void ApplyEffect()
    {
        switch(this.effect)
        {
            case SpellEffectType.Damage:
                Debug.Log($"Applying {effect} of {effectAmount}.");
                break;
            case SpellEffectType.Heal:
                Debug.Log($"Applying {effect} of {effectAmount}.");
                break;
            case SpellEffectType.Buff:
                Debug.Log($"Applying {effect} of {effectAmount}.");
                break;
            case SpellEffectType.Debuff:
                Debug.Log($"Applying {effect} of {effectAmount}.");
                break;
            case SpellEffectType.Summon:
                Debug.Log($"Applying {effect} of {effectAmount} to self");
                // Implement self-effect logic here
                break;
            case SpellEffectType.Destroy:
                Debug.Log($"Applying {effect} to target card {targetCard.GetCard().cardName}");
                // Implement destroy logic here
                break;
            case SpellEffectType.DiscardCards:
                break;
            case SpellEffectType.DrawCards:
                break;
        }
    }
     public virtual void ApplyEffectToTarget()
    {
        switch(this.effect)
        {
            case SpellEffectType.Damage:
                //Debug.Log($"Applying {effect} of {effectAmount} to target card {targetCard.GetCard().cardName}");
                ((BattleCharCard)targetCard).DamageCard(effectAmount);
                break;

            case SpellEffectType.Heal:
                //Debug.Log($"Applying {effect} of {effectAmount} to target card {targetCard.GetCard().cardName}");
                ((BattleCharCard)targetCard).GetCharacter().ChangeHealth(effectAmount);
                break;

            case SpellEffectType.Buff:
                //Debug.Log($"Applying {effect} of {effectAmount} to target card {targetCard.GetCard().cardName}");
                targetCard.GetCharacter().ChangeAttack(effectAmount);
                break;

            case SpellEffectType.Debuff:
                //Debug.Log($"Applying {effect} of {effectAmount} to target card {targetCard.GetCard().cardName}");
                targetCard.GetCharacter().ChangeAttack(effectAmount);
                break;

            case SpellEffectType.Summon:
                //Debug.Log($"Applying {effect} of {effectAmount} to self");
                // Implement self-effect logic here
                break;
            case SpellEffectType.Destroy:
                //Debug.Log($"Applying {effect} to target card {targetCard.GetCard().cardName}");
                // Implement destroy logic here
                break;
            case SpellEffectType.DiscardCards:
                break;
            case SpellEffectType.DrawCards:
                break;
            default:
                break;
        }
    }
    
    protected virtual void GetBattleZone()
    {
        playerBattleGround = GameObject.Find("PlayerBattleGround");
        enemyBattleGround = GameObject.Find("EnemyBattleGround");
        playerDeck = GameObject.Find("PlayerDeck");
        enemyDeck = GameObject.Find("EnemyDeck");
        playerHand = GameObject.Find("PlayerHand");
    }

    public GameObject GetPlayerHandZone()
    {
        return playerHand;
    }
    public GameObject GetPlayerBattleGroundZone()
    {
        if (playerBattleGround == null)
        {
            GetBattleZone();
        }
        return playerBattleGround;
    }
    public GameObject GetEnemyBattleGroundZone()
    {
        if (enemyBattleGround == null)
        {
            GetBattleZone();
        }
        return enemyBattleGround;
    }
    public GameObject GetPlayerDeckZone()
    {
        return playerDeck;
    }

    public GameObject GetEnemyDeckZone()
    {
        return enemyDeck;
    }

    public void CopyValues(SpellCard card)
    {
        this.cardName = card.cardName;
        this.description = card.description;
        this.cost = card.cost;
        this.artwork = card.artwork;
        this.effectAmount = card.effectAmount;
        this.targetType = card.targetType;
        this.effect = card.effect;
    }
}
