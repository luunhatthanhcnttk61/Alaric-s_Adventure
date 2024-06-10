using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public Image healthBarFill;
    public Image manaBarFill;
    public PlayerController2 player; 

    void Start()
    {
        if (healthBarFill == null)
        {
            Debug.LogError("HealthBar fill image is not assigned in the Inspector.");
        }

        if (manaBarFill == null)
        {
            Debug.LogError("ManaBar fill image is not assigned in the Inspector.");
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

    public void UpdateHealthBar()
    {
        if (player != null)
        {
            healthBarFill.fillAmount = (float)player.currentHealth / player.maxHealth;
            manaBarFill.fillAmount = (float)player.currentMana / player.maxMana;
            Debug.Log(player.currentHealth + "/" + player.maxHealth);
        }
    }
    public void SetColor(Color color)
    {
        healthBarFill.color = color;
        manaBarFill.color = color;
    }
}
