using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsSetings : MonoBehaviour
{
    int index = 0;
    [SerializeField] private CreditsMovement credits;
    TextMeshProUGUI text;
    AudioSource audioScene;
    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = "x1";
        audioScene = Camera.main.GetComponent<AudioSource>();
    }
    public void Next()
    {
        index++;
        if (index > 2)
        {
            index = 0;
        }
        switch (index)
        {
            case 0:
                text.text = "x1";
                credits?.MultiplySpeed(1);
                audioScene.pitch = 1;
                break;
            case 1:
                text.text = "x2";
                credits?.MultiplySpeed(2);
                audioScene.pitch = 2f;
                break;
            case 2:
                text.text = "x3";
                credits?.MultiplySpeed(3);
                audioScene.pitch = 3f;
                break;
            default:
                break;
        }
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("TitleMenu");
    }
}
