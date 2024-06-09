using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image m_fillBar;
    [SerializeField] TMP_Text m_status;

    private int m_currentHealth;
    private int m_baseHealth;

    public void Start()
    {
        if (m_fillBar == null || m_status == null)
        {
            Debug.LogError("m_fillBar or m_status is not assigned in HealthBar.");
            return;
        }

        m_currentHealth = 1000;
        m_baseHealth = m_currentHealth;

        m_fillBar.fillAmount = 1.0f;

        m_status.text = "Health: " + m_currentHealth + " / " + m_baseHealth;
    }


    public void UpdateHealth(int current, int baseHealth)
    {
        m_currentHealth = current;
        m_baseHealth = baseHealth;

        if (m_fillBar != null)
        {
            m_fillBar.fillAmount = (float)current / baseHealth;
        }

        if (m_status != null)
        {
            m_status.text = "Health: " + current + " / " + baseHealth;
        }
    }


    public void SetColor(Color color)
    {
        m_fillBar.color = color;
    }

    public void SetStatus(string status)
    {
        if (m_status != null)
        {
            m_status.text = status;
        }
    }

    private void Update()
    {
        Heal(10);
    }

    public void Heal(int amount)
    {
        m_currentHealth += amount;
        if (m_currentHealth > m_baseHealth)
        {
            m_currentHealth = m_baseHealth;
        }
        UpdateHealth(m_currentHealth, m_baseHealth);
    }

    public void TakeDamage(int damage)
    {
        m_currentHealth -= damage;
        if (m_currentHealth < 0)
        {
            m_currentHealth = 0;
        }
        UpdateHealth(m_currentHealth, m_baseHealth);
    }
}
