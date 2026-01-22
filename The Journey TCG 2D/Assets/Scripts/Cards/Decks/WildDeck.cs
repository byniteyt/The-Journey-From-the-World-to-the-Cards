using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class WildDeck : Deck
{
    private CharacterCard commander = null;
    private readonly string[] eliteCards = new string[4];
    private int eliteIndex = 0; // Llevará a cabo la cuenta de cuántos elites llevamos y cual sería el próximo a agregar

    public int EliteIndex
    {
        get { return eliteIndex; }
    }
    public CharacterCard Commander
    {
        get { return commander; }
    }
    public string[] EliteCards
    {
        get { return eliteCards; }
    }
    public WildDeck() : base()
    {
        eliteCards = new string[4];
        deckFormat = DeckFormat.Wild;
        limitCardAmount = 40;
        limitPerCard = 1;
    }
    public WildDeck( Deck deckToClone) : base(deckToClone)
    {
        WildDeck clonedDeck = (WildDeck) deckToClone;
        commander = clonedDeck.commander;
        eliteCards = (string[]) clonedDeck.eliteCards.Clone();
        eliteIndex = clonedDeck.eliteIndex;
    }
    public override void AddCard(BattleCard cardToAdd)
    {
        // Comprobamos que la baraja tenga espacio suficiente
        if (IsFull())
        {
            Debug.Log("Wild Deck is full. Cannot add more cards.");
            return;
        }

        // Comprobamos si la carta es una tropa
        if (cardToAdd.GetCard().GetType() == typeof(CharacterCard))
        {
            Debug.Log("La carta es una tropa");
            // Comprobamos el rango de la tropa
            if (((CharacterCard)cardToAdd.GetCard()).rank == CreatureRank.Boss)
            {
                if (commander != null)
                {
                    Debug.Log("Esta baraja ya tiene a su comandante");
                    return;
                }
                commander = (CharacterCard) cardToAdd.GetCard();
            }

            if (cardToAdd.GetComponent<BattleCharCard>().GetCharacter().rank == CreatureRank.Elite)
            {
                if (eliteCards.Contains(cardToAdd.name))
                {
                    Debug.Log("Este capitan ya fue agregado.");
                    return;
                }
                if (eliteIndex == 4)
                {
                    Debug.Log("No hay hueco para más capitanes.");
                    return;
                }
                eliteCards[eliteIndex] = cardToAdd.name;
                eliteIndex++;
            }
        }
        
        // Revisamos cuántas copias de esa carta hay en la baraja
        if (cardLimits.ContainsKey(cardToAdd.name))
        {
            if (!HasEnoughCards(cardToAdd))
            {
                AddCardToDictionary(cardToAdd);
            }
            else
            {
                Debug.Log("No se puede añadir más copias de " + cardToAdd.name + " a la baraja.");
            }
            return;
        }
        AddCardToDictionary(cardToAdd);
    }
    bool HasEnoughCards(BattleCard cardToAdd)
    {
        return cardLimits[cardToAdd.name] == limitPerCard;
    }

    protected override bool IsFull()
    {
        return deck.Count >= limitCardAmount;
    }

    public override bool IsValidForPlay()
    {
        return deck.Count==limitCardAmount;
    }
}
