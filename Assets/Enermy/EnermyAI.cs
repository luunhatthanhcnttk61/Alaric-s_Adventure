using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnermyAI : MonoBehaviour
{
    public Transform player; // Tham chiếu đến Transform của player
    public float attackRange = 2f; // Phạm vi tấn công
    public float chaseRange = 10f; // Phạm vi phát hiện player
    public float wanderRange = 20f; // Phạm vi lang thang
    public float wanderTimer = 5f; // Thời gian giữa các lần chọn điểm lang thang mới
    public float attackCooldown = 1.5f; // Thời gian hồi chiêu giữa các lần tấn công
    public float wanderSpeed = 1f; // Tốc độ di chuyển khi lang thang
    public float chaseSpeed = 2f; // Tốc độ di chuyển khi đuổi theo người chơi
    public int maxHealth = 300; // Máu tối đa của Enemy Bot
    public Collider handCollider; // Collider của tay
    public Collider bodyCollider; // Collider của object enemy
    public HandAttack handAttack; // Tham chiếu đến script HandAttack

    public int currentHealth; // Máu hiện tại của Enemy Bot
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isAttacking = false;
    private bool isDead = false;
    private bool canAttack = false;
    private bool hasAttacked = false; // Cờ để theo dõi tấn công
    private Vector3 wanderTarget; // Mục tiêu lang thang
    private float wanderTimerCounter; // Bộ đếm thời gian cho lang thang

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; // Khởi tạo máu hiện tại bằng máu tối đa
        handCollider.enabled = false; // Tắt collider của tay khi bắt đầu
        wanderTimerCounter = wanderTimer;

        if (handAttack == null)
        {
            Debug.LogError("HandAttack script is not assigned in the Inspector.");
        }

        SetNewWanderTarget(); // Chọn điểm lang thang đầu tiên
    }

    void Update()
    {
        if (isDead) 
            Die();

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= attackRange)
        {
            navMeshAgent.isStopped = true;
            if (!isAttacking)
            {
                StartCoroutine(AttackPlayer());
            }
            //navMeshAgent.isStopped = false;
        }
        else if (distanceToPlayer <= chaseRange)
        {
            ChasePlayer();
        }
        else
        {
            Wander(); // Gọi hàm lang thang khi player không trong vùng phát hiện
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
        if(currentHealth > 0 && currentHealth <= maxHealth)
        {
            currentHealth -= damage;
            animator.SetTrigger("TakeDamage"); // Kích hoạt animation nhận sát thương
        }
        if (currentHealth < 0)
        {
            Die();
        }

    }

    public void Die()
    {
        navMeshAgent.isStopped = true; // Dừng di chuyển khi chết
        Debug.Log("Animation die of bot");
        animator.SetTrigger("Die"); // Kích hoạt animation chết
        Debug.Log("Da kich hoat animation die");
        
        Destroy(gameObject, 4f); 
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

    void SetNewWanderTarget()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRange;
        randomDirection += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, wanderRange, -1);
        wanderTarget = navHit.position;
    }

    void Wander()
    {
        navMeshAgent.speed = wanderSpeed;
        if (wanderTimerCounter <= 0)
        {
            SetNewWanderTarget();
            wanderTimerCounter = wanderTimer;
        }
        else
        {
            wanderTimerCounter -= Time.deltaTime;
        }

        navMeshAgent.SetDestination(wanderTarget);
        animator.SetBool("isWalking", true);

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            SetNewWanderTarget(); 
        }
    }

    void ChasePlayer()
    {
        navMeshAgent.speed = chaseSpeed; 
        navMeshAgent.SetDestination(player.position);
        animator.SetBool("isWalking", true); 
        SmoothLookAt(player); 
    }

    void SmoothLookAt(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // 5f là tốc độ quay, bạn có thể điều chỉnh giá trị này
    }
}
