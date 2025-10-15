using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static GameState CurrentGameState { get; set; } = GameState.MainMenu;
    private GameObject pauseMenu;
    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        pauseMenu.SetActive(CurrentGameState == GameState.Paused);
    }
    
}
