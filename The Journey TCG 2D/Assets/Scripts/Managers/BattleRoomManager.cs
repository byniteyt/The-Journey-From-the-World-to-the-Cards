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
            RoomCard oldRoom = this.transform.GetChild(0).GetComponent<RoomCard>();
            if (oldRoom != null)
            {
                oldRoom.OnDestroyRoom();
                Destroy(oldRoom.gameObject);
            }
        }
        Debug.Log($"Loading room: {roomCard.cardName}");
        roomCard.transform.SetParent(this.transform);
        roomCard.transform.localPosition = Vector3.zero;
        // Apply room effects here
    }
}
