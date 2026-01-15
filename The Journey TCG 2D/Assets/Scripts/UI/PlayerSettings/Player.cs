using UnityEngine;

public class Player 
{
    private static Player player;
    PlayerSources playerSources;
    PlayerProperties playerProperties;
    PlayerStats playerStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static Player GetPlayer()
    {
        if (player == null)
        {
            player = new();
            player.InitPlayer();
        }
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
        
        if (player.playerProperties.cardsReceived == null || player.playerProperties.cardsReceived.Count == 0)
        {
            Debug.Log("No tenemos cartas recibidas");
        }
        foreach (var card in player.playerProperties.cardsReceived)
        {
            if (card.card == null)
            {
                Debug.LogWarning("La carta recibida es nula"); continue;
            }
            if (card.card.GetCard()== null)
            {
                Debug.LogWarning("No hay carta"); continue;
            }
            if (card.card.GetCard().cardName == null)
            {
                Debug.LogWarning("No hay nombre de carta"); continue;
            }
            Debug.Log($"Card received: {card.card.GetCard().cardName}, Amount: {card.amount}");
        }
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
