using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralButtons : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Cerrando juego...");
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
        Instantiate(Resources.Load<GameObject>("Prefabs/UI/Settings/GlobalSettings"), this.transform);
        //SceneManager.LoadScene("Settings");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void LoadScene(SceneAsset scene)
    {
        SceneManager.LoadScene(scene.name);
    }
}
