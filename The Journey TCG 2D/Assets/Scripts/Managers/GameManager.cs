using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static GameState CurrentGameState { get; set; } = GameState.MainMenu;
    private GameObject pauseMenu;
    private Button pauseButton;
    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        pauseButton = pauseMenu.transform.Find("PauseButton").gameObject.GetComponent<Button>();
        pauseButton.onClick.AddListener(() => ChangeGameState(GameState.InGame));
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        EventManager.StartTurn+= (s, e) => StartTurn();
        EventManager.GameOver += EndGame;
    }
    // Update is called once per frame
    void Update()
    {
        pauseMenu.SetActive(CurrentGameState == GameState.Paused);
    }
    void StartTurn()
    {
        ChangeGameState(GameState.InGame);
    }
    public void ChangeGameState(GameState newState)
    {
        CurrentGameState = newState;
        switch (CurrentGameState)
        {
            case GameState.MainMenu:
            case GameState.InGame:
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
            case GameState.GameOver:
                Time.timeScale = 0f;
                break;
        }
    }
    void EndGame(object sender, bool playerWon)
    {
        Time.timeScale = 0f;
        ChangeGameState(GameState.GameOver);
        GameObject result = (!playerWon)? Resources.Load<GameObject>("Prefabs/UI/GameOver"):Resources.Load<GameObject>("Prefabs/UI/YOU WIN");
        Instantiate(result, GameObject.Find("Canvas").transform);
    }
}
