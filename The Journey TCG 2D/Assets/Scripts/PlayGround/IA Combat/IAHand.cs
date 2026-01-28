using Auxiliares;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class IAHand : Hand
{
    [SerializeField] int testManaAmount = 10;
    List<BattleCard> usableCards;
    int availableCardCount;
    int minManaCost = 100;
    private static IAHand Instance;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        availableCardCount = handLimit;
        hand = new BattleCard[handLimit];
        LoadEvents();
        actualHandSize = 0;
        StartOrdenatedHand(true);
        battleground = GameObject.Find("EnemyBattleGround").GetComponent<BattlegroundArea>();
    }
    protected override void LoadEvents()
    {
        EventManager.FirstIAMainTurn += (s, e) =>
        {
            StartCoroutine(FirstIAMainTurnCoroutine());
        };

        EventManager.IABattleTurn+= IABattleTurn;
        EventManager.EndIATurn+= EndIATurn;
        /*
        EventManager.SecondIAMainTurn;
        EventManager.IACombatTurn;
         */
    }
    #region Start Turn
    #endregion

    public static IAHand GetIAHand()
    {
        if (Instance == null)
        {
            Instance = FindFirstObjectByType<IAHand>();
        }
        return Instance;
    }

    void StartOrdenatedHand(bool toExpensive)
    {
        usableCards = hand
            .Where(c => c != null)
            .OrderBy(c => c.GetCard().cost * (toExpensive ? 1 : -1))
            .ToList();

        if (usableCards.Count == 0)
        {
            Debug.LogWarning("La IA no tiene cartas en la mano.");
            minManaCost = 100;
            availableCardCount = 0;
            return;
        }

        minManaCost = usableCards[0].GetCard().cost;
        availableCardCount = usableCards.Count;
    }

    public override bool AddCard(BattleCard cardToAdd)
    {
        hand[actualHandSize] = cardToAdd;
        actualHandSize++;
        StartOrdenatedHand(true);
        return true;
    }

    void AdaptToMana(int amount)
    {
        if (usableCards == null || usableCards.Count == 0) return;
        for (int i = usableCards.Count-1; i >= 0; i--)
        {
            if (usableCards[i].GetCard().cost <= amount)
            {
                availableCardCount = i + 1;
                break;
            }
            usableCards.RemoveAt(i);
        }
        if (usableCards.Count > 0)
        {
            minManaCost = usableCards[0].GetCard().cost;
        }
        else
            minManaCost = 100;
            //EventManager.EndIATurn?.Invoke(this, System.EventArgs.Empty);
    }

    BattleCard SelectRandomCard()
    {
        int randomIndex = Random.Range(0, usableCards.Count);
        return usableCards[randomIndex];
    }

    void PlayCard(BattleCard card)
    {
        switch (card.GetType().ToString())
        {
            case "BattleRoomCard":
                UseRoomCard(this,(BattleRoomCard) card);
                break;
            case string s when s.Contains("Spell"):
                UseSpellCard(this,(BattleSpellCard) card);
                SpellCard spell = ((BattleSpellCard)card).GetSpell();
                spell.SetTarget(TargetToSpell(spell));
                spell.ApplyEffectToTarget();
                break;
            case "BattleCharCard":
                UseCharacterCard((BattleCharCard) card);
                break;
            default:
                return;
        }
        Debug.Log($"IA jugó {card.GetCard().cardName} por {card.GetCard().cost}." +
            $"\nLe queda {testManaAmount -= card.GetCard().cost} de maná");
        AdaptToMana(testManaAmount);
    }



    BattleCard SelectPrioritizedCard()
    {
        BattleCard selectedCard = usableCards[0];

        return selectedCard;
    }
    #region First Main Turn
    IEnumerator FirstIAMainTurnCoroutine()
    {
        testManaAmount = 10;
        AdaptToMana(testManaAmount);

        while (usableCards.Count > 0 && testManaAmount >= minManaCost)
        {
            BattleCard cardToPlay = SelectRandomCard();
            if(cardToPlay.GetType().ToString().Contains("Spell"))
            {
                SpellCard spell = ((BattleSpellCard)cardToPlay).GetSpell();
                if (spell.targetType == TargetType.SingleAlly ||
                    spell.targetType == TargetType.SingleEnemy||
                    spell.targetType == TargetType.RandomAlly||
                    spell.targetType == TargetType.RandomEnemy)
                {
                    BattleCard target = TargetToSpell(spell);
                    if (target == null)
                    {
                        usableCards.Remove(cardToPlay);
                        continue;
                    }
                }
            }
            PlayCard(cardToPlay);

            usableCards.Remove(cardToPlay);
            RemoveCard(cardToPlay);

            yield return new WaitForSeconds(1f);
        }

        EventManager.IABattleTurn?.Invoke(this, EventArgs.Empty);
    }
    BattleCard TargetToSpell(SpellCard spell)
    {
        BattleCard targetCard = null;
        switch (spell.targetType)
        {
            case TargetType.SingleAlly:
            case TargetType.RandomAlly:
                targetCard = battleground.transform.GetChild(Random.Range
                    (0, battleground.transform.childCount)).GetComponent<BattleCard>();
                break;
            case TargetType.SingleEnemy:
            case TargetType.RandomEnemy:
                BattlegroundArea playerBattleground = GameObject.Find("PlayerBattleGround")
                    .GetComponent<BattlegroundArea>();
                targetCard = playerBattleground.transform.GetChild(Random.Range
                    (0, playerBattleground.transform.childCount)).GetComponent<BattleCard>();
                break;
            default:
                break;
        }
        return targetCard;
    }
    #endregion

    #region Combat Turn
    void IABattleTurn(object sender, System.EventArgs e)
    {
        Debug.Log("-----------------IA Combat Turn------------------");

        int totalDamage = 0;
        GameObject activeBattleground =  GameObject.Find("EnemyBattleGround");
        foreach (Transform child in activeBattleground.transform)
        {
            CharacterCard characterCard = child.gameObject.GetComponent<BattleCharCard>().GetCharacter();
            Debug.Log($"Character {characterCard.cardName} attacks for {characterCard.attack} damage.");
            totalDamage += characterCard.attack;
        }
        if (totalDamage > 0)
        {
            new DamagePlayerCommand(LifeManager.PlayerHealth, totalDamage).Execute();
        }
        EventManager.EndIATurn?.Invoke(this, System.EventArgs.Empty);
    }
    #endregion

    #region End Turn
    void EndIATurn(object sender, System.EventArgs e)
    {
        Debug.Log("-----------------IA Turn Ended-------------------");
        EventManager.StartTurn?.Invoke(this, System.EventArgs.Empty);
    }
    #endregion
}
