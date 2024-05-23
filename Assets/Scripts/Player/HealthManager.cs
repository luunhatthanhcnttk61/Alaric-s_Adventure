// using UnityEngine;

// public class HealthManager : MonoBehaviour
// {
//     public int maxHealth;
//     public int currentHealth;

//     public PlayerController2 thePlayer;

//     void Start()
//     {
//         currentHealth = maxHealth;
//         thePlayer = FindObjectOfType<PlayerController2>();
//     }

//     public void HurtPlayer(int damage, Vector3 direction)
//     {
//         currentHealth -= damage;
//         thePlayer.TakeDamage(damage, direction);    
//         if (currentHealth <= 0)
//         {
            
//         }
//     }

//     public void HealPlayer(int healAmount)
//     {
//         currentHealth += healAmount;
//         if (currentHealth > maxHealth)
//         {
//             currentHealth = maxHealth;
//         }
//     }
// }
using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public PlayerController2 thePlayer;
    public int healthRegenRate = 2; // Tốc độ hồi máu mỗi giây
    public float timeToRegen = 2f; // Thời gian chờ để bắt đầu hồi máu

    private float lastDamageTime; // Thời gian cuối cùng nhận sát thương

    void Start()
    {
        currentHealth = maxHealth;
        thePlayer = FindObjectOfType<PlayerController2>();
        lastDamageTime = Time.time; // Khởi tạo thời gian cuối cùng nhận sát thương
        StartCoroutine(RegenHealth()); // Bắt đầu Coroutine hồi máu
    }

    public void HurtPlayer(int damage, Vector3 direction)
    {
        currentHealth -= damage;
        lastDamageTime = Time.time; // Cập nhật thời gian nhận sát thương
        thePlayer.TakeDamage(damage, direction);
        if (currentHealth <= 0)
        {
            // Xử lý khi máu bằng 0 (ví dụ: game over)
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

    private IEnumerator RegenHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Kiểm tra mỗi giây

            if (Time.time - lastDamageTime >= timeToRegen && currentHealth < maxHealth)
            {
                HealPlayer(healthRegenRate); // Hồi máu nếu không nhận sát thương trong vòng 2 giây
                Debug.Log($"Player healed by {healthRegenRate}. Current health: {currentHealth}");
            }
        }
    }
}
