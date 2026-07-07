using System;
using System.Linq;
using UnityEngine;

public class TutorialHand : Hand
{
    #region Events
    public static event Action<string> OnPlayCharacter;
    public static event Action<string> OnPlaySpell;
    public static event Action<string> OnPlayRoom;
    #endregion

    public static TutorialHand Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        battleground = GameObject.Find("PlayerBattleGround").GetComponent<BattlegroundArea>();
    }

    public override void UseCharacterCard(BattleCharCard card)
    {
        if (!hand.Contains(card)) 
        {
            return;
        }
        base.UseCharacterCard(card);
        ManaTextManager.Instance.AddMana(-card.GetCharacter().cost);
        OnPlayCharacter?.Invoke(card.GetCharacter().cardName);
        Destroy(card.gameObject);
    }

    public override void UseRoomCard(object sender, BattleRoomCard card)
    {
        ManaTextManager.Instance.AddMana(-card.GetRoom().cost);
        base.UseRoomCard(sender, card);
        OnPlayRoom?.Invoke(card.GetRoom().cardName);
        Destroy(card.gameObject);
    }

    public override void UseSpellCard(object sender, BattleSpellCard card)
    {
        ManaTextManager.Instance.AddMana(-card.GetSpell().cost);
        base.UseSpellCard(sender, card);
        OnPlaySpell?.Invoke(card.GetSpell().cardName);
        Destroy(card.gameObject);
    }

    public void StartTutorial()
    {
        GameManager.Instance.ChangeGameState(GameState.InGame);
        hand = new BattleCard[7];
        AddDisabledCard(CardDataBase.Instance.GetBattleCard("Seta"));
        AddDisabledCard(CardDataBase.Instance.GetBattleCard("Town Hall"));
        AddEnabledCard(CardDataBase.Instance.GetBattleCard("Sappy"));
        AddDisabledCard(CardDataBase.Instance.GetBattleCard("Swords"));
        AddDisabledCard(CardDataBase.Instance.GetBattleCard("Shield"));
    }

    void AddDisabledCard(BattleCard card)
    {
        AddCard(card);
        hand[actualHandSize-1].GetComponent<BoxCollider2D>().enabled = false;
    }

    void AddEnabledCard(BattleCard card)
    {
        AddCard(card);
        hand[actualHandSize-1].GetComponent<BoxCollider2D>().enabled = true;
    }
}
