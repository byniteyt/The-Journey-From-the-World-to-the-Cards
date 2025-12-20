using TMPro;
using UnityEngine;

public class CoinTextManager : MonoBehaviour
{
    TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        EventManager.ChangeCoins += ChangeCash;
        text.text = "Coins: " + PlayerSources.sources.coins;
    }

    void ChangeCash(object sender, int value)
    {
        text.text = "Coins: "+PlayerSources.sources.coins;
    }
}
