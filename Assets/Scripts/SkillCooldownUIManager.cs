using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillCooldownUIManager : MonoBehaviour
{
    public SkillManager skillManager; // Tham chiếu đến SkillManager

    public Image skill1Icon;
    public Text skill1CooldownText;

    public Image skill2Icon;
    public Text skill2CooldownText;

    public Image skill3Icon;
    public Text skill3CooldownText;

    public Image skill4Icon;
    public Text skill4CooldownText;

    private void Start()
    {
        skill1CooldownText.text = "";
        skill2CooldownText.text = "";
        skill3CooldownText.text = "";
        skill4CooldownText.text = "";
    }

    private void Update()
    {
        UpdateCooldownUI(skillManager.skill1OnCooldown, skillManager.skill1CooldownTime, skill1CooldownText);
        UpdateCooldownUI(skillManager.skill2OnCooldown, skillManager.skill2CooldownTime, skill2CooldownText);
        UpdateCooldownUI(skillManager.skill3OnCooldown, skillManager.skill3CooldownTime, skill3CooldownText);
        UpdateCooldownUI(skillManager.skill4OnCooldown, skillManager.skill4CooldownTime, skill4CooldownText);
    }

    private void UpdateCooldownUI(bool isOnCooldown, float cooldownTime, Text cooldownText)
    {
        if (isOnCooldown)
        {
            cooldownTime -= Time.deltaTime;
            cooldownText.text = Mathf.Ceil(cooldownTime).ToString();
        }
        else
        {
            cooldownText.text = "";
        }
    }
}
