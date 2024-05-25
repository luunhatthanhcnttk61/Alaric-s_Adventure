using System.Collections;
using UnityEngine;

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

    private bool canMove = true; // Biến để kiểm tra xem player có thể di chuyển không

    // Thuộc tính sức khỏe
    public int maxHealth = 300;
    public int currentHealth;

    // Thêm thuộc tính mana
    public float maxMana = 300f;
    public float currentMana;
    private float manaRegenTimer = 0f;
    public int manaRegenRate = 5; // Tốc độ hồi mana mỗi giây

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentHealth = maxHealth; // Khởi tạo máu đầy đủ khi bắt đầu game
        currentMana = maxMana; // Khởi tạo mana đầy đủ khi bắt đầu game
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

        // Hồi mana mỗi giây
        manaRegenTimer -= Time.deltaTime;
        if (manaRegenTimer <= 0)
        {
            // Kiểm tra xem mana đã đạt đến giá trị tối đa chưa
            if (currentMana < maxMana)
            {
                currentMana += manaRegenRate;
                currentMana = Mathf.Clamp(currentMana, 0, maxMana); // Đảm bảo mana không vượt quá giới hạn tối đa và không nhỏ hơn 0
            }
            manaRegenTimer = 1f; // Đặt lại bộ đếm cho việc hồi mana mỗi giây
        }
    }

    public void KnockBack(Vector3 direction)
    {
        knockBackCounter = knockBackTime;
        moveDirection = direction * knockBackForce;
        moveDirection.y = knockBackForce;
    }

    // Phương thức để trừ mana
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

    // Phương thức để hồi mana (tùy chọn)
    public void RegenerateMana(float amount)
    {
        currentMana = Mathf.Min(currentMana + amount, maxMana);
    }

    // Phương thức để nhận sát thương từ enemy
    public void TakeDamage(int damage, Vector3 direction)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        FindObjectOfType<PlayerUIManager>().UpdateHealthBar();
        anim.SetTrigger("TakeDamage");

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(TakeDamageRoutine());
            KnockBack(direction);
        }
    }

    private IEnumerator TakeDamageRoutine()
    {
        // Tạm thời dừng di chuyển
        SetCanMove(false);
        anim.SetTrigger("TakeDamage");

        // Thời gian chờ cho animation nhận sát thương (điều chỉnh thời gian này để phù hợp với animation của bạn)
        yield return new WaitForSeconds(0.5f);

        // Cho phép di chuyển lại
        SetCanMove(true);
    }

    void Die()
    {
        canMove = false;
        anim.SetTrigger("Die");
        Debug.Log("Player has died!");

        // Tắt tất cả các Collider của Player
        Collider[] colliders = GetComponents<Collider>();
        foreach (Collider col in colliders)
        {
            col.enabled = false;
        }

        // Tắt CharacterController
        if (controller != null)
        {
            controller.enabled = false;
        }

        // Ngừng pivot rotation
        pivot.gameObject.SetActive(false);

        // Vô hiệu hóa script này
        this.enabled = false;

        // Thực hiện các hành động khi player chết (hiển thị màn hình game over)
    }
}
