using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public PauseManager pauseManager;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Resume()
    {
        Debug.Log("Da nhan Resume");
        pauseManager.Resume();
    }

    public void LoadMainMenu()
    {
        if (gameManager != null)
        {
            gameManager.SavePlayerData();
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        if (gameManager != null)
        {
            gameManager.SavePlayerData();
        }
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
