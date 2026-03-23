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

    //public event EventHandler LoadGame;

    public static GameStarter Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        anyInput = new InputAction(type: InputActionType.Button);

        // bindings seguros
        anyInput.AddBinding("<Keyboard>/anyKey"); // Aseguramos que lea todas las teclas del teclado
        anyInput.AddBinding("<Mouse>/leftButton"); // Aseguramos que lea el botón izquierdo del ratón
        anyInput.AddBinding("<Mouse>/rightButton"); // Aseguramos que lea el botón derecho del ratón
        anyInput.AddBinding("<Mouse>/middleButton"); // Aseguramos que lea el botón central del ratón
        anyInput.AddBinding("<Gamepad>/*"); // Aseguramos que lea todos los botones del gamepad
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
        //Debug.Log("Input: " + ctx.control);

        switch (status)
        {
            case 0:

                InitGame();

                break;

            default:
                Debug.Log("Status: " + status + " / " + transform.childCount);
                if (status < transform.childCount)
                {
                    NextLoad();
                }
                else if (status >= transform.childCount)
                {
                    StartCoroutine(StartGame());
                }

                break;
        }
    }

    IEnumerator StartGame()
    {
        anyInput.Disable();

        text.text = "Iniciando el juego...";

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("TitleMenu");
    }

    void InitGame()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        status = 0;
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