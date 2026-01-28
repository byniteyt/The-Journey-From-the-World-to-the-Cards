using UnityEngine;

public class BattleRoomManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventManager.SetActiveRoom += LoadRoom;
    }
    void LoadRoom(object sender, BattleRoomCard roomCard)
    {
        if (this.transform.childCount > 0)
        {
            BattleRoomCard oldRoom = GetComponentInChildren<BattleRoomCard>();
            if (oldRoom != null)
            {
                oldRoom.GetRoom().OnDestroyRoom();
                Destroy(oldRoom.gameObject);
            }
        }
        GameObject newRoom= Instantiate(roomCard.gameObject, transform);
        Debug.Log($"Loading room: {roomCard.GetRoom().cardName}");
        newRoom.transform.localPosition = Vector3.zero;
        // Apply room effects here
    }
    void OnDestroy()
    {
        EventManager.SetActiveRoom -= LoadRoom;
    }
}
