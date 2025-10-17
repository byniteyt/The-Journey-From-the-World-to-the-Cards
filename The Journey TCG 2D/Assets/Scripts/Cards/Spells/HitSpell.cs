using UnityEngine;

public class HitSpell : SpellCard
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.GetBattleZone();
        effect = SpellEffectType.Damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected override void ApplyEffect()
    {
        Debug.Log("HitSpell effect applied: " + cardName);
        switch (targetType)
        {
            case TargetType.SingleEnemy:
                targetCard.GetComponent<CharacterCard>()?.ChangeHeal(-effectAmount);
                break;
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

            case TargetType.SingleAlly:
                targetCard.GetComponent<CharacterCard>()?.ChangeHeal(-effectAmount);
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
                Debug.LogWarning("HealingSpell applied to unsupported target type: " + targetType);
                break;
        }
    }
    protected override void OnMouseDrag()
    {
        transform.position = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            transform.localPosition.z
        );
    }
    protected override void OnMouseUp()
    {
        if (targetType == TargetType.SingleEnemy || targetType == TargetType.SingleAlly)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            if (hit.collider != null)
            {
                if (hit.collider.transform.IsChildOf(enemyBattleGround.transform)&&targetType == TargetType.SingleEnemy||
                    hit.collider.transform.IsChildOf(playerBattleGround.transform) && targetType == TargetType.SingleAlly)
                {
                    Card hitCard = hit.collider.GetComponent<Card>();
                    if (hitCard != null)
                    {
                        targetCard = hitCard;
                        ApplyEffect();
                        Debug.Log("Target selected: " + targetCard.cardName);
                    }
                    return;
                }
                
            }
        }
        // Return the card to its original position
        transform.position = originalPosition;
    }
}
