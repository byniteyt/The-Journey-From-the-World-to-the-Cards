using UnityEngine;

public class BattleSpellCard : BattleCard
{
    [SerializeField] protected SpellCard card = new SpellCard();

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
            RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, LayerMask.GetMask("Battle"));
            if (hit.Length != 0)
            {
                foreach (RaycastHit2D h in hit)
                {
                    BattleCard objectiveCard = h.collider.GetComponent<BattleCard>();
                    if (objectiveCard != null)
                    {
                        if ((card.targetType == TargetType.SingleEnemy &&
                            objectiveCard.gameObject.transform.parent == card.GetEnemyBattleGroundZone().transform) ||
                            (card.targetType == TargetType.SingleAlly &&
                            objectiveCard.gameObject.transform.parent == card.GetPlayerBattleGroundZone().transform))
                        {
                            PlayerHand.GetPlayerHand().UseSpellCard(this, this);
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
        throw new System.NotImplementedException();
    }

    public override void UseCard()
    {
        card.ApplyEffect();

        Destroy(this.gameObject);
    }
    private void OnMouseDown()
    {
        PlayerHand.GetPlayerHand().UseSpellCard(this, this);
        card.OnMouseDown();
    }
}
