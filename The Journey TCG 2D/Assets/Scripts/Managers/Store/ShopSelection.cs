using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopSelection : MonoBehaviour
{
    GameObject selection;
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).GetComponent<Button>())
            {
                transform.GetChild(i).AddComponent<Button>();
            }
            Button boton = transform.GetChild(i).GetComponent<Button>();
            boton.onClick.AddListener(() => ActiveSelection(i));
        }
        ActiveSelection(0);
    }

    void ActiveSelection(int index)
    {
        if (selection != null) selection.SetActive(false);
        selection = GameObject.Find("ShopSelections").transform.GetChild(index).gameObject;
        selection.SetActive(true);
    }
}

