using System.Collections.Generic;
using UnityEngine;

public class NotesManager : MonoBehaviour
{
    public List<ItemData> collectedNotes = new List<ItemData>();
    public GameObject notesMenu; // painel de UI que você cria depois

    public void AddNote(ItemData note)
    {
        if (!collectedNotes.Contains(note))
            collectedNotes.Add(note);
    }

    public void ToggleNotesMenu()
    {
        if (notesMenu == null) return;

        bool active = !notesMenu.activeSelf;
        notesMenu.SetActive(active);
        Time.timeScale = active ? 0f : 1f;
        
        Cursor.visible = active;
        Cursor.lockState = active ? CursorLockMode.None : CursorLockMode.Locked;
    }
}