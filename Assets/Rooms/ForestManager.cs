using UnityEngine;

public class ForestManager : MonoBehaviour
{
    public static Player playerInstance;

    void Start()
    {
        // ������� ������ ������ � ����� "Forest"
        playerInstance = FindObjectOfType<Player>();
    }
}
