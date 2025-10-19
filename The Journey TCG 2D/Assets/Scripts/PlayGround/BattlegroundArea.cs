using UnityEngine;

public class BattlegroundArea : MonoBehaviour
{
    int soldiersAmount = 0;
    [SerializeField] float characterSpacing = 2.0f;
    [SerializeField] float maxSoldiersPerRow = 6;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool GenerateCharacter(CharacterCard card)
    {
        if (soldiersAmount >= maxSoldiersPerRow)
        {
            Debug.Log("BattleGround is full");
            return false;
        }
        Debug.Log("Character "+card.name+" generated in Battleground Area");
        GameObject cardObject = Instantiate(card.gameObject,this.transform);
        soldiersAmount++;
        if (transform.childCount == 1)
            cardObject.transform.localPosition = new Vector3(0,0,-1);
        else
        {
            cardObject.transform.localPosition = this.transform.GetChild(transform.childCount-1).localPosition 
                + new Vector3(characterSpacing, 0, 0);
        }
        ReorderCharacters();
        return true;
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

    void ReorderCharacters()
    {
        if(soldiersAmount>maxSoldiersPerRow)
        {
            // Implement logic to reorder characters into multiple rows if needed
            Debug.Log("Max soldiers per row reached.");
            return;
        }
        float totalWidth = (soldiersAmount - 1) * characterSpacing;
        float startX = -totalWidth / 2;
        for (int i = 0; i < soldiersAmount; i++)
        {
            Transform child = transform.GetChild(i);
            child.localPosition = new Vector3(startX + i * characterSpacing, 0, child.transform.localPosition.z);
        }
    }
}
