using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StandardDeck", menuName = "Prefabs/StandardDeck")]
public class PrefabStandDeck : ScriptableObject
{
    public StandardDeck standardDeck;
    int index = 0;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (standardDeck == null)
        {
            Debug.LogWarning("El deck estándar es nulo.");
            return;
        }

        if (standardDeck.deck == null)
            standardDeck.deck = new List<BattleCard>();

        int limit = standardDeck.GetMaxLimit();

        if (standardDeck.deck.Count > limit)
        {
            Debug.LogWarning("El deck supera el límite. Se truncará.");
            standardDeck.deck.RemoveRange(
                limit,
                standardDeck.deck.Count - limit
            );
        }
        if (standardDeck.GetLastCard() == null)
        {
            standardDeck.deck.RemoveAt(standardDeck.deck.Count - 1);
        }
    }

#endif
}
