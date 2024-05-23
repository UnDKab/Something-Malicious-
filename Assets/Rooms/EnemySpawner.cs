using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Массив префабов врагов
    public Transform[] spawnPoints; // Массив точек спавна

    private void Start()
    {
        SpawnRandomEnemies();
    }

    private void SpawnRandomEnemies()
    {
        // Генерируем случайное число от 0 до 10 для определения количества врагов
        int enemyCount = Random.Range(0, 11);

        // Спавним рандомное количество врагов
        for (int i = 0; i < enemyCount; i++)
        {
            // Выбираем случайную точку спавна
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            // Выбираем случайного врага из массива префабов
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            // Спавним врага на выбранной точке спавна
            Instantiate(randomEnemyPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);
        }
    }
}
