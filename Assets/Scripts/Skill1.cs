using UnityEngine;

public class Skill1 : MonoBehaviour
{
    public Sword sword; // Tham chiếu đến script Sword
    public int manaCost = 10; // Mana tiêu hao khi sử dụng kỹ năng 1

    // Hàm này được gọi khi kỹ năng 1 được sử dụng
    public void UseSkill1()
    {
        if (sword != null)
        {
            // Giảm mana
            sword.ReduceMana(manaCost);
            // Gọi phương thức UpdateSwordDamage trên Sword để cập nhật giá trị sát thương mới
            sword.BasicAttack(); // Tham số là giá trị sát thương mới
        }
    }
}

