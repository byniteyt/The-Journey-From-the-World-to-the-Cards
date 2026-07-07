using System.Collections;
using TMPro;
using Unity.VisualScripting;
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
    void OnEnable()
    {
        StartCoroutine(ShowAndHide());
    }

    private void OnDisable()
    {
        StopCoroutine(ShowAndHide());
    }

    private void OnDestroy()
    {
        StopCoroutine(ShowAndHide());
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
