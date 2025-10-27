using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileSettings : MonoBehaviour
{
    TextMeshProUGUI playerNameText;
    TextMeshProUGUI playerLevelText;
    Slider nextLevelSlider;
    static int currentExp = 0;
    static int expForNextLevel = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerNameText = GameObject.Find("PlayerName").GetComponent<TextMeshProUGUI>();
        playerNameText.text = (PlayerPrefs.HasKey("PlayerName"))? 
            PlayerPrefs.GetString("PlayerName", "Player"): "New Player";
        playerLevelText = GameObject.Find("Level").GetComponent<TextMeshProUGUI>();
        playerLevelText.text = "Level " + 
            ((PlayerPrefs.HasKey("PlayerLevel")) ? 
            PlayerPrefs.GetInt("PlayerLevel", 1) : 1).ToString();
        nextLevelSlider = GameObject.Find("NextLevelSlider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectCharacter()
    {

    }
    public void SetName(string newName) 
    { 
        PlayerPrefs.SetString("PlayerName", newName);
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
        PlayerPrefs.SetInt("PlayerLevel", actLevel);
    }
    void OnDisable()
    {
        PlayerPrefs.Save();
    }
}
