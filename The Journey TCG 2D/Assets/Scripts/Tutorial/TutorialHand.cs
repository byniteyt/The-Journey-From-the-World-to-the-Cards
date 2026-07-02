using UnityEngine;

public class TutorialHand : Hand
{
    public override void UseCharacterCard(BattleCharCard card)
    {
        base.UseCharacterCard(card);
        ManaTextManager.Instance.AddMana(-card.GetCharacter().cost);
        Destroy(card.gameObject);
    }

    public override void UseRoomCard(object sender, BattleRoomCard card)
    {
        ManaTextManager.Instance.AddMana(-card.GetRoom().cost);
        base.UseRoomCard(sender, card);
        Destroy(card.gameObject);
    }

    public override void UseSpellCard(object sender, BattleSpellCard card)
    {
        ManaTextManager.Instance.AddMana(-card.GetSpell().cost);
        base.UseSpellCard(sender, card);
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
        hand[^1].GetComponent<BoxCollider2D>().enabled = false;
    }

    void AddEnabledCard(BattleCard card)
    {
        AddCard(card);
        hand[^1].GetComponent<BoxCollider2D>().enabled = true;
    }
}
