using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2f; // Tốc độ di chuyển của player
    public float jumpForce = 5f; // Lực nhảy của player
    public int maxHealth = 1000; // Máu tối đa của player
    public int maxMana = 300; // Mana tối đa của player

    private int currentHealth; // Máu hiện tại của player
    private int currentMana; // Mana hiện tại của player

    private Rigidbody rb;
    private bool isGrounded;
    private bool facingRight = true;

    // Reference đến UI elements hiển thị máu và mana
    public HealthBar healthBar;
    public ManaBar manaBar;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        currentMana = maxMana;

        // Khởi tạo UI bars
        healthBar.UpdateHealth(currentHealth, maxHealth);
        manaBar.UpdateMana(currentMana, maxMana);
    }

    void Update()
    {
        // Di chuyển player
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized * moveSpeed;
        rb.velocity = movement;

        // Nhảy
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Flip hình ảnh player nếu cần
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

    // Phương thức tạo ra chiêu thức với mức độ mana mất đi tùy thuộc vào từng chiêu thức
    public void CreateSpell(int manaCost)
    {
        // Kiểm tra xem có đủ mana không
        if (currentMana >= manaCost)
        {
            // Tạo ra chiêu thức

            // Giảm lượng mana tương ứng
            currentMana -= manaCost;
            manaBar.UpdateMana(currentMana, maxMana);
        }
        else
        {
            // Xử lý khi không đủ mana
            Debug.Log("Not enough mana!");
        }
    }

    // Hàm hồi mana
    public void RestoreMana(int amount)
    {
        currentMana += amount;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
        manaBar.UpdateMana(currentMana, maxMana);
    }

    // Hàm giảm máu của player
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    // Hàm hồi phục máu của player
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
        // Xử lý khi player chết
    }
}
