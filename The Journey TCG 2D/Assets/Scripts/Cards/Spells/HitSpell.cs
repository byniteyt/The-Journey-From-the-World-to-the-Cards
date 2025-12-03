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

    protected override void ApplyEffect()
    {
        Debug.Log("HitSpell effect applied: " + cardName);
        switch (targetType)
        {
            case TargetType.AllEnemies:
                foreach (CharacterCard enemy in enemyBattleGround.transform)
                {
                    enemy.ChangeHeal(-effectAmount);
                }
                break;

            case TargetType.RandomEnemy:
                if (enemyBattleGround.transform.childCount > 0)
                {
                    int randomIndex = Random.Range(0, enemyBattleGround.transform.childCount);
                    enemyBattleGround.transform.GetChild(randomIndex).GetComponent<CharacterCard>().ChangeHeal(-effectAmount);
                }
                break;

            case TargetType.AllAllies:
                foreach (Transform ally in playerBattleGround.transform)
                {
                    ally.GetComponent<CharacterCard>().ChangeHeal(-effectAmount);
                }
                break;

            case TargetType.RandomAlly:
                if (playerBattleGround.transform.childCount > 0)
                {
                    int randomIndex = Random.Range(0, playerBattleGround.transform.childCount);
                    playerBattleGround.transform.GetChild(randomIndex).GetComponent<CharacterCard>().ChangeHeal(-effectAmount);
                }
                break;

            case TargetType.All:
                foreach (CharacterCard character in Object.FindObjectsByType<CharacterCard>(FindObjectsSortMode.None))
                {
                    character.ChangeHeal(-effectAmount);
                }
                break;
            default:
                Debug.Log("This spell must be dragged");
                return;
        }
        EventManager.UseCardFromHand?.Invoke(this, this);
        Destroy(this.gameObject);
    }
    
    protected override void ApplyEffectToTarget()
    {
        if (targetCard is CharacterCard character)
        {
            character.ChangeHeal(-effectAmount);
        }
        else
        {
            Debug.Log("Invalid target for HitSpell: " + targetCard.cardName);
        }
        Destroy(this.gameObject);
    }
}
