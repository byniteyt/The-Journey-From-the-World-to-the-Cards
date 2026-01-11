using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

public class CollectionDataBase : MonoBehaviour
{
    public static CollectionDataBase Instance { get; private set; }

    // Cache de colecciones cargadas
    private Dictionary<string, Collection> collections =
        new Dictionary<string, Collection>();

    // Handle SOLO para la carga masiva
    private AsyncOperationHandle<IList<Collection>> loadAllHandle;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadAllCollections();
    }

    /// <summary>
    /// Carga todas las colecciones marcadas con la label "Collection"
    /// </summary>
    public void LoadAllCollections(System.Action onComplete = null)
    {
        // Evitar recargar todo dos veces
        if (loadAllHandle.IsValid())
        {
            onComplete?.Invoke();
            return;
        }

        loadAllHandle = Addressables.LoadAssetsAsync<Collection>(
            "Collection",
            collection =>
            {
                string id = collection.collectionName.ToString(); // ignorando ToString

                if (!collections.ContainsKey(id))
                {
                    collections.Add(id, collection);
                }
            }
        );

        loadAllHandle.Completed += _ => onComplete?.Invoke();
    }

    /// <summary>
    /// Obtiene una colección ya cargada
    /// </summary>
    public Collection GetCollection(string id)
    {
        collections.TryGetValue(id, out var collection);
        return collection;
    }

    public IEnumerable<Collection> GetAllCollections()
    {
        return collections.Values;
    }

    /// <summary>
    /// Carga una colección concreta por ID (Address)
    /// </summary>
    public void LoadCollectionById(string id, System.Action<Collection> onLoaded)
    {
        // Ya cargada
        if (collections.TryGetValue(id, out var cached))
        {
            onLoaded?.Invoke(cached);
            return;
        }

        var handle = Addressables.LoadAssetAsync<Collection>(id);

        handle.Completed += h =>
        {
            if (h.Status == AsyncOperationStatus.Succeeded)
            {
                collections[id] = h.Result;
                onLoaded?.Invoke(h.Result);
            }
            else
            {
                Debug.LogError($"No se pudo cargar la colección {id}");
            }
        };
    }

    private void OnDestroy()
    {
        // Liberar carga masiva
        if (loadAllHandle.IsValid())
            Addressables.Release(loadAllHandle);

        // Liberar colecciones cargadas individualmente
        foreach (var collection in collections.Values)
        {
            Addressables.Release(collection);
        }

        collections.Clear();
    }
}
