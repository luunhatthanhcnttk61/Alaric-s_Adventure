using UnityEngine;

public class Skill3 : MonoBehaviour
{
    public Sword sword; // Tham chiếu đến script Sword

    // Hàm này được gọi khi kỹ năng 1 được sử dụng
    public void UseSkill3()
    {
        if (sword != null)
        {
            // Gọi phương thức UpdateSwordDamage trên Sword để cập nhật giá trị sát thương mới
            sword.Magic(); // Tham số là giá trị sát thương mới
        }
    }
}
