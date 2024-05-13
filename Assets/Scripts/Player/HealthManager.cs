using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public PlayerController2 thePlayer;
    // Start is called before the first frame update
    void Start()
    {   
        currentHealth = maxHealth; 
        thePlayer = FindObjectOfType<PlayerController2>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HurtPlayer(int damage, Vector3 direction)
    {
        currentHealth -= damage;
        thePlayer.KnockBack(direction);
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
