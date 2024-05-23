using UnityEngine;

public class ForestManager : MonoBehaviour
{
    public static Player playerInstance;

    void Start()
    {
        // Находим объект игрока в сцене "Forest"
        playerInstance = FindObjectOfType<Player>();
    }
}
