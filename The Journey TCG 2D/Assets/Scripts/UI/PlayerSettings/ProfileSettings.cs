using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileSettings : MonoBehaviour
{
    TextMeshProUGUI playerNameText;
    TextMeshProUGUI playerLevelText;
    TextMeshProUGUI playerExpText;
    Slider nextLevelSlider;
    static int currentExp;
    static int expForNextLevel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        expForNextLevel = 100 + (int) MathF.Exp(PlayerPrefs.GetInt("PlayerLevel", 1));

        playerNameText = GameObject.Find("PlayerName").GetComponent<TextMeshProUGUI>();
        playerNameText.text = (PlayerPrefs.HasKey("PlayerName"))? 
            PlayerPrefs.GetString("PlayerName", "Player"): "New Player";
        
        playerLevelText = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
        playerLevelText.text = "Level " + 
            (PlayerPrefs.HasKey("PlayerLevel") ? 
            PlayerPrefs.GetInt("PlayerLevel", 1) : 1).ToString();

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
        PlayerPrefs.SetString("PlayerName", newName);
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
        int actLevel = (PlayerPrefs.HasKey("PlayerLevel")) ? 
            PlayerPrefs.GetInt("PlayerLevel", 1) : 1;
        currentExp += value;
        while (currentExp>=expForNextLevel)
        {
            actLevel++;
            currentExp -= expForNextLevel;
        }
        UpdateLevel();
        PlayerPrefs.SetInt("PlayerLevel", actLevel);
        expForNextLevel = 100 + (int)MathF.Log(PlayerPrefs.GetInt("PlayerLevel", 1)-1);
        playerLevelText.text = "Level " + PlayerPrefs.GetInt("PlayerLevel", 1);
    }

    void OnDisable()
    {
        PlayerPrefs.Save();
    }
}
