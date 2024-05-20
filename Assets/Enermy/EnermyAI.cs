using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnermyAI : MonoBehaviour
{
    public Transform player; // Tham chiếu đến Transform của player
    public float attackRange = 2f; // Phạm vi tấn công
    public float chaseRange = 10f; // Phạm vi phát hiện player
    public float attackCooldown = 1.5f; // Thời gian hồi chiêu giữa các lần tấn công
    public int maxHealth = 300; // Máu tối đa của Enemy Bot
    public Collider handCollider; // Collider của tay
    public Collider bodyCollider; // Collider của object enemy
    public HandAttack handAttack; // Tham chiếu đến script HandAttack

    private int currentHealth; // Máu hiện tại của Enemy Bot
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isAttacking = false;
    private bool isDead = false;
    private bool canAttack = false;
    private bool hasAttacked = false; // Cờ để theo dõi tấn công

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; // Khởi tạo máu hiện tại bằng máu tối đa
        handCollider.enabled = false; // Tắt collider của tay khi bắt đầu

        if (handAttack == null)
        {
            Debug.LogError("HandAttack script is not assigned in the Inspector.");
        }
    }

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= attackRange)
        {
            if (!isAttacking)
            {
                StartCoroutine(AttackPlayer());
            }
        }
        else if (distanceToPlayer <= chaseRange)
        {
            navMeshAgent.SetDestination(player.position);
            animator.SetBool("isWalking", true); // Chuyển sang animation chạy
        }
        else
        {
            // Enemy Bot idle
            navMeshAgent.SetDestination(transform.position);
            animator.SetBool("isWalking", false); // Chuyển sang animation idle
        }
    }

    IEnumerator AttackPlayer()
    {
        isAttacking = true;
        navMeshAgent.isStopped = true; // Dừng di chuyển khi tấn công
        animator.SetTrigger("Attack"); // Kích hoạt animation tấn công
        
        hasAttacked = false; // Reset cờ trước khi bắt đầu tấn công
        if (handAttack != null)
        {
            handAttack.EnableDamage(); // Bật tấn công của tay
        }
        else
        {
            Debug.LogError("HandAttack script is not assigned or not found.");
        }

        Debug.Log("Attacking player!");

        yield return new WaitForSeconds(attackCooldown);

        if (handAttack != null)
        {
            handAttack.DisableDamage(); // Tắt tấn công của tay
        }

        navMeshAgent.isStopped = false; // Tiếp tục di chuyển sau khi tấn công
        isAttacking = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (canAttack && !hasAttacked && other.CompareTag("Player"))
        {   
            //handCollider.enabled = false;
            // Tấn công player
            other.GetComponent<PlayerController2>().TakeDamage(10, transform.forward); // Giả sử mỗi đòn tấn công gây 10 sát thương
            hasAttacked = true; // Đánh dấu đã tấn công
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            hasAttacked = false;
        }
    }


    // Hàm để nhận sát thương từ player
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("Bot bị trừ máu");
        animator.SetTrigger("TakeDamage"); // Kích hoạt animation nhận sát thương

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        navMeshAgent.isStopped = true; // Dừng di chuyển khi chết
        animator.SetTrigger("Die"); // Kích hoạt animation chết
        // Thực hiện các hành động khác khi enemy chết (hủy enemy sau một thời gian)
        Destroy(gameObject, 2f); // Hủy enemy sau 2 giây
    }

    public void EnableAttack()
    {
        canAttack = true;
        handCollider.enabled = true;
    }

    public void DisableAttack()
    {
        canAttack = false;
        handCollider.enabled = false;
    }
}
