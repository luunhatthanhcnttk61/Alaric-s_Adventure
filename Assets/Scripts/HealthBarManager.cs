using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    public Image healthBarFill;
    public PlayerController2 player;
    public Text healthValue;
    // Start is called before the first frame update
    void Start()
    {
        if (healthBarFill == null)
        {
            Debug.LogError("HealthBar fill image is not assigned in the Inspector.");
        }
        if (healthValue == null)
        {
            Debug.LogError("Health Value Text is not assigned in the Inspector.");
        }
        if (player == null)
        {
            player = FindObjectOfType<PlayerController2>();
            if (player == null)
            {
                Debug.LogError("PlayerController2 script is not found in the scene.");
            }
        }
        
    }
    public void Update()
    {
        healthValue.text = player.currentHealth + "/" + player.maxHealth;
    }

    public void UpdateHealth()
    {
        healthBarFill.fillAmount = (float)player.currentHealth / player.maxHealth;
        healthValue.text = player.currentHealth + "/" + player.maxHealth;
    }

    public void SetColor(Color color)
    {
        healthBarFill.color = color;
    }
}
