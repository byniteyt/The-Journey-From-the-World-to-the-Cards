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
        player.playerSources = PlayerSources.sources;

        Debug.Log("Initializing Player Stats");
        PlayerStats.InitializeStats();
        player.playerStats = PlayerStats.stats;

        Debug.Log("Initializing Player Properties");
        player.playerProperties = PlayerProperties.properties;
        player.playerProperties.Initialize();
        
    }

    public PlayerSources Sources()
    {
        return player.playerSources;
    }

    public PlayerStats Stats()
    {
        return player.playerStats;
    }

    public PlayerProperties Properties()
    {
        return player.playerProperties;
    }
}
