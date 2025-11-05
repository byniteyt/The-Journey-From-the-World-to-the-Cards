using UnityEngine;

public static class PlayerSources
{
    private static int coins;

    public static int GetCoins()
    {
        return coins;
    }

    public static void LoadSources()
    {
        coins = (PlayerPrefs.HasKey("PlayerCoins")) ?
            PlayerPrefs.GetInt("PlayerCoins", 0) : 0;
    }

    public static void SaveSources()
    {
        PlayerPrefs.SetInt("PlayerCoins", coins);
        PlayerPrefs.Save();
    }
    public static void ChangeCoins(int value)
    {
        coins += value;
    }
    public static bool HasCoins(int value)
    {
        return (coins >= value);
    }
}
