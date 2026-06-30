using UnityEngine;

public class TutorialHand : PlayerHand
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTutorial()
    {
        GameManager.Instance.ChangeGameState(GameState.InGame);
        hand = new BattleCard[7];
        AddDisabledCard(CardDataBase.Instance.GetBattleCard("Seta"));
        AddDisabledCard(CardDataBase.Instance.GetBattleCard("Town Hall"));
        AddDisabledCard(CardDataBase.Instance.GetBattleCard("Sappy"));
        AddDisabledCard(CardDataBase.Instance.GetBattleCard("Swords"));
        AddDisabledCard(CardDataBase.Instance.GetBattleCard("Shield"));
    }
    void AddDisabledCard(BattleCard card)
    {
        AddCard(card);
        hand[GetHandAmount() - 1].GetComponent<BoxCollider2D>().enabled = false;
    }
}
