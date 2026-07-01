using System.Collections;
using TMPro;
using UnityEngine;

public class GuideText : MonoBehaviour
{
    private TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(ShowAndHide());
    }
    IEnumerator ShowAndHide()
    {
        while (true)
        {
            while (text.alpha > 0)
            {
                text.alpha -= Time.deltaTime;
                yield return null;
            }
            while (text.alpha < 1)
            {
                text.alpha += Time.deltaTime;
                yield return null;
            }
        }
    }
}
