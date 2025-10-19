using System.Collections;
using UnityEngine;

public class SpellCard : Card
{
    [SerializeField] protected int effectAmount = 15;
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
    void Start()
    {

    }

    override protected void OnMouseDown()
    {
        base.OnMouseDown();
        if (this.targetType != TargetType.AllAllies &&
            this.targetType != TargetType.AllEnemies &&
            this.targetType != TargetType.All)
            return;
         UseCard();
    }

    override protected void UseCard()
    {
        Debug.Log("Spell card played: " + cardName);
        // Implement spell effect here
        ApplyEffect();
        Destroy(this.gameObject);
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
            RaycastHit2D[] hit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, LayerMask.GetMask("Battle"));
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
                            Debug.Log("Target selected: " + card.cardName);
                            targetCard = card;
                            ApplyEffect();
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
}
