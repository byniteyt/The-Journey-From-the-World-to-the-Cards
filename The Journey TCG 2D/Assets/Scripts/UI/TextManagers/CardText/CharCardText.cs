using TMPro;
using UnityEngine;

public class CharCardText : CardText
{
    // Character specific UI Elements
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI attackText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    override protected void Start()
    {
        SetValues(cardToRead);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    override protected void SetValues(Card carta)
    {
        base.SetValues(carta);
        CharacterCard characterCard = (CharacterCard)carta;
        healthText.text = characterCard.health.ToString();
        attackText.text = characterCard.attack.ToString();
    }

}
