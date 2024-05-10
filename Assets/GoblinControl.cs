using UnityEngine;

public class GoblinControl : MonoBehaviour
{
    public NPCStats NPCStats;
    public Transform target;
    public float attackRange = 2.0f; // ��������� �����
    public float cooldownDuration = 2.0f; // ����������������� �������� � ��������
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

        // ���������� �������� �����
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
            // �������� �������� ����� ������
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
        // ��������� �������� �����
        animator.SetTrigger("Attack");
        animator.SetBool("IsRunning", false);

        Debug.Log("Goblin attacks the target!");

        // ��������� ����� ����
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

        // ��������� �������� ����� �����
        canAttack = false;
        attackCooldownEndTime = Time.time + cooldownDuration;
    }

    void MoveTowardsTarget()
    {
        // ������� NPC � ����
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime);

        // �������� ����
        animator.SetBool("IsRunning", true);

        // �������� � ����
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

    // ����� ��� ��������� �����
    public void TakeDamage(int damage)
    {
        NPCStats.maxHealth -= damage;
        if (NPCStats.maxHealth <= 0)
        {
            Die();
        }
        Debug.Log("Goblin takes " + damage + " damage! Remaining health: " + NPCStats.maxHealth);
    }

    // ����� ��� ��������� ������ �������
    void Die()
    {
        Debug.Log("Goblin is defeated!");
        Destroy(gameObject);
    }
}
