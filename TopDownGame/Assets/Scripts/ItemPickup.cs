using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public ItemData itemData;

    bool _playerInside;
    Inventory _playerInventory;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        _playerInside = true;
        _playerInventory = other.GetComponent<Inventory>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (other.GetComponent<Inventory>() == _playerInventory)
        {
            _playerInside = false;
            _playerInventory = null;
        }
    }

    public void TryPickup()
    {
        if (!_playerInside || _playerInventory == null) return;

        _playerInventory.AddItem(itemData);

        // Se for nota, avisa o NotesManager do player
        if (itemData.type == ItemType.Note)
        {
            var notes = _playerInventory.GetComponent<NotesManager>();
            if (notes != null)
            {
                notes.AddNote(itemData);
                Debug.Log("Nota adicionada ao NotesManager: " + itemData.itemName);
            }
        }
        
        // Chave: também liga com PlayerController, se quiser feedback
        if (itemData.type == ItemType.Key)
        {
            var player = _playerInventory.GetComponent<PlayerController>();
            if (player != null)
                player.GiveKey();
        }

        Destroy(gameObject);
    }
}