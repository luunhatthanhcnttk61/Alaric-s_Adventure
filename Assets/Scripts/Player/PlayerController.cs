using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f; 
    public float jumpForce = 5f; 
    public int maxHealth = 1000; 
    public int maxMana = 300; 

    private int currentHealth; 
    private int currentMana; 

    public Rigidbody rb;
    private bool isGrounded;
    private bool facingRight = true;

    public HealthBar healthBar;
    public ManaBar manaBar;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        currentMana = maxMana;

        healthBar.UpdateHealth(currentHealth, maxHealth);
        manaBar.UpdateMana(currentMana, maxMana);
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized * moveSpeed;
        rb.velocity = movement;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (moveHorizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void CreateSpell(int manaCost)
    {
        if (currentMana >= manaCost)
        {
            currentMana -= manaCost;
            manaBar.UpdateMana(currentMana, maxMana);
        }
        else
        {
            Debug.Log("Not enough mana!");
        }
    }

    public void RestoreMana(int amount)
    {
        currentMana += amount;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
        manaBar.UpdateMana(currentMana, maxMana);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    void Die()
    {
        
    }
}
