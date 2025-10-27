using UnityEngine;
using UnityEngine.SceneManagement;

public static class InitialScene 
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void OnBeforeSceneLoad()
    {
        if (SceneManager.GetActiveScene().name != "PreloadScene")
        {
            SceneManager.LoadScene("PreloadScene");
        }
    }   
}
