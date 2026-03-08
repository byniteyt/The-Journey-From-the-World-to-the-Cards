using UnityEngine;

public class CDBInitializer : MonoBehaviour
{
    int loaded = 0;
    string[] collections = {
        CollectionName.Durnei.ToString(),
        CollectionName.Toyring.ToString()
    };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameStarter.Instance.LoadGame += (sender, args) =>
        {
            LoadAvailableCollections();
        };
        LoadAvailableCollections();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LoadAvailableCollections()
    {
        GameStarter.Instance.TextInfo("Cargando colecciones disponibles...");
        foreach (var collection in collections)
        {
            CardDataBase.Instance.LoadCollection(collection, () =>
            {
                Debug.Log($"Colección {collection} cargada correctamente");
                loaded++;
                if ( loaded == collections.Length ) 
                {
                    Debug.Log("Todas las colecciones cargadas...");
                    GameStarter.Instance.NextLoad();
                }
            });
        }
        // Aquí puedes cargar las colecciones disponibles desde Addressables o cualquier otra fuente de datos
        // Por ejemplo, podrías tener una lista de etiquetas para cada colección y cargar las cartas correspondientes
        // CardDataBase.Instance.LoadCollection("CollectionLabel", () => { /* Callback después de cargar la colección */ });

    }
}
