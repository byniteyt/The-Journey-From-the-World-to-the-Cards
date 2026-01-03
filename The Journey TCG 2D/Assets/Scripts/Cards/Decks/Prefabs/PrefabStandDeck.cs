using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StandardDeck", menuName = "Prefabs/StandardDeck")]
public class PrefabStandDeck : ScriptableObject
{
    public StandardDeck standardDeck;

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

        for (int i = standardDeck.deck.Count - 1; i >= 0; i--)
        {
            if (standardDeck.deck[i] == null)
            {
                standardDeck.deck.RemoveAt(i);
                continue;
            }
            if (standardDeck.GetCardLimit(standardDeck.deck[i]) > standardDeck.GetMaxLimit())
            {
                standardDeck.deck.RemoveAt(i);
                Debug.LogWarning("Se ha eliminado una carta que excedía el límite permitido.");
                continue;
            }

            if (standardDeck.deck[i] != null)
            {
                string key = standardDeck.deck[i].name;
                if (!standardDeck.GetDictionary().ContainsKey(key))
                    standardDeck.GetDictionary()[key] = 1;
                else
                    standardDeck.GetDictionary()[key]++;
            }

        }
    }

#endif
}
