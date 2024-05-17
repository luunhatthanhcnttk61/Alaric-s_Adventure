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

    private int currentHealth; // Máu hiện tại của Enemy Bot
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isAttacking = false;
    private bool isDead = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; // Khởi tạo máu hiện tại bằng máu tối đa
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

        // Gọi phương thức tấn công (implement animation và logic tấn công ở đây)
        Debug.Log("Attacking player!");

        yield return new WaitForSeconds(attackCooldown);

        navMeshAgent.isStopped = false; // Tiếp tục di chuyển sau khi tấn công
        isAttacking = false;
    }

    // Hàm để nhận sát thương từ player
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
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
        // Thực hiện các hành động khác khi enemy chết (ví dụ: hủy enemy sau một thời gian)
        Destroy(gameObject, 2f); // Hủy enemy sau 2 giây
    }
}
