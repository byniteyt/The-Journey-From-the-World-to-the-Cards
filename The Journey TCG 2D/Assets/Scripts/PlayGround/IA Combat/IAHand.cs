using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    }
    protected override void LoadEvents()
    {
        EventManager.FirstIAMainTurn += FirstIAMainTurn;
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
        //card.UseCard();
        Debug.Log($"IA jugó {card.GetCard().cardName} por {card.GetCard().cost}." +
            $"\nLe queda {testManaAmount -= card.GetCard().cost} de maná" );
        AdaptToMana(testManaAmount);
    }

    BattleCard SelectPrioritizedCard()
    {
        BattleCard selectedCard = usableCards[0];

        return selectedCard;
    }
    #region First Main Turn
    void FirstIAMainTurn(object sender, System.EventArgs e)
    {
        testManaAmount = 10;
        AdaptToMana(testManaAmount);
        Debug.Log($"----------La IA cuenta con {testManaAmount} de maná-----------");
        while (usableCards.Count > 0 && testManaAmount >= minManaCost)
        {
            BattleCard cardToPlay = SelectRandomCard();
            PlayCard(cardToPlay);
            usableCards.Remove(cardToPlay);
            RemoveCard(cardToPlay);
        }
        string stop = $"La IA no puede jugar más cartas. Tiene {testManaAmount} de maná y ";
        stop += (usableCards.Count> 0)? $"la más barata es de {minManaCost}":"no le quedan más cartas";
        Debug.Log(stop);
        EventManager.IABattleTurn?.Invoke(this, System.EventArgs.Empty);
    }
    #endregion

    #region Combat Turn
    void IABattleTurn(object sender, System.EventArgs e)
    {
        Debug.Log("-----------------IA Combat Turn------------------");
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
