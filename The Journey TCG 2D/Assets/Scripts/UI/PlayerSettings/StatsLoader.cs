using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPrefs.DeleteAll();
        EventManager.AddPlayerExp += (sender, value) =>
        {
            PlayerStats.AddExp(value);
            PlayerStats.SaveStats();
        };
        EventManager.ChangeCoins += (sender, value) =>
        {
            PlayerSources.ChangeCoins(value);
            PlayerSources.SaveSources();
        };
        PlayerStats.LoadStats();
        PlayerSources.LoadSources();
        SceneManager.LoadScene("TitleMenu");
    }
}
