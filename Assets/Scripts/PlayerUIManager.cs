using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public Image healthBarFill;
    public Image manaBarFill;
    public PlayerController2 player; // Tham chiếu đến PlayerController2

    void Start()
    {
        // Đảm bảo rằng các thành phần UI đã được gán
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
        // Cập nhật thanh máu và mana dựa trên trạng thái của player
        if (player != null)
        {
            healthBarFill.fillAmount = (float)player.currentHealth / player.maxHealth;
            manaBarFill.fillAmount = (float)player.currentMana / player.maxMana;
            Debug.Log(player.currentHealth + "/" + player.maxHealth);
        }
    }
}
