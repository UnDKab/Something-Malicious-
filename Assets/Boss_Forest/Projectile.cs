using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage = 20;

    void OnCollisionEnter(Collision collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
