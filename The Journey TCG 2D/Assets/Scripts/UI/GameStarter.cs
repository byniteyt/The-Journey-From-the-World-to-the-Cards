using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    int status;
    int index = 0;

    InputAction anyInput;

    [SerializeField] TextMeshProUGUI text;

    public event EventHandler LoadGame;

    public static GameStarter Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        // detectar cualquier botón de cualquier dispositivo
        anyInput = new InputAction(
            type: InputActionType.Button,
            binding: "*/{Press}"
        );

        anyInput.performed += OnAnyInput;
    }

    void OnEnable()
    {
        anyInput.Enable();
    }

    void OnDisable()
    {
        anyInput.Disable();
    }

    void OnAnyInput(InputAction.CallbackContext ctx)
    {
        //Debug.Log("Input detectado: " + ctx.control.device);

        switch (status)
        {
            case 0:

                InitGame();
                LoadGame?.Invoke(this, EventArgs.Empty);

                break;

            default:

                if (status < transform.childCount)
                {
                    NextLoad();
                }
                else if (status == transform.childCount)
                {
                    StartCoroutine(StartGame());
                }

                break;
        }
    }

    IEnumerator StartGame()
    {
        anyInput.Disable(); // bloquear input real

        Debug.Log("Iniciando el juego...");
        text.text = "Iniciando el juego...";

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("TitleMenu");
    }

    void InitGame()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        status = 1;
    }

    public void NextLoad()
    {
        index++;
        status++;

        if (index >= transform.childCount)
        {
            text.text = "Start Game";
            return;
        }

        transform.GetChild(index).gameObject.SetActive(true);
    }

    public void TextInfo(string t)
    {
        text.text = t;
    }
}