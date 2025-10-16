using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardText : MonoBehaviour
{

    // Basic UI Elements
    protected TextMeshProUGUI nameText;
    [SerializeField] protected TextMeshProUGUI costText;
    [SerializeField] protected TextMeshProUGUI descriptionText;
    [SerializeField] protected Image artworkImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        nameText = GameObject.Find("Name").GetComponent<TextMeshProUGUI>();
        costText = GameObject.Find("Cost").GetComponent<TextMeshProUGUI>();
        descriptionText = GameObject.Find("Description").GetComponent<TextMeshProUGUI>();
        artworkImage = GameObject.Find("Card").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    virtual public void UpdateAsset()
    {
        Card card = GetComponent<Card>();
        if (nameText != null) nameText.text = card.cardName;
        if (costText != null) costText.text = card.cost.ToString();
        if (descriptionText != null) descriptionText.text = card.description;
        if (artworkImage != null) artworkImage.sprite = card.artwork;
    }
}
