using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour
{
    public PlayerController2 thePlayer;
    public int healthRegenRate = 2; // Tốc độ hồi máu mỗi giây
    public float timeToRegen = 2f; // Thời gian chờ để bắt đầu hồi máu

    private float lastDamageTime; // Thời gian cuối cùng nhận sát thương

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController2>();
        lastDamageTime = Time.time; // Khởi tạo thời gian cuối cùng nhận sát thương
        StartCoroutine(RegenHealth()); // Bắt đầu Coroutine hồi máu
    }

    public void HurtPlayer(int damage, Vector3 direction)
    {
        thePlayer.currentHealth -= damage;
        lastDamageTime = Time.time; // Cập nhật thời gian nhận sát thương
        thePlayer.TakeDamage(damage, direction);
        if (thePlayer.currentHealth <= 0)
        {
            // Xử lý khi máu bằng 0 (ví dụ: game over)
        }
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
            yield return new WaitForSeconds(1f); // Kiểm tra mỗi giây

            if (Time.time - lastDamageTime >= timeToRegen && thePlayer.currentHealth < thePlayer.maxHealth)
            {
                HealPlayer(healthRegenRate); // Hồi máu nếu không nhận sát thương trong vòng 2 giây
                Debug.Log($"Player healed by {healthRegenRate}. Current health: {thePlayer.currentHealth}");
            }
        }
    }
}
