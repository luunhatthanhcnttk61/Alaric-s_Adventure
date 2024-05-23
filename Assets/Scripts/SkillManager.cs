using UnityEngine;
using System.Collections;

public class SkillManager : MonoBehaviour
{
    public GameObject player; // Tham chiếu đến GameObject của player
    public Animator playerAnimator; // Tham chiếu đến Animator của player
    public PlayerController2 playerMovement; // Tham chiếu đến script PlayerController2
    public Sword sword; // Tham chiếu đến script Sword
    public Skill1 skill1; // Tham chiếu đến script Skill1
    public Skill2 skill2; // Tham chiếu đến script Skill2
    public Skill3 skill3; // Tham chiếu đến script Skill3
    public Skill4 skill4; // Tham chiếu đến script Skill4

    private bool isAnimating = false; // Biến để kiểm tra xem animation đang chạy hay không

    private bool skill1OnCooldown = false; // Biến để kiểm tra xem kỹ năng 1 có đang trong thời gian hồi chiêu không
    private bool skill2OnCooldown = false; // Biến để kiểm tra xem kỹ năng 2 có đang trong thời gian hồi chiêu không
    private bool skill3OnCooldown = false; // Biến để kiểm tra xem kỹ năng 3 có đang trong thời gian hồi chiêu không
    private bool skill4OnCooldown = false; // Biến để kiểm tra xem kỹ năng 4 có đang trong thời gian hồi chiêu không

    private float skill1CooldownTime = 3f; // Thời gian hồi chiêu cho kỹ năng 1
    private float skill2CooldownTime = 5f; // Thời gian hồi chiêu cho kỹ năng 2
    private float skill3CooldownTime = 7f; // Thời gian hồi chiêu cho kỹ năng 3
    private float skill4CooldownTime = 9f; // Thời gian hồi chiêu cho kỹ năng 4

    // Thêm các giá trị mana cần thiết cho từng skill
    public float skill1ManaCost = 10f;
    public float skill2ManaCost = 30f;
    public float skill3ManaCost = 40f;
    public float skill4ManaCost = 70f;

    void Update()
    {
        // Kiểm tra input từ người chơi và kích hoạt animation của skill 1 khi nhấn phím 1
        if (Input.GetKeyDown(KeyCode.Alpha1) && !isAnimating && !skill1OnCooldown)
        {
            Debug.Log("Da nhan phim 1");
            if (playerMovement.UseMana(skill1ManaCost))
            {
                playerAnimator.SetTrigger("Skill1");
                skill1.UseSkill1();
                StartCoroutine(SkillCooldown(skill1CooldownTime, 1)); // Bắt đầu tính thời gian hồi chiêu cho kỹ năng 1
                isAnimating = true; // Đặt biến isAnimating thành true khi bắt đầu animation
                playerMovement.SetCanMove(false); // Vô hiệu hóa chuyển động
            }
            else
            {
                Debug.Log("Not enough mana for Skill 1");
            }
        }
        // Kiểm tra input từ người chơi và kích hoạt animation của skill 2 khi nhấn phím 2
        if (Input.GetKeyDown(KeyCode.Alpha2) && !isAnimating && !skill2OnCooldown)
        {
            if (playerMovement.UseMana(skill2ManaCost))
            {
                playerAnimator.SetTrigger("Skill2");
                skill2.UseSkill2();
                StartCoroutine(SkillCooldown(skill2CooldownTime, 2)); // Bắt đầu tính thời gian hồi chiêu cho kỹ năng 2
                isAnimating = true; // Đặt biến isAnimating thành true khi bắt đầu animation
                playerMovement.SetCanMove(false); // Vô hiệu hóa chuyển động
            }
            else
            {
                Debug.Log("Not enough mana for Skill 2");
            }
        }
        // Kiểm tra input từ người chơi và kích hoạt animation của skill 3 khi nhấn phím 3
        if (Input.GetKeyDown(KeyCode.Alpha3) && !isAnimating && !skill3OnCooldown)
        {
            if (playerMovement.UseMana(skill3ManaCost))
            {
                playerAnimator.SetTrigger("Skill3");
                skill3.UseSkill3();
                StartCoroutine(SkillCooldown(skill3CooldownTime, 3)); // Bắt đầu tính thời gian hồi chiêu cho kỹ năng 3
                isAnimating = true; // Đặt biến isAnimating thành true khi bắt đầu animation
                playerMovement.SetCanMove(false); // Vô hiệu hóa chuyển động
            }
            else
            {
                Debug.Log("Not enough mana for Skill 3");
            }
        }
        // Kiểm tra input từ người chơi và kích hoạt animation của skill 4 khi nhấn phím 4
        if (Input.GetKeyDown(KeyCode.Alpha4) && !isAnimating && !skill4OnCooldown)
        {
            if (playerMovement.UseMana(skill4ManaCost))
            {
                playerAnimator.SetTrigger("Skill4");
                skill4.UseSkill4();
                StartCoroutine(SkillCooldown(skill4CooldownTime, 4)); // Bắt đầu tính thời gian hồi chiêu cho kỹ năng 4 
                isAnimating = true; // Đặt biến isAnimating thành true khi bắt đầu animation
                playerMovement.SetCanMove(false); // Vô hiệu hóa chuyển động
            }
            else
            {
                Debug.Log("Not enough mana for Skill 4");
            }
        }
    }

    // Coroutine để tính thời gian hồi chiêu cho kỹ năng
    IEnumerator SkillCooldown(float cooldownTime, int skillNumber)
    {
        switch(skillNumber)
        {
            case 1:
                skill1OnCooldown = true;
                yield return new WaitForSeconds(cooldownTime);
                skill1OnCooldown = false;
                break;
            case 2:
                skill2OnCooldown = true;
                yield return new WaitForSeconds(cooldownTime);
                skill2OnCooldown = false;
                break;
            case 3:
                skill3OnCooldown = true;
                yield return new WaitForSeconds(cooldownTime);
                skill3OnCooldown = false;
                break;
            case 4:
                skill4OnCooldown = true;
                yield return new WaitForSeconds(cooldownTime);
                skill4OnCooldown = false;
                break;
        }
    }

    // Hàm được gọi từ script xử lý sự kiện hoàn thành animation
    public void OnAnimationEnd()
    {
        isAnimating = false; // Đặt biến isAnimating thành false khi animation kết thúc
        Debug.Log("Ket thuc animation");
        playerMovement.SetCanMove(true); // Kích hoạt lại chuyển động
        Debug.Log("Player co the chuyen dong");
    }
}
