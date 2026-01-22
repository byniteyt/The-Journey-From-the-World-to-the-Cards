using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StandardDeck", menuName = "Prefabs/StandardDeck")]
public class PrefabStandDeck : ScriptableObject
{
    public StandardDeck standardDeck;
    readonly int index = 0;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (standardDeck == null)
        {
            Debug.LogWarning("El deck estándar es nulo.");
            return;
        }


        int limit = standardDeck.GetMaxLimit();

        if (standardDeck.GetDeck().Count > limit)
        {
            Debug.LogWarning("El deck supera el límite. Se truncará.");
            standardDeck.GetDeck().RemoveRange(
                limit,
                standardDeck.GetDeck().Count - limit
            );
        }
        if (standardDeck.GetLastCard() == null)
        {
            standardDeck.GetDeck().RemoveAt(standardDeck.GetDeck().Count - 1);
        }
    }

#endif
}
