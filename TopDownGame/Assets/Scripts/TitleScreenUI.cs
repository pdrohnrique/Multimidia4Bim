using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenUI : MonoBehaviour
{
    public GameObject optionsPanel; // painel de opções (som)
    public GameObject controlsPanel;

    public void OnPlayPressed()
    {
        // Carrega a cena principal (índice 1 ou pelo nome)
        SceneManager.LoadScene("MainScene");
    }

    public void OnControlsPressed()
    {
        if  (controlsPanel == null) return;   
        controlsPanel.SetActive(true);
    }
    
    public void OnOptionsPressed()
    {
        if (optionsPanel == null) return;
        optionsPanel.SetActive(true);
    }

    public void OnBackFromControls()
    {
        if (controlsPanel == null) return;
        controlsPanel.SetActive(false);
    }

    public void OnQuitPressed()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}