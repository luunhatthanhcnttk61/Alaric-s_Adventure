using UnityEngine;
using UnityEngine.UI;

public class EnermyHealth : MonoBehaviour
{
    public EnermyAI enermyAI;
    public Image healthBarFill; 

    void Start()
    {
        if (healthBarFill == null)
        {
            Debug.LogError("HealthBar fill image is not assigned in the Inspector.");
        }
        if (enermyAI == null)
        {
            enermyAI = FindObjectOfType<EnermyAI>();
            if (enermyAI == null)
            {
                Debug.LogError("EnermyAI script is not found in the scene.");
            }
        }
        UpdateHealth();
    }

  public void Update()
    {
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        healthBarFill.fillAmount = (float)enermyAI.currentHealth / enermyAI.maxHealth;
    }

    public void SetColor(Color color)
    {
        healthBarFill.color = color;
    }
}
