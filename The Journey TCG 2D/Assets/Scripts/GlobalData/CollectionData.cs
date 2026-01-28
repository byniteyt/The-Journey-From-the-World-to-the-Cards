using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "CollectionData", menuName = "Scriptable Objects/CollectionData")]
public class CollectionData : ScriptableObject
{
    public AssetReferenceT<Collection>[] collections;
}
