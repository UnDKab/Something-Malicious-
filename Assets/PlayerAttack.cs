using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint; // �����, ������ ����� ����������� �����
    public float attackRange = 1f;
    public LayerMask enemyLayers;

    void Update()
    {
        // ���������, ���� �� ������ ������ ��� �����
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void Attack()
    {
        if (attackPoint == null)
        {
            Debug.LogError("Attack point is not assigned!");
            return;
        }

        // ������������ ���� ������ � ������� �����
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        // ������� ���� ������� ������������� �����
        foreach (Collider enemy in hitEnemies)
        {
            // �������� ��������� GoblinControl �����
            GoblinControl goblinControl = enemy.GetComponent<GoblinControl>();

            // ���� ��������� GoblinControl ����� ����������, ������� ��� ����
            if (goblinControl != null)
            {
                goblinControl.TakeDamage(10); // ��������������, ��� ����� TakeDamage ��������� � GoblinControl
            }
        }
    }

    // ������������ ������� �����
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
