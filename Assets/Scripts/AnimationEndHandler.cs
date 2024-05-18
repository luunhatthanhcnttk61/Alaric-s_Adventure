// using UnityEngine;

// public class AnimationEndHandler : MonoBehaviour
// {
//     private Transform playerTransform;

//     void Start()
//     {
//         playerTransform = transform; // Lấy tham chiếu đến transform của người chơi
//     }

//     // Phương thức này sẽ được gọi khi animation kết thúc
//     public void OnAnimationEnd()
//     {
//         // Cập nhật vị trí của người chơi thành vị trí hiện tại của transform
//         PlayerManager.Instance.UpdatePlayerPosition(playerTransform.position);
//     }
// }
using UnityEngine;

public class AnimationEndHandler : MonoBehaviour
{
    private SkillManager skillManager;

    void Start()
    {
        // Tìm đối tượng SkillManager trong scene và lấy tham chiếu
        skillManager = FindObjectOfType<SkillManager>();
        if (skillManager == null)
        {
            Debug.LogError("SkillManager not found!");
        }
    }

    // Phương thức này sẽ được gọi khi animation kết thúc
    public void OnAnimationEnd()
    {
        if (skillManager != null)
        {
            skillManager.OnAnimationEnd();
            Debug.Log("Animation ended, calling SkillManager.OnAnimationEnd()");
        }
    }
}
