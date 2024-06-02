using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnermyAI : MonoBehaviour
{
    public Transform player; 
    public float attackRange = 2f; 
    public float chaseRange = 10f;
    public float wanderRange = 20f;
    public float wanderTimer = 5f;
    public float attackCooldown = 1.5f;
    public float wanderSpeed = 1f;
    public float chaseSpeed = 2f;
    public int maxHealth = 300;
    public Collider handCollider;
    public Collider bodyCollider;
    public HandAttack handAttack; 

    public int currentHealth; 
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isAttacking = false;
    private bool isDead = false;
    private bool canAttack = false;
    private bool hasAttacked = false;
    private Vector3 wanderTarget; 
    private float wanderTimerCounter;
    private BotAudioManager botAudioManager;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; 
        handCollider.enabled = false; 
        wanderTimerCounter = wanderTimer;
        botAudioManager = GetComponent<BotAudioManager>();

        if (handAttack == null)
        {
            Debug.LogError("HandAttack script is not assigned in the Inspector.");
        }

        SetNewWanderTarget(); 
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
            other.GetComponent<PlayerController2>().TakeDamage(10, transform.forward);
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
            animator.SetTrigger("TakeDamage"); 
            botAudioManager?.PlayTakeDamageSound(0f, 0.5f); 
        }
        if (currentHealth <= 0)
        {
            Die();
            Destroy(gameObject, 4f); 
        }
    }

    public void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        navMeshAgent.isStopped = true;
        bodyCollider.enabled = false;
        botAudioManager?.PlayDieSound(0f, 1f);
        Debug.Log("Da kich hoat animation die");
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
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); 
    }
}
