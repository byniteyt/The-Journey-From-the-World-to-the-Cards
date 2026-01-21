public sealed class DeckManager
{
    private static DeckManager _instance;
    public static DeckManager Instance => _instance ??= new DeckManager();

    public Deck SelectedDeck { get; set; }
    public Deck IADeck { get; set; }

    private DeckManager() { }

    public void SetSelectedDeck(Deck deck)
    {
        SelectedDeck = deck;
    }

    public void SetIADeck(Deck deck)
    {
        IADeck = deck;
    }

    public void Clear()
    {
        SelectedDeck = null;
        IADeck = null;
    }
}
