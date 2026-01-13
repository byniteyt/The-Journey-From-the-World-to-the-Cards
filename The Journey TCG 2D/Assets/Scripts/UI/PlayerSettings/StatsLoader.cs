using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsLoader : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerPrefs.DeleteAll();
        Player.GetPlayer();
        //PlayerStats.InitializeStats();
        //PlayerSources.Initialize();
        //PlayerProperties.InitializeOwnDecks();
        EventManager.AddPlayerExp += (sender, value) =>
        {
            //PlayerStats.AddExp(value);
            PlayerStats.Save();
        };
        EventManager.ChangeCoins += (sender, value) =>
        {
            PlayerSources.ChangeCoins(value);
            PlayerSources.sources.SaveSources();
        };
        //PlayerSources.sources.LoadSources();
        SceneManager.LoadScene("TitleMenu");
    }
}
