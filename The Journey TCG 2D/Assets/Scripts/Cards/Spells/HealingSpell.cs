using UnityEngine;
using UnityEngine.EventSystems;

public class HealingSpell : SpellCard
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.GetBattleZone();
        effect = SpellEffectType.Heal;
    }

    public override Card Clone()
    {
        HealingSpell healCard = (HealingSpell) base.Clone();
        return healCard;
    }

    public override void ApplyEffect()
    {
        Debug.Log("HealingSpell effect applied: " + cardName);
        switch(targetType)
        {
            case TargetType.AllEnemies:
                if (enemyBattleGround.transform.childCount == 0) return;
                foreach (CharacterCard enemy in enemyBattleGround.transform)
                {
                    enemy.ChangeHealth(effectAmount);
                }
                break;
            case TargetType.RandomEnemy:
                if (enemyBattleGround.transform.childCount == 0) return;
                int randomIndex = Random.Range(0, enemyBattleGround.transform.childCount);
                enemyBattleGround.transform.GetChild(randomIndex).GetComponent<BattleCharCard>().GetCharacter().ChangeHealth(effectAmount);
                break;
                    
            case TargetType.AllAllies:
                if (playerBattleGround.transform.childCount == 0) return;
                foreach (CharacterCard ally in playerBattleGround.transform)
                {
                    ally.ChangeHealth(effectAmount);
                }
                break;
            case TargetType.RandomAlly:
                if (playerBattleGround.transform.childCount == 0) return;
                randomIndex = Random.Range(0, playerBattleGround.transform.childCount);
                playerBattleGround.transform.GetChild(randomIndex).
                    GetComponent<BattleCharCard>().GetCharacter().ChangeHealth(effectAmount);
                break;
            case TargetType.All:
                if (playerBattleGround.transform.childCount == 0&& 
                    enemyBattleGround.transform.childCount == 0) return;
                foreach (BattleCharCard character in Object.FindObjectsByType<BattleCharCard>(FindObjectsSortMode.None))
                {
                    character.GetCharacter().ChangeHealth(effectAmount);
                }
                break;
            default:
                Debug.LogWarning("HealingSpell applied to unsupported target type: " + targetType);
                return;
        }
        EventManager.UseCardFromHand?.Invoke(this, this);
    }

    public override void OnMouseDown()
    {
        base.OnMouseDown();
    }
    public override void ApplyEffectToTarget()
    {
        if (targetCard is BattleCharCard character)
        {
            character.GetCharacter().ChangeHealth(effectAmount);
        }
        else
        {
            Debug.Log("Invalid target for HealSpell: " + targetCard.GetCharacter().cardName);
        }
    }
}
