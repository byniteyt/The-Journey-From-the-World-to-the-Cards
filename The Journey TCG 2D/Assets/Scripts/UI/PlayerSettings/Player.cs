using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player;
    PlayerSources playerSources;
    PlayerProperties playerProperties;
    PlayerStats playerStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        if (player == null)
        {
            player = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (player != this)
        {
            Destroy(gameObject);
        }
        this.playerSources = Player.Sources;
        this.playerProperties = Player.Properties;
        this.playerStats = Player.Stats;
    }

    public static PlayerSources Sources
    {
        get
        {
            if (player.playerSources==null)
            {
                Debug.Log("Initializing Player Sources");
                PlayerSources.Initialize();
                player.playerSources = PlayerSources.sources;
            }
            return PlayerSources.sources;
        }
    }

    public static PlayerStats Stats
    {
        get
        {
            if (player.playerStats==null)
            {
                Debug.Log("Initializing Player Stats");
                PlayerStats.InitializeStats();
                player.playerStats = PlayerStats.stats;
            }
            return PlayerStats.stats;
        }
    }

    public static PlayerProperties Properties
    {
        get
        {
            if (player.playerProperties==null)
            {
                Debug.Log("Initializing Player Properties");
                player.playerProperties = PlayerProperties.properties;
                player.playerProperties.Initialize();
            }
            return PlayerProperties.properties;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
