using UnityEngine;

public class GoblinControl : MonoBehaviour
{
    public NPCStats NPCStats;
    public Transform target; // ������ �� ���� (��������, �� ������)
    public float attackRange = 2f; // ��������� �����
    public Animator animator; // ������ �� ��������� Animator

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

        // ���������, ��� animator ����������
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
            // ����� ����, ���� ��� �� �����������
            FindTarget();
        }

        if (target != null)
        {
            // ���� ���� �������, �������� ��
            AttackTarget();
        }
    }

    void FindTarget()
    {
        // ����� ���� � �������� ������������� ��������� (��������, ������)
        Player[] players = FindObjectsOfType<Player>();
        if (players.Length > 0)
        {
            // ���������� ������ ��������� ����
            target = players[0].transform;
        }
    }

    void AttackTarget()
    {
        // ��������� ���������� �� ����
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= attackRange)
        {
            // ���� ���� � �������� ��������� �����, ��������
            Attack();
        }
        else
        {
            // ���� ���� ��� ��������� �����, ���������� � ���
            MoveTowardsTarget();
        }
    }

    void Attack()
    {
        // �������� �����
        animator.SetTrigger("Attack");

        // �������� ������ ����� ����
        Debug.Log("Goblin attacks the target!");

        // ��������, ������� ����� ��������� ����� � ���� (������� �� ������ ���� ������)
        target.GetComponent<Player>().TakeDamage(NPCStats.damage);
    }

    void MoveTowardsTarget()
    {
        // �������� ����
        animator.SetBool("IsRunning", true);

        // ���������� � ����
        float movementSpeed = NPCStats.movementSpeed;
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        rb.MovePosition(transform.position + directionToTarget * movementSpeed * Time.deltaTime);

        // ���������, ��� �������� ���� �����������, ����� ������ �� �����
        animator.SetBool("IsRunning", false);
    }

    // ����� ��� ��������� �����
    public void TakeDamage(int damage)
    {
        NPCStats.maxHealth -= damage;
        if (NPCStats.maxHealth <= 0)
        {
            Die();
        }
    }

    // ����� ��� ��������� ������ �������
    void Die()
    {
        Destroy(gameObject);
    }
}
