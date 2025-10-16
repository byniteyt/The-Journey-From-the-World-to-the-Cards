using UnityEngine;

public class PlayGameManager : MonoBehaviour
{
    public static PlayGameManager Instance { get; private set; }
    public static InGamePhase CurrentInGamePhase { get; set; } = InGamePhase.DrawPhase;
    public static TurnPlayer CurrentTurnPlayer { get; set; } = TurnPlayer.Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(CurrentInGamePhase)
        {
            case InGamePhase.DrawPhase:
                DrawingPhase();
                TimeWaiter.WaitFor(0.5f);
                break;
            case InGamePhase.FirstMainPhase:
                FirstMainPhase();
                break;
            case InGamePhase.BattlePhase:
                BattlePhase();
                break;
            case InGamePhase.SecondMainPhase:
                SecondMainPhase();
                break;
            case InGamePhase.EndPhase:
                EndPhase();
                break;
        }
    }
    void DrawingPhase()
    {
        DeckManager.Instance.DrawCard(1);
        CurrentInGamePhase = InGamePhase.FirstMainPhase;
    }
    void FirstMainPhase()
    {

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
}
