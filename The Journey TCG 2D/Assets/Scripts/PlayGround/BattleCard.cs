using System;
using UnityEngine;

[Serializable]
public class BattleCard : MonoBehaviour
{
    void Start()
    {
        
    }

    protected virtual void Update()
    {
        if (Input.GetMouseButtonDown(1) && !IsShowingDetails())
        {
            if (MouseIsInside())
            {
                GetCard().ShowCardDetails();
            }
        }
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

    protected bool IsShowingDetails()
    {
        return (GameObject.Find("CharacterCardInfo(Clone)") ||
            GameObject.Find("RoomCardInfo(Clone)") ||
            GameObject.Find("SpellCardInfo(Clone)"));
    }

    public virtual Card GetCard() { return null; }
    public virtual SpellCard GetSpell() { return null; }
    public virtual CharacterCard GetCharacter() { return null; }
    public virtual RoomCard GetRoom() { return null; }

    public virtual void SetCard(Card card) { }
    public virtual void ShowCardDetails() { }
    public virtual void UseCard() { }
}
