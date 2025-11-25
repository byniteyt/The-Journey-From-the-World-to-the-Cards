using System;
using UnityEngine;

public class PlayerHand : Hand
{
    private static PlayerHand Instance;
    
    public static PlayerHand GetPlayerHand()
    {
        if (Instance == null)
        {
            Instance = FindFirstObjectByType<PlayerHand>();
        }
        return Instance;
    }

    protected override void LoadEvents()
    {
        base.LoadEvents();
        EventManager.UseCardFromHand += ReorganizeHand;
        EventManager.SetActiveRoom += UseRoomCard;
    }
    
}
