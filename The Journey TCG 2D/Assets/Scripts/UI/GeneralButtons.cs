using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralButtons : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }
    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Settings()
    {
        SceneManager.LoadScene("SettingsMenu");
    }
    public void Credits()
    {
        SceneManager.LoadScene("CreditsMenu");
    }
    public void LoadScene(SceneAsset scene)
    {
        SceneManager.LoadScene(scene.name);
    }
}
