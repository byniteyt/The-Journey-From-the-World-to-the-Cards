using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IAHand : Hand
{
    [SerializeField] int testManaAmount = 10;
    List<Card> usableCards;
    int availableCardCount = 0;
    int minManaCost = 100;
    private static IAHand Instance;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hand = new Card[handLimit];
        LoadEvents();
        currentCardCount = 0;
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
        usableCards = hand.ToList();
        for (int i = 0; i < currentCardCount-1; i++)
        {
            Card card = usableCards[i];
            for (int j = i+1; j < currentCardCount; j++)
            {
                if ((card.cost > hand[j].cost)==toExpensive)
                {
                    (card, usableCards[j]) = (usableCards[j], card);
                }
            }
        }
        minManaCost = usableCards[0].cost;
        availableCardCount = currentCardCount;
    }

    public override bool AddCard(Card cardToAdd)
    {
        hand[currentCardCount] = cardToAdd;
        currentCardCount++;
        StartOrdenatedHand(true);
        return true;
    }

    void AdaptToMana(int amount)
    {
        if (usableCards == null || usableCards.Count == 0) return;
        for (int i = usableCards.Count-1; i >= 0; i--)
        {
            if (usableCards[i].cost <= amount)
            {
                availableCardCount = i + 1;
                break;
                //usableCards.RemoveAt(i);
            }
        }
        if (usableCards.Count > 0)
        {
            minManaCost = usableCards[0].cost;
        }
        else
        {
            minManaCost = 100;
            //EventManager.EndIATurn?.Invoke(this, System.EventArgs.Empty);
        }
    }

    Card SelectRandomCard()
    {
        int randomIndex = Random.Range(0, usableCards.Count);
        return usableCards[randomIndex];
    }

    void PlayCard(Card card)
    {
        //card.UseCard();
        Debug.Log($"IA jugó {card.cardName} por {card.cost}.\nLe queda {testManaAmount -= card.cost} de maná" );
        //testManaAmount -= card.cost;
        AdaptToMana(testManaAmount);
    }

    Card SelectPrioritizedCard()
    {
        Card selectedCard = usableCards[0];

        return selectedCard;
    }
    #region First Main Turn
    void FirstIAMainTurn(object sender, System.EventArgs e)
    {
        //testManaAmount = IABattlefield.GetIABattlefield().GetCurrentMana();
        //AdaptToMana(testManaAmount);
        Debug.Log($"----------La IA cuenta con {testManaAmount} de maná-----------");
        while (usableCards.Count > 0 && testManaAmount >= minManaCost)
        {
            Card cardToPlay = SelectRandomCard();
            PlayCard(cardToPlay);
        }
        string stop = $"La IA no puede jugar más cartas. Tiene {testManaAmount} de maná y ";
        stop += (availableCardCount>0)? $"la más barata es de {minManaCost}":"no le quedan más cartas";
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
