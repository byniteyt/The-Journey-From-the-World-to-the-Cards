using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CardText : MonoBehaviour
{

    // Basic UI Elements
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private SpriteRenderer artworkImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

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
