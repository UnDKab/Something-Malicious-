using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject DoorN;
    public GameObject DoorS;
    public GameObject DoorW;
    public GameObject DoorE;
    public string roomName; // Название комнаты.
    public int roomID; // Идентификатор комнаты.

    void Start()
    {
        InitializeRoom();
    }

    // Метод для настройки комнаты.
    void InitializeRoom()
    {
        roomName = "New Room";
        roomID = Random.Range(1, 100);
    }

    public void ActivateRoom()
    {
        // Логика активации комнаты.
        gameObject.SetActive(true);
    }

    public void DeactivateRoom()
    {
        // Логика деактивации комнаты.
        gameObject.SetActive(false);
    }
    public void RotateRandomly()
    {
        int count = Random.Range(0, 4);

        for  (int i = 0; i < count; i++)
        {
            transform.Rotate(0, 90, 0);

            GameObject temp = DoorW;
            DoorW = DoorS;
            DoorS = DoorE;
            DoorE = DoorN;
            DoorN = temp;
        }
    }

}
