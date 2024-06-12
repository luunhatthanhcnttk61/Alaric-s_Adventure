using System.Collections;
using UnityEngine;
using System;

[Serializable]
public class PlayerController2 : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public CharacterController controller;

    private Vector3 moveDirection;
    public float gravityScale;

    public Animator anim;

    public Transform pivot;
    public float rotateSpeed;
    public GameObject playerModel;
    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;

    private bool canMove = true;

    public int maxHealth = 500;
    public int currentHealth;

    public float maxMana = 300f;
    public float currentMana;
    private float manaRegenTimer = 0f;
    public int manaRegenRate = 5;

    // Armour
    public int maxArmour = 200;
    public int currentArmour;

    private ArmourManager armourBarManager;
    public int attackPower;

    public void AddAttackPower(int powerToAdd)
    {
        attackPower += powerToAdd;
        Debug.Log("Added attack power: " + powerToAdd);
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth;
        currentMana = maxMana;
        currentArmour = 0; 
        armourBarManager = FindObjectOfType<ArmourManager>();

        if (armourBarManager != null)
        {
            armourBarManager.UpdateArmour(currentArmour);
        }
    }

    void Update()
    {
        if (canMove)
        {
            if (knockBackCounter <= 0)
            {
                float yStore = moveDirection.y;
                moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
                moveDirection = moveDirection.normalized * moveSpeed;
                moveDirection.y = yStore;

                if (controller.isGrounded)
                {
                    moveDirection.y = 0f;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        moveDirection.y = jumpForce;
                    }
                }
            }
            else
            {
                knockBackCounter -= Time.deltaTime;
            }

            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
            controller.Move(moveDirection * Time.deltaTime);

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }

            anim.SetBool("isGrounded", controller.isGrounded);
            anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
        }

        manaRegenTimer -= Time.deltaTime;
        if (manaRegenTimer <= 0)
        {
            if (currentMana < maxMana)
            {
                currentMana += manaRegenRate;
                currentMana = Mathf.Clamp(currentMana, 0, maxMana);
                FindObjectOfType<ManaBarManager>().UpdateMana();
            }
            manaRegenTimer = 1f;
        }
        FindObjectOfType<ManaBarManager>().UpdateMana();
    }

    public void KnockBack(Vector3 direction)
    {
        knockBackCounter = knockBackTime;
        moveDirection = direction * knockBackForce;
        moveDirection.y = knockBackForce;
    }

    public bool UseMana(float amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
            return true;
        }
        return false;
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        FindObjectOfType<HealthBarManager>().UpdateHealth();
    }

    public void RegenerateMana(float amount)
    {
        currentMana += amount;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana);
        FindObjectOfType<ManaBarManager>().UpdateMana();
    }

    public void TakeDamage(int damage, Vector3 direction)
    {
        if (currentArmour > 0)
        {
            int remainingDamage = damage - currentArmour;
            currentArmour -= damage;
            currentArmour = Mathf.Clamp(currentArmour, 0, maxArmour);
            armourBarManager.UpdateArmour(currentArmour);

            if (remainingDamage > 0)
            {
                currentHealth -= remainingDamage;
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
                FindObjectOfType<HealthBarManager>().UpdateHealth();
            }
        }
        else
        {
            currentHealth -= damage;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            FindObjectOfType<HealthBarManager>().UpdateHealth();
        }

        anim.SetTrigger("TakeDamage");

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(TakeDamageRoutine());
        }
    }

    private IEnumerator TakeDamageRoutine()
    {
        SetCanMove(false);
        anim.SetTrigger("TakeDamage");
        yield return new WaitForSeconds(0.5f);
        SetCanMove(true);
    }

    void Die()
    {
        canMove = false;
        anim.SetTrigger("Die");
        Debug.Log("Player has died!");

        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }
        if (controller != null)
        {
            controller.enabled = false;
        }

        pivot.gameObject.SetActive(false);
        this.enabled = false;
    }

    public void AddArmour(int amount)
    {
        currentArmour += amount;
        currentArmour = Mathf.Clamp(currentArmour, 0, maxArmour);
        armourBarManager.UpdateArmour(currentArmour);
    }
}
