using UnityEngine;
using UnityEngine.UI;

public class MainMenuSelections : MonoBehaviour
{
    Button activeButton;
    ColorBlock lastColor;
    ColorBlock newColor;
    int activeIndex;
    [SerializeField] GameObject menuName;

    private void Start()
    {
        this.ActiveMenu(activeIndex);
    }

    public void ActiveMenu(int index)
    {
        GameObject menus = GameObject.Find(menuName.name);
        for (int i = 0; i < menus.transform.childCount; i++)
        {
            menus.transform.GetChild(i).gameObject.SetActive(i == index);
        }
        this.activeIndex = index;
    }

    public void SelectButton(Button selectedButton)
    {
        if (selectedButton==this.GetComponent<Button>())
        {
            return;
        }
        if (activeButton != null)
        {
            activeButton.colors = lastColor;
        }
        activeButton = selectedButton;
        lastColor = activeButton.colors;
        SetColor();
    }

    void SetColor()
    {
        newColor = activeButton.colors;
        newColor.normalColor = new Color(0, 0.2f, 1, 0.7f);
        newColor.selectedColor = new Color(0, 0.2f, 1, 0.7f);
        activeButton.colors = newColor;
    }
}
