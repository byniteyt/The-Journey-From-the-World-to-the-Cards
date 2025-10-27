using TMPro;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public void AddCoins(int value)
    {
        value = Mathf.Abs(value);
        EventManager.ChangeCoins?.Invoke(this, value);
        GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>().text = 
            PlayerSources.GetCoins().ToString();
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
        GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>().text =
            PlayerSources.GetCoins().ToString();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
