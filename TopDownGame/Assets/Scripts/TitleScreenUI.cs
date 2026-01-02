using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenUI : MonoBehaviour
{
    public GameObject optionsPanel; // painel de opções (som)

    public void OnPlayPressed()
    {
        // Carrega a cena principal (índice 1 ou pelo nome)
        SceneManager.LoadScene("MainScene");
    }

    public void OnOptionsPressed()
    {
        if (optionsPanel == null) return;
        optionsPanel.SetActive(true);
    }

    public void OnBackFromOptions()
    {
        if (optionsPanel == null) return;
        optionsPanel.SetActive(false);
    }

    public void OnQuitPressed()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}