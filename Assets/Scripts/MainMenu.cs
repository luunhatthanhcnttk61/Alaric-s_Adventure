using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject guideCanvas; 
    public GameObject mainMenuCanvas;

    private void Start()
    {
        if (guideCanvas != null)
        {
            guideCanvas.SetActive(false);
        }

        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(true);
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
            guideCanvas.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (mainMenuCanvas != null)
            {
                mainMenuCanvas.SetActive(false);
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
            guideCanvas.SetActive(false);

            if (mainMenuCanvas != null)
            {
                mainMenuCanvas.SetActive(true);
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
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
            SceneManager.LoadScene(data.currentScene);
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
