using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "CollectionData", menuName = "CollectionData")]
public class CollectionData : ScriptableObject
{
    public AssetReferenceT<Collection>[] collections;
}
