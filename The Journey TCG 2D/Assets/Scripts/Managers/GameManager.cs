using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static GameState CurrentGameState { get; set; }
    DeckFormat format;
    private GameObject pauseMenu;
    private Button pauseButton;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        format = (SceneManager.GetActiveScene().name == "StandardMatch") ? DeckFormat.Standard : DeckFormat.Wild;
        if(!SceneManager.GetActiveScene().name.Contains("Tutorial"))
        {
            pauseMenu = GameObject.Find("Canvas").transform.Find("PauseMenu").gameObject;
            pauseButton = GameObject.Find("Start").GetComponent<Button>();
            pauseButton.onClick.AddListener(() => ChangeGameState(GameState.InGame));
        }
        EventManager.StartTurn += (s, e) => StartTurn();
        EventManager.GameOver += EndGame;
    }
    // Update is called once per frame
    void Update()
    {
        if (pauseMenu == null)
        {
            Debug.LogWarning("Pause menu not found in the scene.");
            return;
        }
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

    public void ReanudarGame()
    {
        ChangeGameState(GameState.InGame);
    }

    void EndGame(object sender, bool playerWon)
    {
        ChangeGameState(GameState.GameOver);
        GameObject result = (!playerWon)? Resources.Load<GameObject>("Prefabs/UI/GameOver"):Resources.Load<GameObject>("Prefabs/UI/YOU WIN");
        Instantiate(result, GameObject.Find("Canvas").transform);
    }

    public DeckFormat GetCurrentFormat()
    {
        return format;
    }
}
