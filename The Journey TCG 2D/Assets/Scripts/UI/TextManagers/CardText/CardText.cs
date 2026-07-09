using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardText : MonoBehaviour
{
    public static EventHandler<int> closeInfo;
    public Card cardToRead;
    // Basic UI Elements
    [SerializeField] protected TextMeshProUGUI nameText;
    [SerializeField] protected TextMeshProUGUI costText;
    [SerializeField] protected TextMeshProUGUI descriptionText;
    [SerializeField] protected Image artworkImage;

    virtual protected void SetValues(Card carta)
    {
        nameText.text = carta.cardName;
        costText.text = carta.cost.ToString();
        descriptionText.text = carta.description;
        artworkImage.sprite = carta.artwork;
    }
    public virtual void UpdatePanel(Card card)
    {
        SetValues(card);
    }
    public virtual void ClosePanel(int num)
    {
        closeInfo?.Invoke(this, num);
        gameObject.SetActive(false);
    }
}
