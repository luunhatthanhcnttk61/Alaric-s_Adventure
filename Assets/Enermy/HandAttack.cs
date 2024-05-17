using UnityEngine;

public class HandAttack : MonoBehaviour
{
    public int damage = 10; // Lượng sát thương gây ra
    private bool canDamage = false;

    void OnTriggerEnter(Collider other)
    {
        if (canDamage && other.CompareTag("Player"))
        {
            HealthManager playerHealth = other.GetComponent<HealthManager>();
            if (playerHealth != null)
            {
                Vector3 direction = (other.transform.position - transform.position).normalized; // Hướng đẩy lùi
                playerHealth.HurtPlayer(damage, direction);
            }
        }
    }

    public void EnableDamage()
    {
        canDamage = true;
    }

    public void DisableDamage()
    {
        canDamage = false;
    }
}
