using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    int status;
    [SerializeField] TextMeshProUGUI text;
    int index = 0;
    public event EventHandler LoadGame;
    public static GameStarter Instance;
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (Input.anyKeyDown)
        {
            switch (status)
            {
                case 0:
                    InitGame();
                    LoadGame?.Invoke(this, EventArgs.Empty);
                    break;
                default:
                    Debug.Log("Status: " + status);
                    if (status == transform.childCount)
                    {
                        StartCoroutine(StartGame());
                    }
                    break;
            }
        }
    }
    IEnumerator StartGame()
    {
        // Aquí puedes cargar la escena del juego o realizar cualquier otra acción para iniciar el juego
        Debug.Log("Iniciando el juego...");
        text.text = "Iniciando el juego...";
        yield return new WaitForSeconds(2f); // Espera 2 segundos antes de cargar la escena
        SceneManager.LoadScene("TitleMenu");
    }

    void InitGame()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void NextLoad()
    {
        index++;
        status++;
        if (index == transform.childCount)
        {
            text.text = "Start Game";
            //StartCoroutine(StartGame());
            return;
        }
        transform.GetChild(index).gameObject.SetActive(true);

    }

    public void TextInfo(string text)
    {
        this.text.text = text;
    }
}
