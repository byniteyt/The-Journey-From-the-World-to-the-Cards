using TMPro;
using UnityEngine;

public class CharCardText : CardText
{
    public CharacterCard characterCard;
    // Character specific UI Elements
    private TextMeshProUGUI healthText;
    private TextMeshProUGUI attackText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    override protected void Start()
    {
        base.Start();
        healthText = GameObject.Find("Health").GetComponent<TextMeshProUGUI>();
        attackText = GameObject.Find("Attack").GetComponent<TextMeshProUGUI>();
        SetValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetValues()
    {
        healthText.text = characterCard.health.ToString();
        attackText.text = characterCard.attack.ToString();
        nameText.text = characterCard.cardName;
        costText.text = characterCard.cost.ToString();
        descriptionText.text = characterCard.description;
        artworkImage.sprite = characterCard.artwork;
    }

    override public void UpdateAsset()
    {
        base.UpdateAsset();
        CharacterCard card = GetComponent<CharacterCard>(); ;
        if (healthText != null) healthText.text = card.health.ToString();
        if (attackText != null) attackText.text = card.attack.ToString();
    }
}
