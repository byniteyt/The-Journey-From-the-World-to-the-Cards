using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class BattleSpellCard : BattleCard
{
    [SerializeField] protected SpellCard card = new();

    public BattleSpellCard(SpellCard newCard)
    {
        card = newCard;
    }

    protected Vector2 originalPosition = Vector2.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    public override SpellCard GetSpell()
    {
        return card;
    }
    protected virtual void OnMouseDrag()
    {
        if (originalPosition == Vector2.zero)
            originalPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(mousePosition.x, mousePosition.y);
    }

    protected virtual void OnMouseUp()
    {
        // Return the card to its original position
        transform.position = originalPosition;
        originalPosition = Vector2.zero;
        if (card.targetType == TargetType.SingleEnemy || card.targetType == TargetType.SingleAlly)
        {
            Collider2D[] hit = Physics2D.OverlapPointAll( Camera.main.ScreenToWorldPoint(Input.mousePosition),
    LayerMask.GetMask("Battle"));
            if (hit.Length != 0)
            {
                foreach (Collider2D h in hit)
                {
                    BattleCard objectiveCard = h.GetComponent<BattleCard>();
                    if (objectiveCard != null)
                    {
                        if ((card.targetType == TargetType.SingleEnemy &&
                            objectiveCard.gameObject.transform.IsChildOf(card.GetEnemyBattleGroundZone().transform)) ||
                            (card.targetType == TargetType.SingleAlly &&
                            objectiveCard.gameObject.transform.IsChildOf(card.GetPlayerBattleGroundZone().transform)))
                        {
                            if(SceneManager.GetActiveScene().name.Contains("Tutorial"))
                            {
                                TutorialHand.Instance.UseSpellCard(this, this);
                            }
                            else
                            {
                                PlayerHand.GetPlayerHand().UseSpellCard(this, this);
                            }
                            Debug.Log("Target selected: " + objectiveCard.GetCard().cardName);
                            card.SetTarget(objectiveCard);
                            card.ApplyEffectToTarget();
                            return;
                        }
                    }
                }
            }
            Debug.Log("No valid target selected.");
            return;
        }
        PlayerHand.GetPlayerHand().UseSpellCard(this, this);
    }

    public override Card GetCard()
    {
        return (SpellCard) card;
    }

    public override void SetCard(Card card)
    {
        this.card = (SpellCard) card;
    }

    public override void ShowCardDetails()
    {
        //throw new System.NotImplementedException();
    }

    public override void UseCard()
    {
        card.ApplyEffect();

        Destroy(this.gameObject);
    }
    private void OnMouseDown()
    {
        if (card.targetType == TargetType.SingleEnemy ||
            card.targetType == TargetType.SingleAlly)
            return;

        card.OnMouseDown();
    }
}
