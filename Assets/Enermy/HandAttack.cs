using UnityEngine;
using System.Collections.Generic;

public class HandAttack : MonoBehaviour
{
    public int damage = 10; // Lượng sát thương gây ra
    private bool canDamage = false;
    private HashSet<Collider> damagedTargets = new HashSet<Collider>();
    public Collider handCollider;
    
    public void Start()
    {
        //handCollider.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (canDamage && other.CompareTag("Player") && !damagedTargets.Contains(other))
        {
            HealthManager playerHealth = other.GetComponent<HealthManager>();
            if (playerHealth != null)
            {
                Vector3 direction = (other.transform.position - transform.position).normalized; // Hướng đẩy lùi
                playerHealth.HurtPlayer(damage, direction);
                damagedTargets.Add(other); 
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (damagedTargets.Contains(other))
        {
            damagedTargets.Remove(other); 
        }
    }

    public void EnableDamage()
    {
        canDamage = true; 
    }

    public void DisableDamage()
    {
        canDamage = false;
        damagedTargets.Clear(); 
    }
}
    