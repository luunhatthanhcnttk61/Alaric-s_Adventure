using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] Image m_fillBar;
    [SerializeField] TMP_Text m_status;

    public void UpdateMana(int current, int baseMana)
    {
        m_fillBar.fillAmount = (float)current / baseMana;
    }

    public void SetColor(Color color)
    {
        m_fillBar.color = color;
    }

    public void SetStatus(string status)
    {
        m_status.text = status;
    }
}