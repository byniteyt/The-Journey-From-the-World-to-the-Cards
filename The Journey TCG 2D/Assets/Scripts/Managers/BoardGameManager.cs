using UnityEngine;

public class BoardGameManager : MonoBehaviour
{
    public static BoardGameManager Instance { get; private set; }
    BattleRoomCard activeRoom;


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
        EventManager.SetActiveRoom += SetActiveRoom;
    }
    public void SetActiveRoom(object sender, BattleRoomCard room)
    {
        activeRoom = room;
    }

}
