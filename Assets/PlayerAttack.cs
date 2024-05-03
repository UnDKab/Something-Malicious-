using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 1f;
    public LayerMask enemyLayers;
    public WeaponController weaponController;

    void Update()
    {
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

        if (weaponController == null)
        {
            Debug.LogError("No weapon equipped!");
            return;
        }

        // Получаем общий урон из WeaponController
        float totalDamage = weaponController.TotalDamage;

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            GoblinControl goblinControl = enemy.GetComponent<GoblinControl>();

            if (goblinControl != null)
            {
                goblinControl.TakeDamage(Mathf.RoundToInt(totalDamage));
                Debug.Log("Dealt " + totalDamage + " damage to enemy!");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private WeaponModel GetCurrentWeaponModel()
    {
        if (attackPoint == null || attackPoint.childCount == 0)
            return null;

        Transform weapon = attackPoint.GetChild(0);
        return weapon.GetComponent<WeaponModel>();
    }
}
