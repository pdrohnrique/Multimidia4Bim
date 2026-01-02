using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject optionsPanel;
    bool _isPaused;

    void Awake()
    {
        Time.timeScale = 1f;
        _isPaused = false;

        if (pausePanel != null)
            pausePanel.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
                ResumeGame();
            else
                PauseGame();
        }
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

    public void ResumeGame()
    {
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
    }
    
    // Botão "Menu Principal"
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("TitleScreen");
    }

    // Botão "Sair do Jogo"
    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}