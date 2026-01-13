using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CardDataBase : MonoBehaviour
{
    public static CardDataBase Instance { get; private set; }

    // Cache de colecciones cargadas
    private readonly Dictionary<string, GameObject> cards =
        new();

    // Handle SOLO para la carga masiva
    private AsyncOperationHandle<IList<GameObject>> loadAllHandle;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadCardsFromFolder("Card", () =>
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

        loadAllHandle = Addressables.LoadAssetsAsync<GameObject>(
            "Card",
            collection =>
            {
                BattleCard battleCard = collection.GetComponent<BattleCard>();
                if (battleCard == null)
                {
                    Debug.LogWarning($"{collection.name} no tiene BattleCard");
                    return;
                }

                Card cardData = battleCard.GetCard();
                if (cardData == null)
                {
                    Debug.LogWarning($"{collection.name} no tiene Card asignada");
                    return;
                }
                string id = cardData.cardName; 

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
    public GameObject GetCard(string id)
    {
        cards.TryGetValue(id, out var card);
        return card;
    }

    public IEnumerable<GameObject> GetAllCards()
    {
        return cards.Values;
    }

    /// <summary>
    /// Carga una colección concreta por ID (Address)
    /// </summary>
    public void LoadCollectionById(string id, System.Action<GameObject> onLoaded)
    {
        // Ya cargada
        if (cards.TryGetValue(id, out var cached))
        {
            onLoaded?.Invoke(cached);
            return;
        }

        var handle = Addressables.LoadAssetAsync<GameObject>(id);

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
        Addressables.LoadAssetsAsync<GameObject>(
            label,
            card =>
            {
                if (card == null)
                    return;
                Card basic = card.GetComponent<BattleCard>().GetCard();
                string id = basic.cardName;

                if (!cards.ContainsKey(id))
                {
                    cards.Add(id, card);
                }
                CardCollection.AddCard(card.GetComponent<BattleCard>());
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
    /*
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
    }*/
}
