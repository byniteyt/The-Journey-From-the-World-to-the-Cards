using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CardDataBase
{
    public class CardDataBaseSave
    {
        public List<string> cardNames = new();
    }
    public class CollectionData
    {
        public string universe;
        public List<string> universeCollections = new();
    }
    private static CardDataBase instance;
    public static CardDataBase Instance => instance ??= new CardDataBase();

    private Dictionary<string, GameObject> cards = new();

    public List<string> cardNames = new();

    private AsyncOperationHandle<IList<GameObject>> loadAllHandle;
    private bool isLoaded = false;

    private const string SAVE_FILE = "CardDatabase.json";
    private const string CARD_LABEL = "Durnei";

    private CardDataBase() { }

    #region Public API

    /// <summary>
    /// Carga todas las cartas desde Addressables (una sola vez).
    /// </summary>
    public void LoadAllCards(Action onComplete = null)
    {
        if (isLoaded)
        {
            Debug.Log("Las cartas ya están cargadas.");
            onComplete?.Invoke();
            return;
        }

        loadAllHandle = Addressables.LoadAssetsAsync<GameObject>(
            CARD_LABEL,
            OnCardLoaded
        );

        loadAllHandle.Completed += handle =>
        {
            OnLoadAllCompleted(handle);
            onComplete?.Invoke(); // <- ahora se llama siempre
        };
    }

    /// <summary>
    /// Devuelve un prefab cargado por Address.
    /// </summary>
    public GameObject GetObjectCard(string address)
    {
        cards.TryGetValue(address, out var obj);
        return obj;
    }

    public BattleCard GetBattleCard(string address)
    {
        return GetObjectCard(address)?.GetComponent<BattleCard>();
    }

    public Card GetCard(string address)
    {
        return GetBattleCard(address)?.GetCard();
    }

    public IEnumerable<GameObject> GetAllCards()
    {
        return cards.Values;
    }

    #endregion
    public bool HasNewCards(IEnumerable<string> loadedCardNames)
    {
        return loadedCardNames.Any(name => !cardNames.Contains(name));
    }



    #region Loaders

    private void OnCardLoaded(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogWarning("Prefab de carta es null");
            return;
        }

        var battleCard = prefab.GetComponent<BattleCard>();
        if (battleCard == null)
        {
            Debug.LogWarning($"{prefab.name} no tiene BattleCard");
            return;
        }

        // IMPORTANTE: usamos el Address, no el nombre lógico
        string address = prefab.GetComponent<BattleCard>().GetCard().cardName;

        if (!cards.ContainsKey(address))
        {
            cards.Add(address, prefab);
        }
    }

    private void OnLoadAllCompleted(AsyncOperationHandle<IList<GameObject>> handle)
    {
        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("Error cargando cartas desde Addressables");
            return;
        }

        isLoaded = true;

        Debug.Log($"Cartas cargadas correctamente: {cards.Count}");

        if (cards.Count > 0)
        {
            SaveDatabase();
        }
    }

    #endregion

    #region Save / Load

    private void SaveDatabase()
    {
        var data = new CardDataBaseSave
        {
            cardNames = cards.Keys.ToList()
        };

        Debug.Log($"Guardando base de datos de cartas ({data.cardNames.Count})");

        SaveData<CardDataBaseSave>.SerializeJSON(data, SAVE_FILE);
    }

    public void LoadDatabaseFromFile(Action onComplete = null)
    {
        if (!SaveData<List<string>>.SaveDataExists(SAVE_FILE))
        {
            onComplete?.Invoke();
            return;
        }

        var ids = SaveData<List<string>>.DeserializeJSON(SAVE_FILE);
        if (ids == null || ids.Count == 0)
        {
            onComplete?.Invoke();
            return;
        }

        int pending = ids.Count;

        foreach (var address in ids)
        {
            LoadCardByAddress(address, _ =>
            {
                pending--;
                if (pending == 0)
                {
                    onComplete?.Invoke();
                }
            });
        }

    }

    private void LoadCardByAddress(string address, Action<GameObject> onLoaded)
    {
        if (cards.TryGetValue(address, out var cached))
        {
            onLoaded?.Invoke(cached);
            return;
        }

        var handle = Addressables.LoadAssetAsync<GameObject>(address);
        handle.Completed += h =>
        {
            if (h.Status == AsyncOperationStatus.Succeeded)
            {
                cards[address] = h.Result;
                onLoaded?.Invoke(h.Result);
            }
            else
            {
                Debug.LogError($"No se pudo cargar la carta con address: {address}");
            }
        };
    }

    #endregion

    #region Cleanup (opcional)

    public void Clear()
    {
        if (loadAllHandle.IsValid())
        {
            Addressables.Release(loadAllHandle);
        }

        cards.Clear();
        isLoaded = false;
    }

    #endregion
}
