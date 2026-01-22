using UnityEngine;

// Esta clase se utilizará para esos objetos que no queramos destruir
// pero que tampoco requieran un patrón singleton.
public class DontDestroyObject : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("DontDestroyObject Awake called on " + this.gameObject.name);
        DontDestroyOnLoad(this.gameObject);
    }
}
