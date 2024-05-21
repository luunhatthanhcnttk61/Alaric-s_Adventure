using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public PlayerController2 thePlayer;

    void Start()
    {
        currentHealth = maxHealth;
        thePlayer = FindObjectOfType<PlayerController2>();
    }

    public void HurtPlayer(int damage, Vector3 direction)
    {
        currentHealth -= damage;
        thePlayer.TakeDamage(damage, direction);    
        if (currentHealth <= 0)
        {
            
        }
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
