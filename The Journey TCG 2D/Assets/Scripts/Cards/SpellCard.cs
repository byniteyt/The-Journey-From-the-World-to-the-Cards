using System.Collections;
using UnityEngine;

public class SpellCard : Card
{
    [SerializeField] protected int effectAmount;
    [SerializeField] protected TargetType targetType;
    protected Card targetCard;
    protected SpellEffectType effect;

    protected Vector2 originalPosition = Vector2.zero;
    // Regiones del campo de batalla
    protected GameObject playerBattleGround;
    protected GameObject enemyBattleGround;
    protected GameObject playerDeck;
    protected GameObject enemyDeck;
    protected GameObject playerHand;
    protected GameObject enemyHand;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    override protected void OnMouseDown()
    {
        if (this.targetType == TargetType.SingleAlly || this.targetType == TargetType.SingleEnemy)
            return;
         UseCard();
    }

    override protected void UseCard()
    {
        Debug.Log("Spell card played: " + cardName);
        Hand.Instance.UseSpellCard(this,this);
        // Implement spell effect here
        ApplyEffect();
    }
    override protected void ShowCardDetails()
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject cardDetailPanel = Instantiate(Resources.Load<GameObject>("Prefabs/UI/Interfaces/SpellCardInfo"), canvas.transform);
        cardDetailPanel.transform.SetAsLastSibling(); // Ensure the panel is on top
        cardDetailPanel.transform.localPosition = Vector3.zero; // Center the panel
        SpellCardText info = cardDetailPanel.GetComponent<SpellCardText>();
        info.cardToRead = this;
    }
     protected virtual void ApplyEffect()
    {
        
    }
     protected virtual void ApplyEffectToTarget()
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
        if (targetType == TargetType.SingleEnemy || targetType == TargetType.SingleAlly)
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, LayerMask.GetMask("Battle"));
            if (hit.Length != 0)
            {
                foreach (RaycastHit2D h in hit)
                {
                    Card card = h.collider.GetComponent<Card>();
                    if (card != null)
                    {
                        if ((targetType == TargetType.SingleEnemy &&
                            card.gameObject.transform.parent == enemyBattleGround.transform)|| 
                            (targetType == TargetType.SingleAlly &&
                            card.gameObject.transform.parent == playerBattleGround.transform))
                        {
                            Hand.Instance.UseSpellCard(this,this);
                            Debug.Log("Target selected: " + card.cardName);
                            targetCard = card;
                            ApplyEffectToTarget();
                            return;
                        }
                    }
                }
            }
            Debug.Log("No valid target selected.");
            return;
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
