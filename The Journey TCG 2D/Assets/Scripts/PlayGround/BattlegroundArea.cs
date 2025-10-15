using UnityEngine;

public class BattlegroundArea : MonoBehaviour
{
    int soldiersAmount = 0; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GenerateCharacter(CharacterCard card)
    {
        Debug.Log("Character "+card.name+" generated in Battleground Area");
        GameObject cardObject = Instantiate(card.gameObject);
        cardObject.transform.parent = this.transform;
        cardObject.transform.localPosition = new Vector3(-4 + soldiersAmount * 2, 0, 0);
        soldiersAmount++;
    }
    public bool IsRoomCardInArea(Card card)
    {
        if (card != null&& PlayGameManager.CurrentTurnPlayer==TurnPlayer.Player)
        {
            Vector2 cardPosition = card.transform.position;
            Vector2 areaPosition = transform.position;
            Vector2 areaSize = GetComponent<Collider2D>().bounds.size;
            if (cardPosition.x >= areaPosition.x - areaSize.x / 2 &&
                cardPosition.x <= areaPosition.x + areaSize.x / 2 &&
                cardPosition.y >= areaPosition.y - areaSize.y / 2 &&
                cardPosition.y <= areaPosition.y + areaSize.y / 2)
                return true;
        }
        return false;
    }
}
