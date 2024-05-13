// using UnityEngine;

// public class SkillManager : MonoBehaviour
// {
//     public GameObject player; // Tham chiếu đến GameObject của player
//     public Animator playerAnimator; // Tham chiếu đến Animator của player
//     public Sword sword; // Tham chiếu đến script Sword
//     public Skill1 skill1; // Tham chiếu đến script Skill1
//     public Skill2 skill2; // Tham chiếu đến script Skill2

//     private AnimationEndHandler animationEndHandler; // Tham chiếu đến script xử lý sự kiện hoàn thành animation

//     void Start()
//     {
//         // Tạo một thể hiện của script xử lý sự kiện hoàn thành animation và gắn vào người chơi
//         animationEndHandler = player.AddComponent<AnimationEndHandler>();
//     }

//     void Update()
//     {
//         // Kiểm tra input từ người chơi và kích hoạt animation của skill 1 khi nhấn phím 1
//         if (Input.GetKeyDown(KeyCode.Alpha1))
//         {
//             playerAnimator.SetTrigger("Skill1");
//             skill1.UseSkill1();
//         }
//         // Kiểm tra input từ người chơi và kích hoạt animation của skill 2 khi nhấn phím 2
//         if (Input.GetKeyDown(KeyCode.Alpha2))
//         {
//             playerAnimator.SetTrigger("Skill2");
//             skill2.UseSkill2();
//         }
//     }
// }
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public GameObject player; // Tham chiếu đến GameObject của player
    public Animator playerAnimator; // Tham chiếu đến Animator của player
    public Sword sword; // Tham chiếu đến script Sword
    public Skill1 skill1; // Tham chiếu đến script Skill1
    public Skill2 skill2; // Tham chiếu đến script Skill2

    private bool isAnimating = false; // Biến để kiểm tra xem animation đang chạy hay không

    void Update()
    {
        // Kiểm tra input từ người chơi và kích hoạt animation của skill 1 khi nhấn phím 1
        if (Input.GetKeyDown(KeyCode.Alpha1) && !isAnimating)
        {
            playerAnimator.SetTrigger("Skill1");
            skill1.UseSkill1();
            isAnimating = true; // Đặt biến isAnimating thành true khi bắt đầu animation
        }
        // Kiểm tra input từ người chơi và kích hoạt animation của skill 2 khi nhấn phím 2
        if (Input.GetKeyDown(KeyCode.Alpha2) && !isAnimating)
        {
            playerAnimator.SetTrigger("Skill2");
            skill2.UseSkill2();
            isAnimating = true; // Đặt biến isAnimating thành true khi bắt đầu animation
        }
    }

    // Hàm được gọi từ script xử lý sự kiện hoàn thành animation
    public void OnAnimationEnd()
    {
        isAnimating = false; // Đặt biến isAnimating thành false khi animation kết thúc
    }
}

