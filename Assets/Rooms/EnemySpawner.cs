using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // ������ �������� ������
    public Transform[] spawnPoints; // ������ ����� ������

    private void Start()
    {
        SpawnRandomEnemies();
    }

    private void SpawnRandomEnemies()
    {
        // ���������� ��������� ����� �� 0 �� 10 ��� ����������� ���������� ������
        int enemyCount = Random.Range(0, 11);

        // ������� ��������� ���������� ������
        for (int i = 0; i < enemyCount; i++)
        {
            // �������� ��������� ����� ������
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            // �������� ���������� ����� �� ������� ��������
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            // ������� ����� �� ��������� ����� ������
            Instantiate(randomEnemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
        }
    }
}
