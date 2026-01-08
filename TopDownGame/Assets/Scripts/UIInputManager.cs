using UnityEngine;

public class UIInputManager : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public NotesManager notesManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Se notas estiverem abertas, ESC não mexe
            if (notesManager != null && notesManager.notesMenu != null &&
                notesManager.notesMenu.activeSelf)
                return;

            if (pauseMenu != null)
                pauseMenu.TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Se o jogo estiver pausado, Tab não abre notas
            if (pauseMenu != null && pauseMenu.IsPaused)
                return;

            if (notesManager != null)
                notesManager.ToggleNotesMenu();
        }
    }
}