using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    TextMeshProUGUI timerText;
    float[] timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = new float[2];
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
                timerText.text = $"Time: {timer[0]:00}:{timer[1]:00}";
            }
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            GameManager.CurrentGameState = GameState.MainMenu;
        }
        /*if (Input.GetKeyUp(KeyCode.P))
        {
            GameManager.CurrentGameState = GameState.InGame;
        }*/
        if (GameManager.CurrentGameState!= GameState.InGame)
        {
            if (GameManager.CurrentGameState == GameState.MainMenu) {
                timer[0] = timer[1] = 0;
                timerText.text = "In Menu"; // Escondemos el temporizador en el menú principal
            }
            return;
        }
        timer[1] += Time.deltaTime;
        if (timer[1] >= 60)
        {
            timer[1] = 0;
            timer[0] ++;
        }
        timerText.text = $"Time: {timer[0]:00}:{timer[1]:00}";
    }
}
