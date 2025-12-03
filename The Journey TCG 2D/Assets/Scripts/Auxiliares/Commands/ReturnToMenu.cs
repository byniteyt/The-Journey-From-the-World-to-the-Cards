using UnityEngine;
namespace Auxiliares
{
    public class ReturnToMenu : ICommand
    {
        public void Execute()
        {
            Debug.Log("Returning to Main Menu...");
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }
}