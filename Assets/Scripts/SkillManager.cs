using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public GameObject player; 
    public Animator playerAnimator;
    public PlayerController2 playerMovement;
    public Sword sword;
    public Skill1 skill1; 
    public Skill2 skill2;
    public Skill3 skill3;
    public Skill4 skill4;

    public SkillCooldownUIManager skillCooldownUIManager; 

    private bool isAnimating = false;

    public bool skill1OnCooldown = false;
    public bool skill2OnCooldown = false; 
    public bool skill3OnCooldown = false;
    public bool skill4OnCooldown = false; 

    public float skill1CooldownTime = 3f; 
    public float skill2CooldownTime = 5f; 
    public float skill3CooldownTime = 7f; 
    public float skill4CooldownTime = 9f; 

    public float skill1ManaCost = 10f;
    public float skill2ManaCost = 30f;
    public float skill3ManaCost = 40f;
    public float skill4ManaCost = 70f;

    public Text skill1CooldownText; 
    public Text skill2CooldownText; 
    public Text skill3CooldownText; 
    public Text skill4CooldownText; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !isAnimating && !skill1OnCooldown)
        {
            Debug.Log("Da nhan phim 1");
            if (playerMovement.UseMana(skill1ManaCost))
            {
                playerAnimator.SetTrigger("Skill1");
                skill1.UseSkill1();
                StartCoroutine(SkillCooldown(skill1CooldownTime, 1)); 
                isAnimating = true; 
                playerMovement.SetCanMove(false); 
            }
            else
            {
                Debug.Log("Not enough mana for Skill 1");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && !isAnimating && !skill2OnCooldown)
        {
            if (playerMovement.UseMana(skill2ManaCost))
            {
                playerAnimator.SetTrigger("Skill2");
                skill2.UseSkill2();
                StartCoroutine(SkillCooldown(skill2CooldownTime, 2)); 
                isAnimating = true; 
                playerMovement.SetCanMove(false); 
            }
            else
            {
                Debug.Log("Not enough mana for Skill 2");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && !isAnimating && !skill3OnCooldown)
        {
            if (playerMovement.UseMana(skill3ManaCost))
            {
                playerAnimator.SetTrigger("Skill3");
                skill3.UseSkill3();
                StartCoroutine(SkillCooldown(skill3CooldownTime, 3)); 
                isAnimating = true;
                playerMovement.SetCanMove(false); 
            }
            else
            {
                Debug.Log("Not enough mana for Skill 3");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && !isAnimating && !skill4OnCooldown)
        {
            if (playerMovement.UseMana(skill4ManaCost))
            {
                playerAnimator.SetTrigger("Skill4");
                skill4.UseSkill4();
                StartCoroutine(SkillCooldown(skill4CooldownTime, 4)); 
                isAnimating = true; 
                playerMovement.SetCanMove(false); 
            }
            else
            {
                Debug.Log("Not enough mana for Skill 4");
            }
        }
    }

    IEnumerator SkillCooldown(float cooldownTime, int skillNumber)
    {
        switch(skillNumber)
        {
            case 1:
                skill1OnCooldown = true;
                StartCoroutine(skillCooldownUIManager.UpdateCooldownUI(skill1CooldownTime, skill1CooldownText)); 
                yield return new WaitForSeconds(cooldownTime);
                skill1OnCooldown = false;
                break;
            case 2:
                skill2OnCooldown = true;
                StartCoroutine(skillCooldownUIManager.UpdateCooldownUI(skill2CooldownTime, skill2CooldownText)); 
                yield return new WaitForSeconds(cooldownTime);
                skill2OnCooldown = false;
                break;
            case 3:
                skill3OnCooldown = true;
                StartCoroutine(skillCooldownUIManager.UpdateCooldownUI(skill3CooldownTime, skill3CooldownText)); 
                yield return new WaitForSeconds(cooldownTime);
                skill3OnCooldown = false;
                break;
            case 4:
                skill4OnCooldown = true;
                StartCoroutine(skillCooldownUIManager.UpdateCooldownUI(skill4CooldownTime, skill4CooldownText));
                yield return new WaitForSeconds(cooldownTime);
                skill4OnCooldown = false;
                break;
        }
    }

    public void OnAnimationEnd()
    {
        isAnimating = false; 
        Debug.Log("Ket thuc animation");
        playerMovement.SetCanMove(true); 
        Debug.Log("Player co the chuyen dong");
    }
}
