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
        Debug.Log($"Spell played: {spellName}, expected: {dialogueSequence[activeDialogue].cardName}");
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

    public void HideInformation(object sender, string cardName)
    {
        if (dialogueSequence[activeDialogue].task != TutorialTasks.HideInformation)
        {
            Debug.Log($"Task: {TutorialTasks.HideInformation}, expected: {dialogueSequence[activeDialogue].task}");
            return;
        }
        if ((cardName != dialogueSequence[activeDialogue].cardName &&
            dialogueSequence[activeDialogue].cardName!=""))
        {
            Debug.Log($"Information hidden for: {cardName}, expected: {dialogueSequence[activeDialogue].cardName}");
            return;
        }
        StartDialogue(++activeDialogue);
    }

    public void ChangeGamePhase(string phase)
    {
        if(dialogueSequence[activeDialogue].task != TutorialTasks.ChangePhase)
        {
            Debug.Log($"Task: {TutorialTasks.ChangePhase}, expected: {dialogueSequence[activeDialogue].task}");
            return;
        }
        if (phase != TurnManager.CurrentInGamePhase.ToString() && phase != "")
        {
            Debug.Log($"Phase changed to: {phase}, expected: {TurnManager.CurrentInGamePhase}");
            return;
        }
        StartDialogue(++activeDialogue);
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
        HideInformation,
        ChangePhase
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
        CardText.closeInfo += HideInformation;
    }

    private void OnDisable()
    {
        TutorialHand.OnPlayCharacter -= PlayCharacter;
        TutorialHand.OnPlaySpell -= PlaySpell;
        TutorialHand.OnPlayRoom -= PlayRoom;
        Card.OnCardClicked -= ShowInformation;
        CardText.closeInfo -= HideInformation;
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

    #region Dialogue Methods
    // Dialogue controllers
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
    #endregion

    #region Dialogue Events     
    // Events called from the dialogue sequence
    public void EnableCollider(Collider collider)
    {
        collider.enabled = true;
    }

    public void DisableCollider(Collider collider)
    {
        collider.enabled = false;
    }

    public void EnableCard(string card)
    {
        BattleCard searchedCard = TutorialHand.Instance.GetCard(card);
        if (searchedCard == null)
        {
            Debug.LogWarning($"Card {card} not found in TutorialHand.");
            return;
        }
        searchedCard.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void DisableCard(string card)
    {
        BattleCard searchedCard = TutorialHand.Instance.GetCard(card);
        if (searchedCard != null)
        {
            searchedCard.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    #endregion
}