using UnityEngine;

public class NPCAttack : MonoBehaviour
{
    public Transform attackPoint; // ����� ����� NPC
    public float attackRange = 1f; // ��������� ����� NPC
    public LayerMask playerLayer; // ���� ������
    public int attackDamage = 10; // ���� ����� NPC

    void Update()
    {
        // ���������, ���������� �� ��������� �����
        if (attackPoint == null)
        {
            // ���� ����� ����� �� �����������, ���������� ������� NPC
            attackPoint = transform;

            Debug.LogWarning("Attack point is not assigned! Using NPC position as attack point.");
        }

        // ���������, ��������� �� ����� � ��������� ����� NPC
        Collider[] hitPlayers = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

        // ������� ���� ������� ������������� ������
        foreach (Collider player in hitPlayers)
        {
            // �������� ��������� PlayerStats ������
            PlayerStats playerStats = player.GetComponent<PlayerStats>();

            // ���� ��������� PlayerStats ����������, ������� ������ ����
            if (playerStats != null)
            {
                playerStats.TakeDamage(attackDamage);
            }
        }
    }

    // ������������ ������� ����� � ��������� Unity
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
