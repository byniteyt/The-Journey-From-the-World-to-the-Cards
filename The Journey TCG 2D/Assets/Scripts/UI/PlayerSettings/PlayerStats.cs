using Unity.VisualScripting;
using UnityEngine;
public static class PlayerStats
{
    public static string playerName = "New Player";
    public static int playerLevel = 1;
    public static int playerExp = 0;
    private static int expForNextLevel = 100;
    #region Loader and Saver
    public static void LoadStats()
    {
        playerName = (PlayerPrefs.HasKey("PlayerName")) ?
            PlayerPrefs.GetString("PlayerName", "Player") : "New Player";
        playerLevel = (PlayerPrefs.HasKey("PlayerLevel")) ?
            PlayerPrefs.GetInt("PlayerLevel", 1) : 1;
        playerExp = (PlayerPrefs.HasKey("PlayerExp")) ?
            PlayerPrefs.GetInt("PlayerExp", 0) : 0;
    }
    public static void SaveStats()
    {
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.SetInt("PlayerLevel", playerLevel);
        PlayerPrefs.SetInt("PlayerExp", playerExp);
        PlayerPrefs.Save();
    }
    #endregion
    public static void AddExp(int value)
    {
        playerExp += value;
        while (playerExp >= expForNextLevel)
        {
            playerLevel++;
            playerExp -= expForNextLevel;
            expForNextLevel = Mathf.RoundToInt(expForNextLevel * 1.1f);
        }
    }

    
}
