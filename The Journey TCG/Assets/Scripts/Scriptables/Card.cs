using TMPro;
using UnityEngine;

//[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]
public class Card : ScriptableObject
{
    // Basic UI Elements
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private SpriteRenderer artworkImage;

    // Basic Info
    public Sprite artwork;
    public string cardName;
    public string description;
    public int cost;

    // Advanced Info
    /*public CardType cardType;
    public Rarity rarity;
    public Element element;*/

    // Advanced UI Elements
    //[SerializeField] private TextMeshProUGUI cardTypeText;

    virtual public void UpdateAsset()
    {
        if (nameText != null) nameText.text = cardName;
        if (costText != null) costText.text = cost.ToString();
        if (descriptionText != null) descriptionText.text = description;
        if (artworkImage != null) artworkImage.sprite = artwork;
    }
}
