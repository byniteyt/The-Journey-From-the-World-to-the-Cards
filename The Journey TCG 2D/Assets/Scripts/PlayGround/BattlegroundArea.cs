using UnityEngine;

public class BattlegroundArea : MonoBehaviour
{
    public int soldiersAmount;
    [SerializeField] protected float characterSpacing;
    [SerializeField] protected float maxSoldiersPerRow;
    void Start()
    {
        characterSpacing = (characterSpacing == 0.0f )?2.0f:characterSpacing;
        maxSoldiersPerRow = (maxSoldiersPerRow == 0.0f )?2.0f: maxSoldiersPerRow;
    }

    public int GetSoldiersAmount()
    {
        return soldiersAmount;
    }
    public virtual bool GenerateCharacter(BattleCharCard card)
    {
        if (soldiersAmount >= maxSoldiersPerRow)
        {
            Debug.Log("BattleGround is full");
            return false;
        }
        GameObject cardObject = Instantiate(card.gameObject,this.transform);
        soldiersAmount++;
        cardObject.transform.localPosition = new Vector3(0,0,-1);
        ReorderCharacters();
        return true;
    }
    public bool IsRoomCardInArea(BattleCard card)
    {
        Vector2 cardPosition = card.transform.position;
        Vector2 areaPosition = transform.position;
        Vector2 areaSize = GetComponent<Collider2D>().bounds.size;
        return (TurnManager.Instance.IsPlayerTurn&&
                cardPosition.x >= areaPosition.x - areaSize.x / 2 &&
                cardPosition.x <= areaPosition.x + areaSize.x / 2 &&
                cardPosition.y >= areaPosition.y - areaSize.y / 2 &&
                cardPosition.y <= areaPosition.y + areaSize.y / 2);
    }

    public void ReorderCharacters()
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
