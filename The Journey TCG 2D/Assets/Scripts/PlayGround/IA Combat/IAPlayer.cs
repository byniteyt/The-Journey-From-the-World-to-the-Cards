using System;
using UnityEngine;

public class IAPlayer : MonoBehaviour
{
    static IADeck deck;
    static IAHand hand;
    
    public static IADeck GetDeck()
    {
        if (deck == null)
        {
            deck = FindFirstObjectByType<IADeck>();
        }
        return deck;
    }
    public static IAHand GetHand()
    {
        if (hand == null)
        {
            hand = FindFirstObjectByType<IAHand>();
        }
        return hand;
    }
    private void Start()
    {
        EventManager.StartIATurn+=StartIATurn;
    }

    private void StartIATurn(object sender, EventArgs e)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Debug.Log("----------IA Turn Started----------------");
        EventManager.FirstIAMainTurn?.Invoke(this, EventArgs.Empty);
    }

}
