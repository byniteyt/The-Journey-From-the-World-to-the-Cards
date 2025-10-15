using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    TextMeshProUGUI timerText;
    float timer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyUp(KeyCode.Escape) )
        {
            if (GameManager.CurrentGameState == GameState.InGame)
            {
                GameManager.CurrentGameState = GameState.Paused;
                timerText.text = "Paused";
            }
            else if (GameManager.CurrentGameState == GameState.Paused)
            {
                GameManager.CurrentGameState = GameState.InGame;
                timerText.text = "Time: " + (timer / 60).ToString("00") + " : " + (timer % 60).ToString("00");
            }
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            GameManager.CurrentGameState = GameState.MainMenu;
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            GameManager.CurrentGameState = GameState.InGame;
        }
        if (GameManager.CurrentGameState!= GameState.InGame)
        {
            if (GameManager.CurrentGameState == GameState.MainMenu) { 
                timer = 0f;
                timerText.text = "In Menu"; // Escondemos el temporizador en el menú principal
            }
            return;
        }
        timer += Time.deltaTime;
        timerText.text = "Time: " + (timer/60).ToString("00") + " : " + (timer%60).ToString("00");
    }
}
