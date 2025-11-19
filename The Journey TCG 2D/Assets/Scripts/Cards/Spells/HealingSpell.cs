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

    protected override void ApplyEffect()
    {
        Debug.Log("HealingSpell effect applied: " + cardName);
        switch(targetType)
        {
            case TargetType.AllEnemies:
                if (enemyBattleGround.transform.childCount == 0) return;
                foreach (CharacterCard enemy in enemyBattleGround.transform)
                {
                    enemy.ChangeHeal(effectAmount);
                }
                break;
            case TargetType.RandomEnemy:
                if (enemyBattleGround.transform.childCount == 0) return;
                int randomIndex = Random.Range(0, enemyBattleGround.transform.childCount);
                enemyBattleGround.transform.GetChild(randomIndex).GetComponent<CharacterCard>().ChangeHeal(effectAmount);
                break;
                    
            case TargetType.AllAllies:
                if (playerBattleGround.transform.childCount == 0) return;
                foreach (CharacterCard ally in playerBattleGround.transform)
                {
                    ally.ChangeHeal(effectAmount);
                }
                break;
            case TargetType.RandomAlly:
                if (playerBattleGround.transform.childCount == 0) return;
                randomIndex = Random.Range(0, playerBattleGround.transform.childCount);
                playerBattleGround.transform.GetChild(randomIndex).
                    GetComponent<CharacterCard>().ChangeHeal(effectAmount);
                break;
            case TargetType.All:
                if (playerBattleGround.transform.childCount == 0&& 
                    enemyBattleGround.transform.childCount == 0) return;
                foreach (CharacterCard character in Object.FindObjectsByType<CharacterCard>(FindObjectsSortMode.None))
                {
                    character.ChangeHeal(effectAmount);
                }
                break;
            default:
                Debug.LogWarning("HealingSpell applied to unsupported target type: " + targetType);
                return;
        }
        EventManager.UseCardFromHand?.Invoke(this, this);
        Destroy(this.gameObject);
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
    }
    protected override void ApplyEffectToTarget()
    {
        if (targetCard is CharacterCard character)
        {
            character.ChangeHeal(-effectAmount);
        }
        else
        {
            Debug.Log("Invalid target for HealSpell: " + targetCard.cardName);
        }
        Destroy(this.gameObject);
    }
}
