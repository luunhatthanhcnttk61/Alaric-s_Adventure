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

    void Update()
    {
        // Cập nhật thời gian hồi chiêu cho mỗi kỹ năng
        basicAttackTimer -= Time.deltaTime;
        comboAttackTimer -= Time.deltaTime;
        magicTimer -= Time.deltaTime;
        areaDamageTimer -= Time.deltaTime;
    }

    public void BasicAttack()
    {
        if (basicAttackTimer <= 0)
        {
            // Thực hiện chém thường
            // Ví dụ: Gây sát thương cho mục tiêu
            Debug.Log("Basic Attack!");
            
            // Reset hồi chiêu và đặt lại thời gian hồi chiêu
            basicAttackTimer = basicAttackCooldown;
        }
    }

    public void ComboAttack()
    {
        if (comboAttackTimer <= 0)
        {
            // Thực hiện liên hoàn chém
            // Ví dụ: Gây sát thương cho mục tiêu
            Debug.Log("Combo Attack!");
            
            // Reset hồi chiêu và đặt lại thời gian hồi chiêu
            comboAttackTimer = comboAttackCooldown;
        }
    }

    public void Magic()
    {
        if (magicTimer <= 0)
        {
            // Thực hiện cầu phép
            // Ví dụ: Gây sát thương cho mục tiêu
            Debug.Log("Magic!");
            
            // Reset hồi chiêu và đặt lại thời gian hồi chiêu
            magicTimer = magicCooldown;
        }
    }

    public void AreaDamage()
    {
        if (areaDamageTimer <= 0)
        {
            // Thực hiện vùng sát thương
            // Ví dụ: Gây sát thương cho mục tiêu
            Debug.Log("Area Damage!");
            
            // Reset hồi chiêu và đặt lại thời gian hồi chiêu
            areaDamageTimer = areaDamageCooldown;
        }
    }
    public void UpdateSwordDamage(int newDamage)
{
    // Cập nhật giá trị sát thương của thanh kiếm
    // Ví dụ: gán giá trị mới cho basicAttackDamage
    basicAttackDamage = newDamage;
}

}
