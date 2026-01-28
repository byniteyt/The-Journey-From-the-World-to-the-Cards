using TMPro;
using UnityEngine;

public class LifeTextManager : MonoBehaviour
{
    string player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = (gameObject.name == "PlayerLife") ? "Player\n" : "Enemy\n";
        LifeManager lifeManager = this.GetComponent<LifeManager>();
        GetComponent<TextMeshProUGUI>().text = $"{player}Lives: {lifeManager.GetLives()}";
        EventManager.UpdateLife += ChangeLifeText;
    }

    void ChangeLifeText(object caller, int lives)
    {
        Debug.Log(caller);
        if ((caller as LifeManager).gameObject.name != this.gameObject.name) return;
        GetComponent<TextMeshProUGUI>().text = $"{player}Lives: {lives}";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
