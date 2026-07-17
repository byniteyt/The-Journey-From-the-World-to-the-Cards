using TMPro;
using UnityEngine;

public class CharCardText : CardText
{
    // Character specific UI Elements
    static CharCardText instance;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI attackText;

    private void Awake()
    {
        instance = this;
    }

    override protected void SetValues(Card carta)
    {
        base.SetValues(carta);
        CharacterCard characterCard = (CharacterCard)carta;
        healthText.text = characterCard.health.ToString();
        attackText.text = characterCard.attack.ToString();
    }

    private void OnDestroy()
    {
        instance = null;
    }

}
