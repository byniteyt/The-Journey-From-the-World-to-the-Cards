using Unity.VisualScripting;
using UnityEngine;

//[CreateAssetMenu(fileName = "Card", menuName = "Scriptable Objects/Card")]
public class Card : MonoBehaviour
{
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
    protected void OnMouseOver()
    {

    }
    protected void OnMouseExit()
    {
    }
    protected void OnMouseDown()
    {
        if(ManaTextManager.Instance.ChangeMana(-cost))
        {
            Debug.Log("Card played: " + cardName);
            //Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Not enough mana to play: " + cardName);
        }
    }

}
