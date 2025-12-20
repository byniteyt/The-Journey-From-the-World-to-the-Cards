using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileSettings : MonoBehaviour
{
    TextMeshProUGUI playerNameText;
    TMP_InputField newPlayerNameText;
    TextMeshProUGUI playerLevelText;
    TextMeshProUGUI playerExpText;
    Slider nextLevelSlider;
    static int currentExp;
    static int expForNextLevel;
    int actLevel;


    void Start()
    {
        newPlayerNameText = GameObject.Find("InputName").GetComponent<TMP_InputField>();
        newPlayerNameText.text = null;
        actLevel = PlayerStats.stats.playerLevel;
        expForNextLevel = 100 + (int) MathF.Log(PlayerStats.stats.playerLevel);
        currentExp = PlayerStats.stats.playerExp;
        playerNameText = GameObject.Find("PlayerName").GetComponent<TextMeshProUGUI>();
        playerNameText.text = PlayerStats.stats.playerName;
        
        playerLevelText = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
        playerLevelText.text = "Level " + PlayerStats.stats.playerLevel;

        playerExpText = GameObject.Find("Exp").GetComponent<TextMeshProUGUI>();
        playerExpText.text = $"{currentExp}/{expForNextLevel}";
        nextLevelSlider = GameObject.Find("NextLevelSlider").GetComponent<Slider>();
        nextLevelSlider.maxValue = expForNextLevel;
        nextLevelSlider.value = currentExp;
    }

    public void SelectCharacter()
    {

    }


    public void SetName(string newName) 
    {
        PlayerStats.stats.playerName = newName;
        playerNameText.text = PlayerStats.stats.playerName;
        newPlayerNameText.text = null;
    }

    void UpdateLevel()
    {
        nextLevelSlider.maxValue = expForNextLevel;
        nextLevelSlider.value = currentExp;

        playerExpText = GameObject.Find("Exp").GetComponent<TextMeshProUGUI>();
        playerExpText.text = $"{currentExp}/{expForNextLevel}";
    }

    public void GetExp(int value)
    {
        AddExp(this, value);
    }
    
    void AddExp(object sender, int value)
    {
        currentExp += value;
        while (currentExp>=expForNextLevel)
        {
            actLevel++;
            currentExp -= expForNextLevel;
        }
        UpdateLevel();
        expForNextLevel = 100 + 5*actLevel;
        playerLevelText.text = "Level " + actLevel;
    }

    public void SaveProfile()
    {
        PlayerStats.stats.playerExp = currentExp;
        PlayerStats.stats.playerLevel = actLevel;

        PlayerStats.Save();
        PlayerStats.Load();
    }

    void OnDisable()
    {
        PlayerPrefs.Save();
    }
    private void OnEnable()
    {
        Start();
    }
}
