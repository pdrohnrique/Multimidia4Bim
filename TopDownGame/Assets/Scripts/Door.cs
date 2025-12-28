using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Teleporte")]
    public Transform teleportTarget;
    public string targetAreaName; // "Recepcao", "Corredor", "Laboratorio"
    
    [Header("Lock")]
    public bool requiresKey; // marca no Inspector
    
    bool _playerInside;
    Transform _player;
    CameraAreaSwitcher _areaSwitcher;
    PlayerController _playerController;

    void Start()
    {
        _areaSwitcher = FindFirstObjectByType<CameraAreaSwitcher>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        _playerInside = true;
        _player = other.transform;
        _playerController = other.GetComponent<PlayerController>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        _playerInside = false;
        if (_player == other.transform)
        {
            _player = null;
            _playerController = null;
        }
    }

    public void TryUseDoor()
    {
        if (!_playerInside || teleportTarget == null) return;

        // Se precisa de chave, checa antes
        if (requiresKey)
        {
            if (_playerController == null || !_playerController.hasKey)
            {
                Debug.Log("Porta trancada. Você precisa de uma chave.");
                return;
            }
        }
        // Teleporta
        _player.position = teleportTarget.position;

        // Troca bounds da câmera
        if (_areaSwitcher != null && !string.IsNullOrEmpty(targetAreaName))
            _areaSwitcher.SetArea(targetAreaName);
    }
}
