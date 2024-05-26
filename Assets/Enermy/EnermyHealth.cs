using UnityEngine;
using UnityEngine.UI;

public class EnermyHealth : MonoBehaviour
{
    public EnermyAI enermyAI;
    public Image healthBarFill; // Tham chiếu đến Image của HealthBarFill

    void Start()
    {
        UpdateHealth();
    }

  public void Update()
    {
        
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
