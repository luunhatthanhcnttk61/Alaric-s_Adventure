// using UnityEngine;

// public class SkillManager : MonoBehaviour
// {
//     public GameObject player; // Tham chiếu đến GameObject của player
//     public Animator playerAnimator; // Tham chiếu đến Animator của player
//     public Sword sword; // Tham chiếu đến script Sword
//     public Skill1 skill1; // Tham chiếu đến script Skill1
//     public Skill2 skill2; // Tham chiếu đến script Skill2

//     private bool isAnimating = false; // Biến để kiểm tra xem animation đang chạy hay không

//     void Update()
//     {
//         // Kiểm tra input từ người chơi và kích hoạt animation của skill 1 khi nhấn phím 1
//         if (Input.GetKeyDown(KeyCode.Alpha1) && !isAnimating)
//         {
//             playerAnimator.SetTrigger("Skill1");
//             skill1.UseSkill1();
//             Debug.Log("Da nhan phim 1");
//             isAnimating = true; // Đặt biến isAnimating thành true khi bắt đầu animation
//             Debug.Log("Animation dang hoat dong");
//         }
//         // Kiểm tra input từ người chơi và kích hoạt animation của skill 2 khi nhấn phím 2
//         if (Input.GetKeyDown(KeyCode.Alpha2) && !isAnimating)
//         {
//             playerAnimator.SetTrigger("Skill2");
//             skill2.UseSkill2();
//             Debug.Log("Da nhan phim 2");
//             isAnimating = true; // Đặt biến isAnimating thành true khi bắt đầu animation
//             Debug.Log("Animation dang hoat dong");
//         }
//     }

//     // Hàm được gọi từ script xử lý sự kiện hoàn thành animation
//     public void OnAnimationEnd()
//     {
//         isAnimating = false; // Đặt biến isAnimating thành false khi animation kết thúc
//         Debug.Log("Animation ket thuc");
//     }
// }

using UnityEngine;
using System.Collections;

public class SkillManager : MonoBehaviour
{
    public GameObject player; // Tham chiếu đến GameObject của player
    public Animator playerAnimator; // Tham chiếu đến Animator của player
    public Sword sword; // Tham chiếu đến script Sword
    public Skill1 skill1; // Tham chiếu đến script Skill1
    public Skill2 skill2; // Tham chiếu đến script Skill2
    public Skill3 skill3;
    public Skill4 skill4;

    private bool isAnimating = false; // Biến để kiểm tra xem animation đang chạy hay không

    private bool skill1OnCooldown = false; // Biến để kiểm tra xem kỹ năng 1 có đang trong thời gian hồi chiêu không
    private bool skill2OnCooldown = false; // Biến để kiểm tra xem kỹ năng 2 có đang trong thời gian hồi chiêu không
    private bool skill3OnCooldown = false;
    private bool skill4OnCooldown = false;

    private float skill1CooldownTime = 3f; // Thời gian hồi chiêu cho kỹ năng 1
    private float skill2CooldownTime = 5f; // Thời gian hồi chiêu cho kỹ năng 2
    private float skill3CooldownTime = 7f;
    private float skill4CooldownTime = 9f;

    void Update()
    {
        // Kiểm tra input từ người chơi và kích hoạt animation của skill 1 khi nhấn phím 1
        if (Input.GetKeyDown(KeyCode.Alpha1) && !isAnimating && !skill1OnCooldown)
        {
            playerAnimator.SetTrigger("Skill1");
            skill1.UseSkill1();
            //Debug.Log("Skill 1 dang su dung");
            StartCoroutine(SkillCooldown(skill1CooldownTime, 1)); // Bắt đầu tính thời gian hồi chiêu cho kỹ năng 1
            //Debug.Log("Dang hoi skill 1");
            isAnimating = true; // Đặt biến isAnimating thành true khi bắt đầu animation
        }
        // Kiểm tra input từ người chơi và kích hoạt animation của skill 2 khi nhấn phím 2
        if (Input.GetKeyDown(KeyCode.Alpha2) && !isAnimating && !skill2OnCooldown)
        {
            playerAnimator.SetTrigger("Skill2");
            skill2.UseSkill2();
            StartCoroutine(SkillCooldown(skill2CooldownTime, 2)); // Bắt đầu tính thời gian hồi chiêu cho kỹ năng 2
            isAnimating = true; // Đặt biến isAnimating thành true khi bắt đầu animation
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !isAnimating && !skill3OnCooldown)
        {
            playerAnimator.SetTrigger("Skill3");
            skill3.UseSkill3();
            StartCoroutine(SkillCooldown(skill3CooldownTime, 3)); // Bắt đầu tính thời gian hồi chiêu cho kỹ năng 3
            isAnimating = true; // Đặt biến isAnimating thành true khi bắt đầu animation
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && !isAnimating && !skill4OnCooldown)
        {
            playerAnimator.SetTrigger("Skill4");
            skill4.UseSkill4();
            StartCoroutine(SkillCooldown(skill4CooldownTime, 2)); // Bắt đầu tính thời gian hồi chiêu cho kỹ năng 4 
            isAnimating = true; // Đặt biến isAnimating thành true khi bắt đầu animation
        }
    }

    // Coroutine để tính thời gian hồi chiêu cho kỹ năng
    IEnumerator SkillCooldown(float cooldownTime, int skillNumber)
    {
        if (skillNumber == 1)
        {
            skill1OnCooldown = true; // Đánh dấu kỹ năng 1 đang trong thời gian hồi chiêu
            yield return new WaitForSeconds(cooldownTime); // Chờ cho thời gian hồi chiêu kết thúc
            Debug.Log("Da hoi skill 1");
            skill1OnCooldown = false; // Đặt lại biến đánh dấu sau khi thời gian hồi chiêu kết thúc
        }
        else if (skillNumber == 2)
        {
            skill2OnCooldown = true; // Đánh dấu kỹ năng 2 đang trong thời gian hồi chiêu
            yield return new WaitForSeconds(cooldownTime); // Chờ cho thời gian hồi chiêu kết thúc
            skill2OnCooldown = false; // Đặt lại biến đánh dấu sau khi thời gian hồi chiêu kết thúc
        }
        else if (skillNumber == 3)
        {
            skill3OnCooldown = true; // Đánh dấu kỹ năng 3 đang trong thời gian hồi chiêu
            yield return new WaitForSeconds(cooldownTime); // Chờ cho thời gian hồi chiêu kết thúc
            skill3OnCooldown = false; // Đặt lại biến đánh dấu sau khi thời gian hồi chiêu kết thúc
        }
        else if (skillNumber == 4)
        {
            skill4OnCooldown = true; // Đánh dấu kỹ năng 4 đang trong thời gian hồi chiêu
            yield return new WaitForSeconds(cooldownTime); // Chờ cho thời gian hồi chiêu kết thúc
            skill4OnCooldown = false; // Đặt lại biến đánh dấu sau khi thời gian hồi chiêu kết thúc
        }
    }

    // Hàm được gọi từ script xử lý sự kiện hoàn thành animation
    public void OnAnimationEnd()
    {
        isAnimating = false; // Đặt biến isAnimating thành false khi animation kết thúc
    }
}
