using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnermyAI : MonoBehaviour
{
    public Transform player; // Tham chiếu đến Transform của player
    public float attackRange = 4f; // Phạm vi tấn công
    public float chaseRange = 10f; // Phạm vi phát hiện player
    public float attackCooldown = 1.5f; // Thời gian hồi chiêu giữa các lần tấn công
    public int attackDamage = 30; // Sát thương của mỗi đòn tấn công
    public int maxHealth = 300; // Máu tối đa của Enemy Bot

    private int currentHealth; // Máu hiện tại của Enemy Bot
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isAttacking = false;
    private bool isDead = false;
    private PlayerController2 playerController; // Tham chiếu đến script của player
    public float attackDistance = 1.5f; // Khoảng cách tấn công mong muốn
    public Collider attackCollider; // Tham chiếu đến Collider của vũ khí/tay tấn công

    // Phương thức để bật Collider tấn công
    public void EnableAttackCollider()
    {
        attackCollider.enabled = true;
    }

    // Phương thức để tắt Collider tấn công
    public void DisableAttackCollider()
    {
        attackCollider.enabled = false;
    }


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; // Khởi tạo máu hiện tại bằng máu tối đa
        playerController = player.GetComponent<PlayerController2>(); // Lấy tham chiếu đến PlayerController2
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
            // Điều chỉnh khoảng cách giữa bot và người chơi
            if (distanceToPlayer > attackDistance)
            {
                navMeshAgent.SetDestination(player.position);
            }
            else
            {
                // Đứng yên nếu đã ở gần người chơi
                navMeshAgent.SetDestination(transform.position);
            }

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

        // Đợi đến khi animation tấn công chạm mục tiêu (có thể dùng animation event)
        yield return new WaitForSeconds(0.5f);

        // Gây sát thương cho player
        if (playerController != null)
        {
            playerController.TakeDamage(attackDamage);
            Debug.Log("Attacking player!");
        }

        yield return new WaitForSeconds(attackCooldown - 0.5f); // Thời gian hồi chiêu trừ đi thời gian chờ trước đó

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
        Destroy(gameObject, 2f); // Hủy enemy sau 2 giây
    }
}
