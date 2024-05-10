using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomsPlacer : MonoBehaviour
{
    public Room[] RoomPrefabs; // Массив с префабами комнат
    public Room StartingRoomPrefab; // Префаб стартовой комнаты

    private Room[,] spawnedRooms;

    void Start()
    {
        spawnedRooms = new Room[100, 100];

        // Создаем стартовую комнату в центре массива (5, 5)
        spawnedRooms[5, 5] = Instantiate(StartingRoomPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        // Размещаем дополнительные комнаты
        for (int i = 0; i < 101; i++)
        {
            PlaceOneRoom();
        }
        ConfigureStartingRoomDoors();
    }

    private void PlaceOneRoom()
    {
        // Найдем свободные позиции для размещения комнат
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();

        // Пройдите по всем клеткам в массиве
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                // Проверяем, есть ли комната на текущей позиции
                if (spawnedRooms[x, y] == null) continue;

                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;
                if (x > 0 && spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y > 0 && spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                if (x < maxX && spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (y < maxY && spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
            }
        }

        Room newRoom = Instantiate(RoomPrefabs[Random.Range(0, RoomPrefabs.Length)]);

        int limit = 500;
        while(limit-- > 0)
        {
            Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
            newRoom.RotateRandomly();

            if(ConnectToSomething(newRoom, position))
            {
                newRoom.transform.position = new Vector3(position.x - 5, 0, position.y - 5) * 12;
                spawnedRooms[position.x, position.y] = newRoom;
                return;
            }
        }

        
    }

    private bool ConnectToSomething (Room room, Vector2Int p)
    {
        int maxX = spawnedRooms.GetLength(0) - 1;
        int maxY = spawnedRooms.GetLength(1) - 1;

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room.DoorN != null && p.y + 1 <= maxY && spawnedRooms[p.x, p.y + 1] != null && spawnedRooms[p.x, p.y + 1]?.DoorS != null)
            neighbours.Add(Vector2Int.up);
        if (room.DoorS != null && p.y - 1 >= 0 && spawnedRooms[p.x, p.y - 1] != null && spawnedRooms[p.x, p.y - 1]?.DoorN != null)
            neighbours.Add(Vector2Int.down);
        if (room.DoorE != null && p.x + 1 <= maxX && spawnedRooms[p.x + 1, p.y] != null && spawnedRooms[p.x + 1, p.y]?.DoorW != null)
            neighbours.Add(Vector2Int.right);
        if (room.DoorW != null && p.x - 1 >= 0 && spawnedRooms[p.x - 1, p.y] != null && spawnedRooms[p.x - 1, p.y]?.DoorE != null)
            neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0)
        {
            return false;
        }

        Vector2Int selectedDirection = neighbours[Random.Range(0, neighbours.Count)];
        Room selectedRoom = spawnedRooms[p.x + selectedDirection.x, p.y + selectedDirection.y];

        if (selectedDirection == Vector2Int.up)
        {
            room.DoorN.SetActive(false);
            selectedRoom.DoorS.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.down)
        {
            room.DoorS.SetActive(false);
            selectedRoom.DoorN.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.right)
        {
            room.DoorE.SetActive(false);
            selectedRoom.DoorW.SetActive(false);
        }
        else if (selectedDirection == Vector2Int.left)
        {
            room.DoorW.SetActive(false);
            selectedRoom.DoorE.SetActive(false);
        }

        return true;
    }

    void ConfigureStartingRoomDoors()
    {
        // Координаты стартовой комнаты
        int startX = 5;
        int startY = 5;

        Room startingRoom = spawnedRooms[startX, startY];

        // Проверяем северную дверь
        if (startY + 1 < spawnedRooms.GetLength(1) && spawnedRooms[startX, startY + 1] != null)
        {
            startingRoom.DoorN.SetActive(false); // Есть комната - деактивируем дверь
        }
        else
        {
            startingRoom.DoorN.SetActive(true); // Нет комнаты - активируем дверь
        }

        // Проверяем южную дверь
        if (startY - 1 >= 0 && spawnedRooms[startX, startY - 1] != null)
        {
            startingRoom.DoorS.SetActive(false); // Есть комната - деактивируем дверь
        }
        else
        {
            startingRoom.DoorS.SetActive(true); // Нет комнаты - активируем дверь
        }

        // Проверяем восточную дверь
        if (startX + 1 < spawnedRooms.GetLength(0) && spawnedRooms[startX + 1, startY] != null)
        {
            startingRoom.DoorE.SetActive(false); // Есть комната - деактивируем дверь
        }
        else
        {
            startingRoom.DoorE.SetActive(true); // Нет комнаты - активируем дверь
        }
        // Проверяем западную дверь
        if (startX - 1 >= 0 && spawnedRooms[startX - 1, startY] != null)
        {
            startingRoom.DoorW.SetActive(false); // Есть комната - деактивируем дверь
        }
        else
        {
            startingRoom.DoorW.SetActive(true); // Нет комнаты - активируем дверь
        }
    }


}