using System.Collections;
using UnityEngine;

public class SpellCard : Card
{
    [SerializeField] protected int effectAmount = 15;
    [SerializeField] protected TargetType targetType;
    protected Card targetCard;
    protected SpellEffectType effect;

    protected Vector2 originalPosition;
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
        originalPosition = transform.position;
        //enemyHand;
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }
    override protected void OnMouseDown()
    {
        base.OnMouseDown();
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
        transform.localPosition = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            transform.localPosition.z
        );
    }
    protected virtual void OnMouseUp()
    {
        transform.position = originalPosition;
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
