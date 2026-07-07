
using UnityEngine;

public class PlayerHand : Hand
{
    private static PlayerHand Instance;
    
    public static PlayerHand GetPlayerHand()
    {
        if (Instance == null)
        {
            Instance = FindFirstObjectByType<PlayerHand>();
            GameObject bg = GameObject.Find("PlayerBattleGround");
            Instance.battleground = bg.GetComponent<BattlegroundArea>();
        }
        return Instance;
    }
    public override void UseCharacterCard(BattleCharCard card)
    {
        // Check if the card is in hand
        if (!Corrections(card)) return;


        base.UseCharacterCard(card);
        ManaTextManager.Instance.AddMana(-card.GetCharacter().cost);
        Destroy(card.gameObject);
    }

    public override void UseRoomCard(object sender, BattleRoomCard card)
    {
        // Check if the card is in hand
        if (!Corrections(card)) return;
        ManaTextManager.Instance.AddMana(-card.GetRoom().cost);
        base.UseRoomCard(sender, card);
        Destroy(card.gameObject);
    }

    public override void UseSpellCard(object sender, BattleSpellCard card)
    {
        // Check if the card is in hand
        if (!Corrections(card)) return;
        ManaTextManager.Instance.AddMana(-card.GetSpell().cost);
        base.UseSpellCard(sender, card);
        Destroy(card.gameObject);
    }

    protected override void LoadEvents()
    {

        base.LoadEvents();
        EventManager.UseCardFromHand += ReorganizeHand;
    }

    
}
