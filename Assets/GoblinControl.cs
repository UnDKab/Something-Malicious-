using UnityEngine;

public class GoblinControl : MonoBehaviour
{
    public NPCStats NPCStats;
    public Transform target;
    public float attackRange = 2.0f; // Дистанция атаки
    public float cooldownDuration = 2.0f; // Продолжительность кулдауна в секундах
    public Animator animator;
    private Vector3 lastTargetPosition;
    private float attackCooldownEndTime;
    private bool canAttack = true;

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
            FindTarget();
        }

        // Обновление кулдауна атаки
        if (!canAttack && Time.time >= attackCooldownEndTime)
        {
            canAttack = true;
        }

        if (target != null)
        {
            AttackTarget();
        }
    }

    void FindTarget()
    {
        Player[] players = FindObjectsOfType<Player>();
        if (players.Length > 0)
        {
            target = players[0].transform;
        }
    }

    void AttackTarget()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= attackRange)
        {
            // Проверка кулдауна перед атакой
            if (canAttack)
            {
                Attack();
            }
        }
        else
        {
            MoveTowardsTarget();
        }
    }

    void Attack()
    {
        // Запускаем анимацию атаки
        animator.SetTrigger("Attack");
        animator.SetBool("IsRunning", false);

        Debug.Log("Goblin attacks the target!");

        // Нанесение урона цели
        Player player = target.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(NPCStats.damage);
            Debug.Log("Goblin deals " + NPCStats.damage + " damage to the player!");
        }
        else
        {
            Debug.LogWarning("Goblin failed to find player target!");
        }

        // Установка кулдауна после атаки
        canAttack = false;
        attackCooldownEndTime = Time.time + cooldownDuration;
    }

    void MoveTowardsTarget()
    {
        // Поворот NPC к цели
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime);

        // Анимация бега
        animator.SetBool("IsRunning", true);

        // Движение к цели
        float movementSpeed = NPCStats.movementSpeed;
        rb.MovePosition(transform.position + transform.forward * movementSpeed * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            if (target.position != lastTargetPosition)
            {
                lastTargetPosition = target.position;
                MoveTowardsTarget();
            }
        }
    }

    // Метод для получения урона
    public void TakeDamage(int damage)
    {
        NPCStats.maxHealth -= damage;
        if (NPCStats.maxHealth <= 0)
        {
            Die();
        }
        Debug.Log("Goblin takes " + damage + " damage! Remaining health: " + NPCStats.maxHealth);
    }

    // Метод для обработки смерти гоблина
    void Die()
    {
        Debug.Log("Goblin is defeated!");
        Destroy(gameObject);
    }
}
