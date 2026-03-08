
using UnityEngine;

public class Player: MonoBehaviour 
{
    private static Player player;
    PlayerSources playerSources;
    PlayerProperties playerProperties;
    PlayerStats playerStats;

    private void Awake()
    {
        if (player == null)
        {
            player = this;
            DontDestroyOnLoad(gameObject);
            player.InitPlayer();
            GameStarter.Instance.NextLoad();
        }
        else if (player != this)
        {
            Destroy(gameObject);
        }
    }
    public static Player GetPlayer()
    {
        return player;
    }

    void InitPlayer()
    {
        Debug.Log("Initializing Player Sources");
        PlayerSources.Initialize();
        playerSources = PlayerSources.sources;

        Debug.Log("Initializing Player Stats");
        PlayerStats.InitializeStats();
        playerStats = PlayerStats.stats;

        Debug.Log("Initializing Player Properties");
        playerProperties = PlayerProperties.properties;
        playerProperties.Initialize();
    }

    public PlayerSources Sources()
    {
        return playerSources;
    }

    public PlayerStats Stats()
    {
        return playerStats;
    }

    public PlayerProperties Properties()
    {
        return playerProperties;
    }
}
