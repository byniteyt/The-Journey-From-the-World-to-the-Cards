using UnityEngine;

public sealed class DeckManager : MonoBehaviour
{
    public static DeckManager Instance { get; private set; }

    public Deck SelectedDeck { get; set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetSelectedDeck(Deck deck)
    {
        SelectedDeck = deck;
    }

    public void Clear()
    {
        SelectedDeck = null;
    }
}
