using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject optionsPanel;
    bool _isPaused;
    public bool IsPaused => _isPaused;

    void Awake()
    {
        Time.timeScale = 1f;
        _isPaused = false;

        if (pausePanel != null)
            pausePanel.SetActive(false);
        if (optionsPanel != null)
            optionsPanel.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void TogglePause()
    {
        if (_isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    private void PauseGame()
    {
        if (pausePanel != null)
            pausePanel.SetActive(true);

        Time.timeScale = 0f;
        _isPaused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void ResumeGame()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(false);
        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;
        _isPaused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void OpenOptions()
    {
        if (optionsPanel == null || pausePanel == null) return;

        pausePanel.SetActive(false);
        optionsPanel.SetActive(true);
        // continua pausado, cursor já está visível
    }

    public void CloseOptions()
    {
        if (optionsPanel == null || pausePanel == null) return;

        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
        // continua pausado
    }
    
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("TitleScreen");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}