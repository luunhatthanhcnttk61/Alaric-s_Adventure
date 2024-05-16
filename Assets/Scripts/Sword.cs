using UnityEngine;

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

    public void ReduceMana(int amount)
    {
        currentMana -= amount;
        currentMana = Mathf.Clamp(currentMana, 0, maxMana); // Đảm bảo mana không vượt quá giới hạn tối đa và không nhỏ hơn 0
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
        if (basicAttackTimer <= 0)
        {
            UpdateSwordDamage(basicAttackDamage);
            // Thực hiện chém thường
            Debug.Log("Basic Attack! Damage: " + swordDamage);

            // Gây sát thương cho mục tiêu
            //DealDamageToTarget();

            // Reset hồi chiêu và đặt lại thời gian hồi chiêu
            basicAttackTimer = basicAttackCooldown;
        }
    }

    // Hàm này thực hiện việc gây sát thương cho mục tiêu
    // private void DealDamageToTarget()
    // {
    //     // Giả sử bạn có một đối tượng mục tiêu với script "Target" có phương thức "TakeDamage"
    //     // Lấy đối tượng mục tiêu (ví dụ từ raycast, collider, etc.)
    //     Target target = GetTarget(); // Bạn cần thực hiện phương thức GetTarget() để lấy mục tiêu thực tế

    //     if (target != null)
    //     {
    //         target.TakeDamage(swordDamage);
    //     }
    // }

    // // Giả định phương thức này trả về đối tượng mục tiêu
    // private Target GetTarget()
    // {
    //     // Tìm kiếm đối tượng mục tiêu trong tầm tấn công
    //     // Điều này có thể dựa trên raycast, collider hoặc bất kỳ phương pháp nào bạn sử dụng để xác định mục tiêu
    //     return null; // Thay thế bằng logic lấy mục tiêu thực tế
    // }

    public void ComboAttack()
    {
        if (comboAttackTimer <= 0)
        {
            UpdateSwordDamage(comboAttackDamage);
            Debug.Log("Combo Attack! Damage: " + comboAttackDamage);
            comboAttackTimer = comboAttackCooldown;
        }
    }

    public void Magic()
    {
        if (magicTimer <= 0)
        {
            UpdateSwordDamage(magicDamage);
            Debug.Log("Magic! Damage: " + magicDamage);
            magicTimer = magicCooldown;
        }
    }

    public void AreaDamage()
    {
        if (areaDamageTimer <= 0)
        {
            UpdateSwordDamage(areaDamage);
            Debug.Log("Area Damage! Damage: " + areaDamage);
            areaDamageTimer = areaDamageCooldown;
        }
    }

    public void UpdateSwordDamage(int newDamage)
    {
        swordDamage = newDamage;
    }
}
