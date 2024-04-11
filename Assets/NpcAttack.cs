using UnityEngine;

public class NPCAttack : MonoBehaviour
{
    public Transform attackPoint; // Точка атаки NPC
    public float attackRange = 1f; // Дальность атаки NPC
    public LayerMask playerLayer; // Слой игрока
    public int attackDamage = 10; // Урон атаки NPC

    void Update()
    {
        // Проверяем, существует ли атакующая точка
        if (attackPoint == null)
        {
            // Если точка атаки не установлена, используем позицию NPC
            attackPoint = transform;

            Debug.LogWarning("Attack point is not assigned! Using NPC position as attack point.");
        }

        // Проверяем, находится ли игрок в диапазоне атаки NPC
        Collider[] hitPlayers = Physics.OverlapSphere(attackPoint.position, attackRange, playerLayer);

        // Наносим урон каждому обнаруженному игроку
        foreach (Collider player in hitPlayers)
        {
            // Получаем компонент PlayerStats игрока
            PlayerStats playerStats = player.GetComponent<PlayerStats>();

            // Если компонент PlayerStats существует, наносим игроку урон
            if (playerStats != null)
            {
                playerStats.TakeDamage(attackDamage);
            }
        }
    }

    // Визуализация области атаки в редакторе Unity
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
