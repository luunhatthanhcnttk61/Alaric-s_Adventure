using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public Sword sword; // Thanh kiếm để gắn các kỹ năng vào

    void Update()
    {
        // Kiểm tra input của người chơi để kích hoạt các kỹ năng
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            sword.BasicAttack();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            sword.ComboAttack();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            sword.Magic();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            sword.AreaDamage();
        }
    }
}
