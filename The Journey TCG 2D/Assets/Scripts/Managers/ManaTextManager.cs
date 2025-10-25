using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaTextManager : MonoBehaviour
{
    public static ManaTextManager Instance { get; private set; }
    public static TextMeshProUGUI manaText;
    public float velocity = 1f; 
    public int maxMana = 100;
    int actualMana = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        manaText = GameObject.Find("Mana Source").GetComponentInChildren<TextMeshProUGUI>();
        ResetMana();
        EventManager.StartTurn += ManaTurn;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.CurrentGameState == GameState.MainMenu) ResetMana();
        if (GameManager.CurrentGameState != GameState.InGame) return;
        if (actualMana == maxMana) return; 
    }
    void UpdateManaText()
    {
        manaText.text = (actualMana == maxMana)? $"Mana: {actualMana}  MAX" : $"Mana: {actualMana}";
        
    }
    public void AddMana(int amount)
    {
        actualMana = Mathf.Min( actualMana + amount, maxMana);
        UpdateManaText();
    }

    public void ManaTurn(object sender, EventArgs e)
    {
        actualMana = Mathf.Min(actualMana + 4, maxMana);
        UpdateManaText();
    }

    public bool IsEnoughMana(int cost)
    {
        Debug.Log($"Checking mana: {actualMana} / {cost}");
        return (actualMana >= cost);
    }
    public void ResetMana()
    {
        actualMana = maxMana;
        UpdateManaText();
    }
}
