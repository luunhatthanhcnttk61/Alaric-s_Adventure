using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour
{
    public PlayerController2 thePlayer;
    public int healthRegenRate = 2; 
    public float timeToRegen = 2f; 

    private float lastDamageTime; 

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController2>();
        lastDamageTime = Time.time; 
        StartCoroutine(RegenHealth()); 
    }

    public void HurtPlayer(int damage, Vector3 direction)
    {
        thePlayer.currentHealth -= damage;
        lastDamageTime = Time.time; 
        thePlayer.TakeDamage(damage, direction);
        // if (thePlayer.currentHealth <= 0)
        // {
            
        // }
    }

    public void HealPlayer(int healAmount)
    {
        if(thePlayer.currentHealth > 0)
        {
            thePlayer.currentHealth += healAmount;
            thePlayer.currentHealth = Mathf.Clamp(thePlayer.currentHealth, 0, thePlayer.maxHealth);
            FindObjectOfType<HealthBarManager>().UpdateHealth();
            if (thePlayer.currentHealth > thePlayer.maxHealth)
            {
                thePlayer.currentHealth = thePlayer.maxHealth;
                FindObjectOfType<HealthBarManager>().UpdateHealth();
            }
        }
    }

    private IEnumerator RegenHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); 

            if (Time.time - lastDamageTime >= timeToRegen && thePlayer.currentHealth < thePlayer.maxHealth && thePlayer.currentHealth > 0)
            {
                HealPlayer(healthRegenRate);
            }
        }
    }
}
