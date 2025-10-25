using System;
using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    public static InGamePhase CurrentInGamePhase { get; set; } = InGamePhase.DrawPhase;

    GameObject button;

    [SerializeField] bool isPlayerTurn = true;
    public bool IsPlayerTurn => isPlayerTurn;
    private void Start()
    {
        if (Instance == null) Instance = this;
       
        else Destroy(gameObject);

        EventLoader();

        button = GameObject.Find("PhaseChanger");

    }
    public void StartGame()
    {

        isPlayerTurn = true;
        EventManager.StartTurn?.Invoke(this, EventArgs.Empty);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void DrawingPhase(object sender, EventArgs e)
    {
        BattleDeckManager.Instance.DrawCard(1);
        EventManager.FirstMainTurn?.Invoke(this, EventArgs.Empty);
    }
    void FirstMainPhase()
    {
        button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "To Battle Phase";
        button.SetActive(true);
        CurrentInGamePhase = InGamePhase.FirstMainPhase;
    }
    public void NextPhase()
    {
        switch(CurrentInGamePhase)
        {
            case InGamePhase.FirstMainPhase:
                EventManager.BattleTurn?.Invoke(this, EventArgs.Empty);
                break;
            case InGamePhase.BattlePhase:
                int totalDamage = 0;
                GameObject lifeManager = (isPlayerTurn) ? GameObject.Find("EnemyLife") : GameObject.Find("PlayerLife");
                GameObject activeBattleground = (isPlayerTurn) ? GameObject.Find("PlayerBattleGround") : GameObject.Find("EnemyBattleGround");
                foreach (Transform child in activeBattleground.transform)
                {
                        CharacterCard characterCard = child.GetComponent<CharacterCard>();
                        Debug.Log($"Character {characterCard.cardName} attacks for {characterCard.attack} damage.");
                        totalDamage += characterCard.attack;
                }
                if (totalDamage > 0)
                {
                    EventManager.DealDamage?.Invoke(this, -totalDamage);
                    Debug.Log($"Total damage dealt: {totalDamage}");
                }   
                EventManager.SecondMainTurn?.Invoke(this, EventArgs.Empty);
                break;
            case InGamePhase.SecondMainPhase:
                EventManager.StartTurn?.Invoke(this, EventArgs.Empty);
                break;
            default:
                Debug.Log("Invalid phase transition");
                break;
        }
        
    }
    void BattlePhase()
    {
        button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "To Second Main Phase";
        CurrentInGamePhase = InGamePhase.BattlePhase;
    }
    void SecondMainPhase()
    {
        CurrentInGamePhase = InGamePhase.SecondMainPhase;
        button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "To End Phase";
    }
    void EndPhase()
    {
        isPlayerTurn = !isPlayerTurn;
        button.SetActive(false);
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
