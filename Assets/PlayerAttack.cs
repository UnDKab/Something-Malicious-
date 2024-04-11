using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint; // точка, откуда будет происходить атака
    public float attackRange = 1f;
    public LayerMask enemyLayers;

    void Update()
    {
        // Проверяем, была ли нажата кнопка для атаки
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

        // Обнаруживаем всех врагов в радиусе атаки
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        // Наносим урон каждому обнаруженному врагу
        foreach (Collider enemy in hitEnemies)
        {
            // Получаем компонент GoblinControl врага
            GoblinControl goblinControl = enemy.GetComponent<GoblinControl>();

            // Если компонент GoblinControl врага существует, наносим ему урон
            if (goblinControl != null)
            {
                goblinControl.TakeDamage(10); // Предполагается, что метод TakeDamage определен в GoblinControl
            }
        }
    }

    // Визуализация области атаки
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
