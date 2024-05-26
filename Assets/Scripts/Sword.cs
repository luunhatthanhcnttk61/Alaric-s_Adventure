using UnityEngine;
using System.Collections; 

public class Sword : MonoBehaviour
{
    public float basicAttackCooldown = 1f; // Thời gian hồi chiêu cho chém thường
    public float comboAttackCooldown = 3f; // Thời gian hồi chiêu cho liên hoàn chém
    public float magicCooldown = 4f; // Thời gian hồi chiêu cho cầu phép
    public float areaDamageCooldown = 7f; // Thời gian hồi chiêu cho vùng sát thương

    public int basicAttackDamage = 30; // Sát thương của chém thường
    public int comboAttackDamage = 70; // Sát thương của liên hoàn chém
    public int magicDamage = 90; // Sát thương của cầu phép
    public int areaDamage = 150; // Sát thương của vùng sát thương

    public int basicAttackManaCost = 10; // Mana tiêu hao của chém thường
    public int comboAttackManaCost = 30; // Mana tiêu hao của liên hoàn chém
    public int magicManaCost = 40; // Mana tiêu hao của cầu phép
    public int areaDamageManaCost = 70; // Mana tiêu hao của vùng sát thương

    private float basicAttackTimer = 0f;
    private float comboAttackTimer = 0f;
    private float magicTimer = 0f;
    private float areaDamageTimer = 0f;
    private int swordDamage = 0;
    public int currentMana = 300; // Mana hiện tại
    public int maxMana = 300; // Mana tối đa
    private float manaRegenTimer = 0f;
    public int manaRegenRate = 5; // Tốc độ hồi mana mỗi giây

    public Collider swordCollider; // Thêm tham chiếu đến Collider của sword

    void Start()
    {
        swordCollider.enabled = false; // Đảm bảo sword collider bị tắt khi bắt đầu
    }

    void Update()
    {
        // Cập nhật thời gian hồi chiêu cho mỗi kỹ năng
        basicAttackTimer -= Time.deltaTime;
        comboAttackTimer -= Time.deltaTime;
        magicTimer -= Time.deltaTime;
        areaDamageTimer -= Time.deltaTime;
        
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

    public void BasicAttack()
    {
        if (basicAttackTimer <= 0 && currentMana >= basicAttackManaCost)
        {
            ReduceMana(basicAttackManaCost);
            UpdateSwordDamage(basicAttackDamage);
            // Thực hiện chém thường
            Debug.Log("Basic Attack! Damage: " + swordDamage);
            
            StartCoroutine(EnableSwordColliderTemporarily(1f));

            // Gây sát thương cho mục tiêu
            //DealDamageToTarget(basicAttackDamage);

            // Reset hồi chiêu và đặt lại thời gian hồi chiêu
            basicAttackTimer = basicAttackCooldown;
        }
    }

    public void ComboAttack()
    {
        if (comboAttackTimer <= 0 && currentMana >= comboAttackManaCost)
        {
            ReduceMana(comboAttackManaCost);
            UpdateSwordDamage(comboAttackDamage);
            Debug.Log("Combo Attack! Damage: " + comboAttackDamage);

            StartCoroutine(EnableSwordColliderTemporarily(2.5f));

            //DealDamageToTarget(comboAttackDamage);
            comboAttackTimer = comboAttackCooldown;
        }
    }

    public void Magic()
    {
        if (magicTimer <= 0 && currentMana >= magicManaCost)
        {
            ReduceMana(magicManaCost);
            UpdateSwordDamage(magicDamage);
            Debug.Log("Magic! Damage: " + magicDamage);

            StartCoroutine(EnableSwordColliderTemporarily(1f));

            //DealDamageToTarget(magicDamage);
            magicTimer = magicCooldown;
        }
    }

    public void AreaDamage()
    {
        if (areaDamageTimer <= 0 && currentMana >= areaDamageManaCost)
        {
            ReduceMana(areaDamageManaCost);
            UpdateSwordDamage(areaDamage);
            Debug.Log("Area Damage! Damage: " + areaDamage);

            StartCoroutine(EnableSwordColliderTemporarily(3f));

            //DealDamageToTarget(areaDamage);
            areaDamageTimer = areaDamageCooldown;
        }
    }

    public void UpdateSwordDamage(int newDamage)
    {
        swordDamage = newDamage;
    }

    // private void DealDamageToTarget(int damage)
    // {
    //     // Lấy tất cả các collider trong một vùng xung quanh thanh kiếm
    //     Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f);
    //     foreach (var hitCollider in hitColliders)
    //     {
    //         // Nếu collider là Bot, gây sát thương
    //         if (hitCollider.CompareTag("Bot"))
    //         {
    //             EnermyAI botHealth = hitCollider.GetComponent<EnermyAI>();
    //             if (botHealth != null)
    //             {
    //                 botHealth.TakeDamage(damage);
    //                 Debug.Log("Bot da bi tan cong!");
    //             }
    //         }
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bot"))
        {
            EnermyAI botHealth = other.GetComponent<EnermyAI>();
            if (botHealth != null)
            {
                botHealth.TakeDamage(swordDamage);
                Debug.Log("Bot da bi tan cong!");
            }
        }
    }

    public void ReduceMana(int amount)
    {
        currentMana -= amount;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana); // Đảm bảo mana không vượt quá giới hạn tối đa và không nhỏ hơn 0
    }

    private IEnumerator EnableSwordColliderTemporarily(float time)
    {
        swordCollider.enabled = true; // Bật collider của sword
        yield return new WaitForSeconds(time); // Thời gian chờ (điều chỉnh thời gian này để phù hợp với thời gian của skill)
        swordCollider.enabled = false; // Tắt collider của sword
    }
}
