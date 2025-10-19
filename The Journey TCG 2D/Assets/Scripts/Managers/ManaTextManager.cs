using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaTextManager : MonoBehaviour
{
    public static ManaTextManager Instance { get; private set; }
    public static TextMeshProUGUI manaText;
    public static Slider manaSlider;
    public float velocity = 1f; 
    public int maxMana = 100;
    int actualMana = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        manaText = GameObject.Find("Mana Source").GetComponentInChildren<TextMeshProUGUI>();
        manaSlider = GameObject.Find("Mana Source").GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.CurrentGameState == GameState.MainMenu) ResetMana();
        if (GameManager.CurrentGameState != GameState.InGame) return;
        if (actualMana == maxMana) return; 
        manaSlider.value += velocity * Time.deltaTime;
        if (manaSlider.value>=1)
        {
            actualMana++;
            manaSlider.value = 0;
            UpdateManaText();
        }
    }
    void UpdateManaText()
    {
        if (actualMana == maxMana)
        {
            manaText.text = $"Mana: {actualMana}  MAX";
            return;
        }
        else
        {
            manaText.text = $"Mana: {actualMana}";
        }
    }
    public void ChangeMana(int amount)
    {
        actualMana += amount;
        if (actualMana > maxMana) actualMana = maxMana;
        UpdateManaText();
    }
    public bool IsEnoughMana(int cost)
    {
        Debug.Log($"Checking mana: + {actualMana} / { cost}" );
        return (actualMana >= cost);
    }
    public void ResetMana()
    {
        actualMana = 0;
        manaSlider.value = 0;
        UpdateManaText();
    }
}
