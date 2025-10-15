using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterCard", menuName = "Cards/CharacterCard")]
public class CharacterCard : Card
{
    // Character specific attributes
    public int health;
    public int attack;

    // Character specific UI Elements
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI attackText;

    override public void UpdateAsset()
    {
        base.UpdateAsset();
        if (healthText != null) healthText.text = health.ToString();
        if (attackText != null) attackText.text = attack.ToString();
    }

}
