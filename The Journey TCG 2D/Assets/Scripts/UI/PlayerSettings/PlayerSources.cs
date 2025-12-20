using System;
using UnityEngine;
[Serializable]
public class PlayerSources 
{
    public static PlayerSources sources = new PlayerSources();
    public int coins;
    public int gems;
    public int cardFragments;

    #region Global loader and saver
    public static void Initialize()
    {
        if (!SaveData<PlayerSources>.SaveDataExists("PlayerSources.json"))
        {
            sources.coins = 50;
            sources.SaveSources();
        }
        else
        {
            sources.LoadSources();
        }
    }
    #endregion

    public void LoadSources()
    {
        PlayerSources sourcesToLoad = SaveData<PlayerSources>.DeserializeJSON("PlayerSources.json");
        sources.coins = sourcesToLoad.coins;
        sources.gems = sourcesToLoad.gems;
        sources.cardFragments = sourcesToLoad.cardFragments;
        Debug.Log("Sources loaded: " + sources.coins + " coins, " + sources.gems + " gems, " + sources.cardFragments + " card fragments.");
    }

    public void SaveSources()
    {
        SaveData<PlayerSources>.SerializeJSON(sources, "PlayerSources.json");
    }

    #region Coin methods
    public static void ChangeCoins(int value)
    {
        sources.coins += value;
    }

    public static bool HasCoins(int value)
    {
        return (sources.coins >= value);
    }
    #endregion

    #region Gem methods
    public static void ChangeGems(int value)
    {
        sources.gems += value;
    }

    public static bool HasGems(int value)
    {
        return (sources.gems >= value);
    }
    #endregion

    #region Card Fragment methods
    public static void ChangeCardFragments(int value)
    {
        sources.cardFragments += value;
    }

    public static bool HasCardFragments(int value)
    {
        return (sources.cardFragments >= value);
    }
    #endregion
}
