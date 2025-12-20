using System;
using UnityEngine;
[Serializable]
public class PlayerStats
{
    public static PlayerStats stats = new PlayerStats();
    public string playerName = "New Player";
    public int playerLevel = 1;
    public int playerExp = 0;
    public int gamesPlayed;
    public int gamesWon;
    public int gamesLost;
    private int expForNextLevel = 100;


    public static void Load()
    {
        stats.LoadStats();
    }

    public static void Save()
    {
        stats.SaveStats();
    }
    public static PlayerStats GetStats()
    {
        return stats;
    }
    public static void ResetStats()
    {
        stats = new PlayerStats();
    }
    public static void InitializeStats()
    {
        if (!SaveData<PlayerStats>.SaveDataExists("PlayerStats.json"))
        {
            stats.SaveStats();
        }
        else
        {
            stats.LoadStats();
        }
    }
    #region Loader and Saver
    public void LoadStats()
    {
        //SaveData<PlayerStats>.DeserializeJSON(stats, "PlayerStats.json");
        PlayerStats statsToLoad = SaveData<PlayerStats>.DeserializeJSON( "PlayerStats.json");
        stats.playerName = statsToLoad.playerName;
        stats.playerLevel = statsToLoad.playerLevel;
        stats.playerExp = statsToLoad.playerExp;
        stats.gamesPlayed = statsToLoad.gamesPlayed;
        stats.gamesWon = statsToLoad.gamesWon;
        stats.gamesLost = statsToLoad.gamesLost;
    }
    public void SaveStats()
    {
        SaveData<PlayerStats>.SerializeJSON(stats, "PlayerStats.json");
    }
    #endregion
    public void AddExp(int value)
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
