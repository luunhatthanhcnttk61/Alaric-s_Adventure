using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HurtPlayer(int damage)
    {
        currentHealth -= damage;
    }
    public void HealPlayer(int healMount)
    {
        currentHealth += healMount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
