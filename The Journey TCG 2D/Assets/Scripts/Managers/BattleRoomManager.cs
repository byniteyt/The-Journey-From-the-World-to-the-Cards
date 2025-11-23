using UnityEngine;

public class BattleRoomManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventManager.SetActiveRoom += LoadRoom;
    }
    void LoadRoom(object sender, RoomCard roomCard)
    {
        if (this.transform.childCount > 0)
        {
            RoomCard oldRoom = GetComponentInChildren<RoomCard>();
            if (oldRoom != null)
            {
                oldRoom.OnDestroyRoom();
                Destroy(oldRoom.gameObject);
            }
        }
        GameObject newRoom= Instantiate(roomCard.gameObject, transform);
        Destroy(roomCard.gameObject);
        Debug.Log($"Loading room: {roomCard.cardName}");
        newRoom.transform.localPosition = Vector3.zero;
        // Apply room effects here
    }
    void OnDestroy()
    {
        EventManager.SetActiveRoom -= LoadRoom;
    }
}
