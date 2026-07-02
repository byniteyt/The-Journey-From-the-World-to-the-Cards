using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TutorialGuide : MonoBehaviour
{
    [Serializable]
    struct DialogueData
    {
        [TextArea(1, 5)] public string dialogue;
        public RectTransform image;
        public UnityEvent action;
    }
    [Serializable]
    struct DialogueSequence
    {
        public DialogueData[] dialogues;
    }

    private int index;
    [SerializeField, Range(0, 0.4f)] private float delay;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private DialogueData[] dialogues;
    [SerializeField] private List<DialogueSequence> dialogueSequence;
    TextMeshProUGUI text;

    private void Start()
    {
        text = dialoguePanel.GetComponentInChildren<TextMeshProUGUI>();
        StartDialogue(); // Start the dialogue immediately when the scene starts
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (text.text == dialogues[index].dialogue)
            {
                Next();
            }
            else
            {
                StopAllCoroutines();
                text.text = dialogues[index].dialogue;
            }
        }
    }
    private void StartDialogue()
    {
        index = 0;
        StartCoroutine(Show());
        //Time.timeScale = 0f;
    }
    private IEnumerator Show()
    {
        DarkUI.Instance.SetTarget(dialogues[index].image);
        text.text = string.Empty;
        foreach (char ch in dialogues[index].dialogue)
        {
            text.text += ch;
            yield return new WaitForSecondsRealtime(delay);
        }
    }
    private void Next()
    {
        if(dialogues[index].action.GetPersistentEventCount() > 0)
        {
            dialogues[index].action.Invoke();
        }
        index++;
        if (index < dialogues.Length)
        {
            StartCoroutine(Show());
        }
        else
        {
            gameObject.SetActive(false);
            //Time.timeScale = 1f;
        }
    }
}