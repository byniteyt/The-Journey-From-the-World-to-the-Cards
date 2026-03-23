using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SpellCardText : CardText
{
    [SerializeField] private Image spellTarget;
    [SerializeField] private Image spellEffect;
    [SerializeField] private TextMeshProUGUI spellPower;
    private void Start()
    {
        
    }
    override protected void SetValues(Card carta)
    {
        base.SetValues(carta);
        if(carta.artwork == null)
        {
            Debug.LogWarning("Artwork is missing for card: " + carta.cardName);
        }
        SpellCard spellCard = (SpellCard)carta;
        //spellTarget.sprite = spellCard.targetType;
        //spellEffect.sprite = spellCard.effectSprite;
        spellPower.text = spellCard.effectAmount.ToString();
    }
    public override void UpdatePanel(Card card)
    {
        SetValues(card);
    }
}
