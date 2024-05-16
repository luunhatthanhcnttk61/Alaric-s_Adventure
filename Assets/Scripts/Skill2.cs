using UnityEngine;

public class Skill2 : MonoBehaviour
{
    public Sword sword; // Tham chiếu đến script Sword
    public int manaCost = 30;

    // Hàm này được gọi khi kỹ năng 1 được sử dụng
    public void UseSkill2()
    {
        if (sword != null)
        {
            sword.ReduceMana(manaCost);
            // Gọi phương thức UpdateSwordDamage trên Sword để cập nhật giá trị sát thương mới
            sword.ComboAttack(); // Tham số là giá trị sát thương mới
        }
    }
}
