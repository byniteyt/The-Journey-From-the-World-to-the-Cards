using UnityEngine;

public class TutorialBG : BattlegroundArea
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override bool GenerateCharacter(BattleCharCard card)
    {
        if (soldiersAmount >= maxSoldiersPerRow)
        {
            Debug.Log("BattleGround is full");
            return false;
        }
        GameObject cardObject = Instantiate(card.gameObject, this.transform);
        soldiersAmount++;
        cardObject.transform.localPosition = new Vector3(0, 0, -1);
        ReorderCharacters();
        return true;
    }
}
