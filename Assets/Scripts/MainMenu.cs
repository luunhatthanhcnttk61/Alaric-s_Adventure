// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class MainMenu : MonoBehaviour
// {
//     public void StartGame()
//     {
//         SceneManager.LoadScene("SampleScene");
//     }

//     public void ContinueGame()
//     {
//         PlayerData data = SaveSystem.LoadPlayer();
//         if (data != null)
//         {
//             SceneManager.LoadScene("GameScene");
//         }
//         else
//         {
//             Debug.LogError("No save data found!");
//         }
//     }
//     public void ExitGame()
//     {
//         Debug.Log("Game is exiting");
//         Application.Quit();
//     }
// }
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject guideCanvas; // Thêm tham chiếu tới GuideCanvas
    public GameObject mainMenuCanvas; // Thêm tham chiếu tới MainMenuCanvas

    private void Start()
    {
        if (guideCanvas != null)
        {
            guideCanvas.SetActive(false); // Đảm bảo GuideCanvas bắt đầu ở trạng thái không hiển thị
        }

        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(true); // Đảm bảo MainMenuCanvas bắt đầu ở trạng thái hiển thị
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void GuideUi()
    {
        if (guideCanvas != null)
        {
            guideCanvas.SetActive(true); // Hiển thị GuideCanvas
            Cursor.lockState = CursorLockMode.None; // Bỏ khóa con trỏ chuột
            Cursor.visible = true; // Hiển thị con trỏ chuột

            if (mainMenuCanvas != null)
            {
                mainMenuCanvas.SetActive(false); // Ẩn MainMenuCanvas
            }
        }
        else
        {
            Debug.LogError("GuideCanvas is not assigned in the inspector!");
        }
    }

    public void CloseGuideUi()
    {
        if (guideCanvas != null)
        {
            guideCanvas.SetActive(false); // Tắt GuideCanvas

            if (mainMenuCanvas != null)
            {
                mainMenuCanvas.SetActive(true); // Hiển thị MainMenuCanvas
            }

            Cursor.lockState = CursorLockMode.None; // Bỏ khóa con trỏ chuột
            Cursor.visible = true; // Hiển thị con trỏ chuột
        }
        else
        {
            Debug.LogError("GuideCanvas is not assigned in the inspector!");
        }
    }

    public void ContinueGame()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            Debug.LogError("No save data found!");
        }
    }

    public void ExitGame()
    {
        Debug.Log("Game is exiting");
        Application.Quit();
    }
}
