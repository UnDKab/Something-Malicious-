using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    public int maxHealth = 1000;
    private int currentHealth;

    public Transform attackPoint1;
    public Transform attackPoint2;
    public GameObject proj1;
    public GameObject proj2;
    public LayerMask playerLayer;
    public float attackRange = 1f;

    private Animator animator;
    private NavMeshAgent agent;
    public Transform player; // Ссылка на игрока

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Пример поведения для атак
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer < 10f)
        {
            int attackType = Random.Range(1, 4); // Случайно выбираем тип атаки
            switch (attackType)
            {
                case 1:
                    animator.SetTrigger("Attack");
                    break;
                case 2:
                    animator.SetTrigger("Attack2");
                    break;
                case 3:
                    animator.SetTrigger("ArmatureAction");
                    break;
            }
        }
        else
        {
            agent.SetDestination(player.position); // Босс идет к игроку
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
        agent.isStopped = true;
        Invoke("GoToMainMenu", 3f); // Переход на сцену MainMenu через 3 секунды после смерти босса
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Методы для выполнения атак, вызываемые из анимаций
    void PerformAttack()
    {
        Shoot(proj1, attackPoint1);
    }

    void PerformAttack2()
    {
        Shoot(proj2, attackPoint2);
    }

    void PerformArmatureAction()
    {
        RainProjectiles(proj1);
    }

    void Shoot(GameObject projectile, Transform attackPoint)
    {
        Instantiate(projectile, attackPoint.position, attackPoint.rotation);
    }

    void RainProjectiles(GameObject projectile)
    {
        Vector3 targetPosition = player.position;
        for (int i = 0; i < 5; i++) // Создаем 5 снарядов
        {
            Vector3 spawnPosition = targetPosition + new Vector3(Random.Range(-3f, 3f), 10, Random.Range(-3f, 3f));
            Instantiate(projectile, spawnPosition, Quaternion.identity);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint1 != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint1.position, attackRange);
        }
        if (attackPoint2 != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(attackPoint2.position, attackRange);
        }
    }
}
