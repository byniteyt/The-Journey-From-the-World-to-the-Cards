using Auxiliares;
using System;
using TMPro;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    //bool matchIsStarted = false;

    public static event Action<InGamePhase> ChangePhase;

    public static TurnManager Instance { get; private set; }

    public static InGamePhase CurrentInGamePhase { get; set; }

    GameObject button;

    [SerializeField] bool isPlayerTurn = true;
    public bool IsPlayerTurn => isPlayerTurn;
    private void Start()
    {
        int index = 0;
        foreach (Deck deck in DeckCollection.SavedDecks())
        {
            index++;
            if (!deck.IsValidForPlay())
            {
                Debug.LogError($"Deck {index} is {deck.GetDeckName()}");
            }
        }
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
    void DrawingPhase()
    {
        EventManager.FirstMainTurn?.Invoke(this, EventArgs.Empty);
    }
    void FirstMainPhase()
    {
        Cursor.lockState = CursorLockMode.None;
        button.SetActive(true);
        CurrentInGamePhase = InGamePhase.FirstMainPhase; 
        button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "To Battle Phase";

        if (isPlayerTurn)
        {
            button.SetActive(true);
        }
        else
        {
            NextPhase();
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
        button.SetActive(false); // Se reactiva al empezar el siguiente turno de combate
        EventManager.StartIATurn?.Invoke(this, EventArgs.Empty);
    }

    public void NextPhase()
    {
        switch (CurrentInGamePhase)
        {
            case InGamePhase.FirstMainPhase:
                EventManager.BattleTurn?.Invoke(this, EventArgs.Empty);
                break;
            case InGamePhase.BattlePhase:
                int totalDamage = 0;
                GameObject activeBattleground = GameObject.Find("PlayerBattleGround");
                foreach (Transform child in activeBattleground.transform)
                {
                    CharacterCard characterCard = child.gameObject.GetComponent<BattleCharCard>().GetCharacter();
                    Debug.Log($"Character {characterCard.cardName} attacks for {characterCard.attack} damage.");
                    totalDamage += characterCard.attack;
                }
                if (totalDamage > 0)
                {
                    //EventManager.DealDamage?.Invoke(this, -totalDamage);
                    new DamagePlayerCommand(LifeManager.EnemyHealth, totalDamage).Execute();
                }
                EventManager.SecondMainTurn?.Invoke(this, EventArgs.Empty);
                break;
            case InGamePhase.SecondMainPhase:
                EventManager.EndTurn?.Invoke(this, EventArgs.Empty);
                break;
                case InGamePhase.EndPhase:
                    EventManager.EndTurn?.Invoke(this, EventArgs.Empty);
                break;
            default:
                Debug.Log("Invalid phase transition");
                break;
        }
        ChangePhase?.Invoke(CurrentInGamePhase);

    }

    public void FromSecondMainToEndPhase()
    {
        CurrentInGamePhase = InGamePhase.EndPhase;
    }
    public void FromFirstMainToBattle()
    {
        CurrentInGamePhase = InGamePhase.BattlePhase;
    }
    
    void EventLoader()
    {
        EventManager.StartTurn += (s, e) => DrawingPhase();
        EventManager.FirstMainTurn += (s, e) => FirstMainPhase();
        EventManager.BattleTurn += (s, e) => BattlePhase();
        EventManager.SecondMainTurn += (s, e) => SecondMainPhase();
        EventManager.EndTurn += (s, e) => EndPhase();
    }
}
