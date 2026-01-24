using UnityEngine;

// Esta clase se utilizará para esos objetos que no queramos destruir
// pero que tampoco requieran un patrón singleton.
public class DontDestroyObject : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
