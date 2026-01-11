using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CardDataBase : MonoBehaviour
{
    public static CardDataBase Instance { get; private set; }

    // Cache de colecciones cargadas
    private Dictionary<string, Card> cards =
        new Dictionary<string, Card>();

    // Handle SOLO para la carga masiva
    private AsyncOperationHandle<IList<Card>> loadAllHandle;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadCardsFromFolder("Durnei", () =>
        {
            Debug.Log("Todas las cartas de Durnei cargadas");
        });
    }

    /// <summary>
    /// Carga todas las colecciones marcadas con la label "Collection"
    /// </summary>
    public void LoadAllCards(System.Action onComplete = null)
    {
        // Evitar recargar todo dos veces
        if (loadAllHandle.IsValid())
        {
            onComplete?.Invoke();
            return;
        }

        loadAllHandle = Addressables.LoadAssetsAsync<Card>(
            "Card",
            collection =>
            {
                string id = collection.cardName.ToString(); // ignorando ToString

                if (!cards.ContainsKey(id))
                {
                    cards.Add(id, collection);
                }
            }
        );

        loadAllHandle.Completed += _ => onComplete?.Invoke();
    }

    /// <summary>
    /// Obtiene una colección ya cargada
    /// </summary>
    public Card GetCard(string id)
    {
        cards.TryGetValue(id, out var card);
        return card;
    }

    public IEnumerable<Card> GetAllCards()
    {
        return cards.Values;
    }

    /// <summary>
    /// Carga una colección concreta por ID (Address)
    /// </summary>
    public void LoadCollectionById(string id, System.Action<Card> onLoaded)
    {
        // Ya cargada
        if (cards.TryGetValue(id, out var cached))
        {
            onLoaded?.Invoke(cached);
            return;
        }

        var handle = Addressables.LoadAssetAsync<Card>(id);

        handle.Completed += h =>
        {
            if (h.Status == AsyncOperationStatus.Succeeded)
            {
                cards[id] = h.Result;
                onLoaded?.Invoke(h.Result);
            }
            else
            {
                Debug.LogError($"No se pudo cargar la colección {id}");
            }
        };
    }

    /// <summary>
    /// Carga todas las cartas que tengan una Label concreta (ej: una carpeta)
    /// </summary>
    public void LoadCardsFromFolder(string label, System.Action onComplete = null)
    {
        Addressables.LoadAssetsAsync<Card>(
            label,
            card =>
            {
                if (card == null)
                    return;

                string id = card.cardName;

                if (!cards.ContainsKey(id))
                {
                    cards.Add(id, card);
                }
                CardCollection.AddCard(card);
            }
        ).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                onComplete?.Invoke();
                Debug.Log("Cargado de cartas exitoso");
            }
            else
            {
                Debug.LogError($"Error cargando cartas de la carpeta/label {label}");
            }
        };
    }

    private void OnDestroy()
    {
        // Liberar carga masiva
        if (loadAllHandle.IsValid())
            Addressables.Release(loadAllHandle);

        // Liberar colecciones cargadas individualmente
        foreach (var collection in cards.Values)
        {
            Addressables.Release(collection);
        }

        cards.Clear();
    }
}
