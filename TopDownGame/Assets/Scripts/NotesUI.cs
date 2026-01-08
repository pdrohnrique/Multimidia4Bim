using UnityEngine;
using TMPro;

public class NotesUI : MonoBehaviour
{
    public NotesManager notesManager;
    public TextMeshProUGUI noteTitleText;
    public TextMeshProUGUI noteBodyText;
    public GameObject emptyMessage; // texto "Nenhuma nota"

    void OnEnable()
    {
        Refresh();
    }

    private void Refresh()
    {
        if (notesManager == null || !notesManager.HasNotes)
        {
            if (emptyMessage != null)
                emptyMessage.SetActive(true);

            if (noteTitleText != null)
                noteTitleText.text = "";
            if (noteBodyText != null)
                noteBodyText.text = "";

            return;
        }

        if (emptyMessage != null)
            emptyMessage.SetActive(false);

        ItemData note = notesManager.GetCurrentNote();
        if (noteTitleText != null)
            noteTitleText.text = note.itemName;
        if (noteBodyText != null)
            noteBodyText.text = note.noteText;
    }

    public void OnNextPressed()
    {
        notesManager.NextNote();
        Refresh();
    }

    public void OnPrevPressed()
    {
        notesManager.PreviousNote();
        Refresh();
    }
}