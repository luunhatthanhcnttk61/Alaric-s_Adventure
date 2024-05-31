using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillCooldownUIManager : MonoBehaviour
{
    public Text skill1CooldownText;
    public Text skill2CooldownText;
    public Text skill3CooldownText;
    public Text skill4CooldownText;

    private void Start()
    {
        skill1CooldownText.text = "";
        skill2CooldownText.text = "";
        skill3CooldownText.text = "";
        skill4CooldownText.text = "";
    }

    public IEnumerator UpdateCooldownUI(float cooldownTime, Text cooldownText)
    {
        float remainingTime = cooldownTime;
        while (remainingTime > 0)
        {
            cooldownText.text = Mathf.Ceil(remainingTime).ToString();
            yield return new WaitForSeconds(1f); // Chờ 1 giây
            remainingTime -= 1f;
        }
        cooldownText.text = ""; // Khi hết thời gian, xóa text
    }
}
