using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class BattleCharCard : BattleCard
{
    [SerializeField] protected CharacterCard card = new();

    public BattleCharCard(CharacterCard newCard)
    {
        card = newCard;
    }
    public override Card GetCard()
    {
        if (card == null)
        {
            Debug.LogWarning("Tropa no tiene carta");
            return null;
        }
        return (CharacterCard)card;
    }
    public override CharacterCard GetCharacter()
    {
        return card;
    }

    protected override void Update() 
    {
        base.Update();
    }

    public override void SetCard(Card card)
    {
        this.card = (CharacterCard) card;
    }

    public override void ShowCardDetails()
    {

    }

    public override void UseCard()
    {
        throw new NotImplementedException();
    }
    private void OnMouseDown()
    {
        if (SceneManager.GetActiveScene().name.Contains("Tutorial"))
        {
            Debug.Log("Tutorial: Usando carta de personaje");
            TutorialHand.Instance.UseCharacterCard(this);
            return;
        }
        PlayerHand.GetPlayerHand().UseCharacterCard(this);
    }

    public void DamageCard(int damage)
    {
        card.health -= damage;
        if (card.health <= 0)
        {
            GameObject ground = gameObject.transform.parent.gameObject;
            gameObject.transform.SetParent(null);
            ground.GetComponent<BattlegroundArea>().soldiersAmount--;
            ground.GetComponent<BattlegroundArea>().ReorderCharacters();
            Destroy(this.gameObject);
        }
    }
}
