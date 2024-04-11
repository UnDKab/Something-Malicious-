using UnityEngine;

public class GoblinControl : MonoBehaviour
{
    public NPCStats npcStats;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (npcStats == null)
        {
            Debug.LogError("NPCStats is not assigned to GoblinControl!");
            return;
        }
        npcStats.SetStatsForNPCType(npcStats.npcType);
    }

    void Update()
    {
        MoveRandomly();
    }

    void MoveRandomly()
    {
        if (npcStats == null)
        {
            Debug.LogError("NPCStats is not assigned to GoblinControl!");
            return;
        }
        float movementSpeed = npcStats.movementSpeed;
        Vector3 randomDirection = Random.insideUnitSphere * 10f;
        randomDirection.y = 0f;
        Vector3 destination = transform.position + randomDirection;

        transform.LookAt(destination);

        rb.MovePosition(Vector3.MoveTowards(transform.position, destination, movementSpeed * Time.deltaTime));
    }

    // Метод для получения урона
    public void TakeDamage(int damage)
    {
        npcStats.maxHealth -= damage;
        if (npcStats.maxHealth <= 0)
        {
            Die();
        }
    }

    // Метод для обработки смерти гоблина
    void Die()
    {
        Destroy(gameObject);
    }
}
