using UnityEngine;

public class GoblinControl : MonoBehaviour
{
    public NPCStats NPCStats;
    public Transform target; // Ссылка на цель (например, на игрока)
    public float attackRange = 2f; // Дистанция атаки
    public Animator animator; // Ссылка на компонент Animator

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (NPCStats == null)
        {
            Debug.LogError("NPCStats is not assigned to GoblinControl!");
            return;
        }
        NPCStats.SetStatsForNPCType(NPCStats.npcType);

        // Убедитесь, что animator установлен
        if (animator == null)
        {
            Debug.LogError("Animator is not assigned to GoblinControl!");
            return;
        }
    }

    void Update()
    {
        if (target == null)
        {
            // Поиск цели, если она не установлена
            FindTarget();
        }

        if (target != null)
        {
            // Если цель найдена, атакуйте ее
            AttackTarget();
        }
    }

    void FindTarget()
    {
        // Поиск цели в пределах определенного диапазона (например, игрока)
        Player[] players = FindObjectsOfType<Player>();
        if (players.Length > 0)
        {
            // Установите первую найденную цель
            target = players[0].transform;
        }
    }

    void AttackTarget()
    {
        // Вычислите расстояние до цели
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= attackRange)
        {
            // Если цель в пределах дистанции атаки, атакуйте
            Attack();
        }
        else
        {
            // Если цель вне дистанции атаки, двигайтесь к ней
            MoveTowardsTarget();
        }
    }

    void Attack()
    {
        // Анимация атаки
        animator.SetTrigger("Attack");

        // Добавьте логику атаки цели
        Debug.Log("Goblin attacks the target!");

        // Например, вызвать метод получения урона у цели (зависит от вашего кода игрока)
        target.GetComponent<Player>().TakeDamage(NPCStats.damage);
    }

    void MoveTowardsTarget()
    {
        // Анимация бега
        animator.SetBool("IsRunning", true);

        // Двигайтесь к цели
        float movementSpeed = NPCStats.movementSpeed;
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        rb.MovePosition(transform.position + directionToTarget * movementSpeed * Time.deltaTime);

        // Убедитесь, что анимация бега отключается, когда гоблин не бежит
        animator.SetBool("IsRunning", false);
    }

    // Метод для получения урона
    public void TakeDamage(int damage)
    {
        NPCStats.maxHealth -= damage;
        if (NPCStats.maxHealth <= 0)
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
