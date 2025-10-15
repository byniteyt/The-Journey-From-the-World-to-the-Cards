using UnityEngine;

public class BoardGameManager : MonoBehaviour
{
    public static BoardGameManager Instance { get; private set; }
    RoomCard activeRoom = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void SetActiveRoom(RoomCard room)
    {
        if(activeRoom != null)
        {
            activeRoom.OnDestroyRoom();
        }
        activeRoom = room;
    }
}
