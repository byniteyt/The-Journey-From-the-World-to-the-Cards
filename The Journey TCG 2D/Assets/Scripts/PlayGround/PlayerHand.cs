using System;
using UnityEngine;

public class PlayerHand : Hand
{
    public static PlayerHand Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    protected override void LoadEvents()
    {
        base.LoadEvents();
        EventManager.UseCardFromHand += ReorganizeHand;
        EventManager.SetActiveRoom += UseRoomCard;
    }
    
}
