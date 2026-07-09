using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TutorialGuide : MonoBehaviour
{
    #region Event Methods
    public void PlayCharacter(string characterName)
    {
        if (characterName != dialogueSequence[activeDialogue].cardName
            || dialogueSequence[activeDialogue].task != TutorialTasks.PlayCard)
        {
            Debug.Log($"Character played: {characterName}, expected: {dialogueSequence[activeDialogue].cardName}");
            return;
        }
        Debug.Log($"Character played: {characterName}, expected: {dialogueSequence[activeDialogue].cardName}");
        StartDialogue(++activeDialogue);
    }

    public void PlaySpell(string spellName)
    {
        if (spellName != dialogueSequence[activeDialogue].cardName
            || dialogueSequence[activeDialogue].task != TutorialTasks.PlayCard)
        {
            Debug.Log($"Spell played: {spellName}, expected: {dialogueSequence[activeDialogue].cardName}");
            return;
        }
        StartDialogue(++activeDialogue);
    }

    public void PlayRoom(string roomName)
    {
        if (roomName != dialogueSequence[activeDialogue].cardName
            || dialogueSequence[activeDialogue].task != TutorialTasks.PlayCard)
        {
            Debug.Log($"Room played: {roomName}, expected: {dialogueSequence[activeDialogue].cardName}");
            return;
        }
        StartDialogue(++activeDialogue);
    }

    public void ShowInformation(Card card)
    {
        if (card.cardName != dialogueSequence[activeDialogue].cardName
            || dialogueSequence[activeDialogue].task != TutorialTasks.ShowInformation)
        {
            Debug.Log($"Information shown for: {card.cardName}, expected: {dialogueSequence[activeDialogue].cardName}");
            return;
        }
        StartDialogue(++activeDialogue);
    }

    public void HideInformation(int num)
    {
        switch (num)
        {
            case 0:     // close character info

                break; 
            case 1:     // close spell info

                break;
            case 2:     // close room info

                break;
            default:
                break;
        }
    }
    #endregion

    [Serializable]
    struct DialogueData
    {
        [TextArea(1, 5)] public string dialogue;
        public RectTransform image;
        public UnityEvent action;
    }

    enum TutorialTasks
    {
        PlayCard,
        DestroyCard,
        ShowInformation,
        HideInformation
    }

    [Serializable]
    struct DialogueSequence
    {
        public DialogueData[] dialogues;
        public string cardName;
        public TutorialTasks task;
    }

    public static TutorialGuide Instance;

    private int index;
    private int activeDialogue;
    bool isTexting = false;
    [SerializeField, Range(0, 0.4f)] private float delay;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private List<DialogueSequence> dialogueSequence;
    TextMeshProUGUI text;

    #region Initializers
    private void OnEnable()
    {
        TutorialHand.OnPlayCharacter += PlayCharacter;
        TutorialHand.OnPlaySpell += PlaySpell;
        TutorialHand.OnPlayRoom += PlayRoom;
        Card.OnCardClicked += ShowInformation;
    }

    private void OnDisable()
    {
        TutorialHand.OnPlayCharacter -= PlayCharacter;
        TutorialHand.OnPlaySpell -= PlaySpell;
        TutorialHand.OnPlayRoom -= PlayRoom;
        Card.OnCardClicked -= ShowInformation;
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }

    private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

    private void Start()
        {
            text = dialoguePanel.GetComponentInChildren<TextMeshProUGUI>();
            StartDialogue(); // Start the dialogue immediately when the scene starts
        }
    #endregion

    
    private void Update()
    {
        if (!isTexting) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (text.text == dialogueSequence[activeDialogue].dialogues[index].dialogue)
            {
                Next();
            }
            else
            {
                StopAllCoroutines();
                text.text = dialogueSequence[activeDialogue].dialogues[index].dialogue;
            }
        }
    }
    private void StartDialogue(int indice = 0)
    {
        index = 0;
        activeDialogue = indice;
        isTexting = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(Show());
        //Time.timeScale = 0f;
    }
    private IEnumerator Show()
    {
        DarkUI.Instance.SetTarget(dialogueSequence[activeDialogue].dialogues[index].image);
        text.text = string.Empty;
        foreach (char ch in dialogueSequence[activeDialogue].dialogues[index].dialogue)
        {
            text.text += ch;
            yield return new WaitForSecondsRealtime(delay);
        }
    }
    private void Next()
    {
        if(dialogueSequence[activeDialogue].dialogues[index].action.GetPersistentEventCount() > 0)
        {
            dialogueSequence[activeDialogue].dialogues[index].action.Invoke();
        }
        index++;
        if (index < dialogueSequence[activeDialogue].dialogues.Length)
        {
            StartCoroutine(Show());
        }
        else
        {
            DarkUI.Instance.SetTarget(null);
            isTexting = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            //Time.timeScale = 1f;
        }
    }
}