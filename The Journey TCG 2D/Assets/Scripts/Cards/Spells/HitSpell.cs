using UnityEngine;

public class HitSpell : SpellCard
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.GetBattleZone();
        effect = SpellEffectType.Damage;
    }
    public override Card Clone()
    {
        HitSpell healCard = (HitSpell)base.Clone();
        return healCard;
    }

    public override void ApplyEffect()
    {
        Debug.Log("HitSpell effect applied: " + cardName);
        switch (targetType)
        {
            case TargetType.AllEnemies:
                foreach (CharacterCard enemy in enemyBattleGround.transform)
                {
                    enemy.ChangeHealth(-effectAmount);
                }
                break;

            case TargetType.RandomEnemy:
                if (enemyBattleGround.transform.childCount > 0)
                {
                    int randomIndex = Random.Range(0, enemyBattleGround.transform.childCount);
                    enemyBattleGround.transform.GetChild(randomIndex).GetComponent<BattleCharCard>().GetCharacter().ChangeHealth(-effectAmount);
                }
                break;

            case TargetType.AllAllies:
                foreach (Transform ally in playerBattleGround.transform)
                {
                    ally.GetComponent<CharacterCard>().ChangeHealth(-effectAmount);
                }
                break;

            case TargetType.RandomAlly:
                if (playerBattleGround.transform.childCount > 0)
                {
                    int randomIndex = Random.Range(0, playerBattleGround.transform.childCount);
                    playerBattleGround.transform.GetChild(randomIndex).GetComponent<CharacterCard>().ChangeHealth(-effectAmount);
                }
                break;

            case TargetType.All:
                foreach (BattleCharCard character in Object.FindObjectsByType<BattleCharCard>(FindObjectsSortMode.None))
                {
                    character.GetCharacter().ChangeHealth(-effectAmount);
                }
                break;
            default:
                Debug.Log("This spell must be dragged");
                return;
        }
        EventManager.UseCardFromHand?.Invoke(this, this);
    }

    public override void ApplyEffectToTarget()
    {
        if (targetCard is BattleCharCard character)
        {
            character.GetCharacter().ChangeHealth(-effectAmount);
        }
        else
        {
            Debug.Log("Invalid target for HitSpell: " + targetCard.GetCharacter().cardName);
        }
    }
}
