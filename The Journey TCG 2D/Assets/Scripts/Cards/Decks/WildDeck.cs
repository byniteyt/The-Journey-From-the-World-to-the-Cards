using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WildDeck : Deck
{
    private CharacterCard commander;
    private string[] eliteCards;
    private int eliteIndex; // Llevará a cabo la cuenta de cuántos elites llevamos y cual sería el próximo a agregar

    protected override void Awake()
    {
        base.Awake();
        eliteCards = new string[4];
        deckFormat = DeckFormat.Wild;
        limitCardAmount = 40;
    }

    public override void AddCard(Card cardToAdd)
    {
        // Comprobamos que la baraja tenga espacio suficiente
        if (IsFull())
        {
            Debug.Log("Wild Deck is full. Cannot add more cards.");
            return;
        }

        // Comprobamos si la carta es una tropa
        if (cardToAdd.GetComponent<CharacterCard>())
        {
            Debug.Log("La carta es una tropa");
            // Comprobamos el rango de la tropa
            if (cardToAdd.GetComponent<CharacterCard>().rank == CreatureRank.Boss)
            {
                if (commander != null)
                {
                    Debug.Log("Esta abraja ya tiene a su comandante");
                    return;
                }
                commander = (CharacterCard) cardToAdd;
            }

            if (cardToAdd.GetComponent<CharacterCard>().rank == CreatureRank.Elite)
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
                deck.Add(cardToAdd);
                cardLimits[cardToAdd.name]++;
            }
            else
            {
                Debug.Log("No se puede añadir más copias de " + cardToAdd.name + " a la baraja.");
            }
            return;
        }
        deck.Add(cardToAdd);
        cardLimits.Add(cardToAdd.name, 1);
    }
    bool HasEnoughCards(Card cardToAdd)
    {/*
        int count = 0;
        foreach (var card in deck)
        {
            if (card.name == cardToAdd.name)
            {
                count++;
            }
        }
        */
        return cardLimits[cardToAdd.name] == 10;
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
