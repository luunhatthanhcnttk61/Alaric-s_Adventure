using UnityEngine;

public class AnimationEndHandler : MonoBehaviour
{
    private Transform playerTransform;

    void Start()
    {
        playerTransform = transform; // Lấy tham chiếu đến transform của người chơi
    }

    // Phương thức này sẽ được gọi khi animation kết thúc
    public void OnAnimationEnd()
    {
        // Cập nhật vị trí của người chơi thành vị trí hiện tại của transform
        PlayerManager.Instance.UpdatePlayerPosition(playerTransform.position);
    }
}
