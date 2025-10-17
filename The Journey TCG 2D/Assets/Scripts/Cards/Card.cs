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
   
    protected virtual void Update()
    {
        if (Input.GetMouseButtonDown(1)&&!IsShowingDetails())
        {
            if (MouseIsInside())
            {
                ShowCardDetails();
            }
                
        }
    }

    protected void OnMouseOver()
    {

    }
    protected void OnMouseExit()
    {
    }
    protected virtual void OnMouseDown()
    {
        /*if(ManaTextManager.Instance.IsEnoughMana(cost))
        {
            Debug.Log("Card played: " + cardName);
            //Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Not enough mana to play: " + cardName);
        }*/
    }
    protected virtual void UseCard()
    {

    }
    protected virtual void ShowCardDetails()
    {
        
    }
    protected bool IsShowingDetails()
    {
        return (GameObject.Find("CharacterCardInfo(Clone)")|| 
            GameObject.Find("RoomCardInfo(Clone)")|| 
            GameObject.Find("SpellCardInfo(Clone)"));
    }
    protected bool MouseIsInside()
    {
        Vector2 areaPosition = this.transform.position;
        Vector2 areaSize = this.GetComponent<Collider2D>().bounds.size;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return (mousePosition.x >= areaPosition.x - areaSize.x / 2 &&
                mousePosition.x <= areaPosition.x + areaSize.x / 2 &&
                mousePosition.y >= areaPosition.y - areaSize.y / 2 &&
                mousePosition.y <= areaPosition.y + areaSize.y / 2);
    }
}
