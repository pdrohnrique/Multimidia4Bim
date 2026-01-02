using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    bool _playerInsideZone;
    PlayerController _player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        _playerInsideZone = true;
        _player = other.GetComponent<PlayerController>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (other.GetComponent<PlayerController>() == _player)
        {
            _playerInsideZone = false;
            _player = null;
        }
    }

    public void TryToggleHide()
    {
        if (!_playerInsideZone || _player == null) return;

        _player.ToggleHide();
    }
}