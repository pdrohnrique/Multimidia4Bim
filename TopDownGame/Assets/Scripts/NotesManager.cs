using System.Collections.Generic;
using UnityEngine;

public class NotesManager : MonoBehaviour
{
    public List<ItemData> collectedNotes = new List<ItemData>();
    public int currentIndex;
    public GameObject notesMenu;

    public void AddNote(ItemData note)
    {
        if (!collectedNotes.Contains(note))
            collectedNotes.Add(note);
    }

    public bool HasNotes => collectedNotes.Count > 0;

    public ItemData GetCurrentNote()
    {
        if (!HasNotes) return null;
        currentIndex = Mathf.Clamp(currentIndex, 0, collectedNotes.Count - 1);
        return collectedNotes[currentIndex];
    }

    public void NextNote()
    {
        if (!HasNotes) return;
        currentIndex++;
        if  (currentIndex >= collectedNotes.Count) currentIndex = 0;
    }

    public void PreviousNote()
    {
        if (!HasNotes) return;
        currentIndex--;
        if (currentIndex < 0) currentIndex = collectedNotes.Count - 1;
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