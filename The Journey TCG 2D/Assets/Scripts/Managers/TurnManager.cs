using System;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    public static InGamePhase CurrentInGamePhase { get; set; } = InGamePhase.DrawPhase;

    [SerializeField] bool isPlayerTurn = true;
    public bool IsPlayerTurn => isPlayerTurn;
    private void Start()
    {
        if (Instance == null) Instance = this;
       
        else Destroy(gameObject);

        EventLoader();

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void DrawingPhase(object sender, EventArgs e)
    {
        DeckManager.Instance.DrawCard(1);
        EventManager.FirstMainTurn?.Invoke(this, EventArgs.Empty);
    }
    void FirstMainPhase()
    {
        CurrentInGamePhase = InGamePhase.FirstMainPhase;
    }
    void EndCombat()
    {
        int totalDamage = 0;
        GameObject activeBattleground = (isPlayerTurn) ? GameObject.Find("EnemyLife") : GameObject.Find("PlayerLife");
        foreach (var child in activeBattleground.transform)
        {
            if (child is Transform character)
            {
                CharacterCard characterCard = character.GetComponent<CharacterCard>();
                totalDamage += characterCard.attack;
            }
        }
    }
    void BattlePhase()
    {

    }
    void SecondMainPhase()
    {

    }
    void EndPhase()
    {

    }
    public void FromMainPhaseToEndPhase()
    {
        CurrentInGamePhase = InGamePhase.EndPhase;
    }
    public void FromFirstMainToBattle()
    {
        CurrentInGamePhase = InGamePhase.BattlePhase;
    }
    void EventLoader()
    {
        EventManager.StartTurn += DrawingPhase;
        EventManager.FirstMainTurn += (s, e) => FirstMainPhase();
        EventManager.BattleTurn += (s, e) => BattlePhase();
        EventManager.SecondMainTurn += (s, e) => SecondMainPhase();
        EventManager.EndTurn += (s, e) => EndPhase();
    }
}
