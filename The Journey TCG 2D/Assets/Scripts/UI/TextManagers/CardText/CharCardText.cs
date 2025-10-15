using TMPro;
using UnityEngine;

public class CharCardText : CardText
{
    // Character specific UI Elements
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI attackText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void UpdateAsset()
    {
        base.UpdateAsset();
        CharacterCard card = GetComponent<CharacterCard>(); ;
        if (healthText != null) healthText.text = card.health.ToString();
        if (attackText != null) attackText.text = card.attack.ToString();
    }
}
