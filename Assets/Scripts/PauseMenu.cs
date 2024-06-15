using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private PauseManager pauseManager;
    private GameManager gameManager;

    private void Start()
    {
        pauseManager = FindObjectOfType<PauseManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public void Resume()
    {
        if (pauseManager != null)
        {
            pauseManager.Resume();
        }
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
