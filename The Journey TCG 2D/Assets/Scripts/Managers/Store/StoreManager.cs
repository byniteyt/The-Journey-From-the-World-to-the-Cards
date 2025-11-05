using TMPro;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    
    public void AddCoins(int value)
    {
        value = Mathf.Abs(value);
        EventManager.ChangeCoins?.Invoke(this, value);
    }

    public void RemoveCoins(int value)
    {
        value = Mathf.Abs(value);
        if (PlayerSources.GetCoins() < value)
        {
            Debug.Log("No cuenta con suficiente dinero");
            return;
        }
        EventManager.ChangeCoins?.Invoke(this, -value);
    }


}
