using UnityEngine;

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
            case TargetType.SingleEnemy:
                targetCard.GetComponent<CharacterCard>()?.ChangeHeal(effectAmount);
                break;
            case TargetType.AllEnemies:
                foreach (CharacterCard enemy in enemyBattleGround.transform)
                {
                    enemy.ChangeHeal(effectAmount);
                }
                break;
            case TargetType.RandomEnemy:
                if (enemyBattleGround.transform.childCount > 0)
                {
                    int randomIndex = Random.Range(0, enemyBattleGround.transform.childCount);
                    enemyBattleGround.transform.GetChild(randomIndex).GetComponent<CharacterCard>().ChangeHeal(effectAmount);
                }
                break;
                    
            case TargetType.SingleAlly:
                targetCard.GetComponent<CharacterCard>()?.ChangeHeal(effectAmount);
                break;
            case TargetType.AllAllies:
                foreach (CharacterCard ally in playerBattleGround.transform)
                {
                    ally.ChangeHeal(effectAmount);
                }
                break;
            case TargetType.RandomAlly:
                if (playerBattleGround.transform.childCount > 0)
                {
                    int randomIndex = Random.Range(0, playerBattleGround.transform.childCount);
                    playerBattleGround.transform.GetChild(randomIndex).GetComponent<CharacterCard>().ChangeHeal(effectAmount);
                }
                break;
            case TargetType.All:
                foreach (CharacterCard character in Object.FindObjectsByType<CharacterCard>(FindObjectsSortMode.None))
                {
                    character.ChangeHeal(effectAmount);
                }
                break;
            default:
                Debug.LogWarning("HealingSpell applied to unsupported target type: " + targetType);
                break;
        }
    }
}
